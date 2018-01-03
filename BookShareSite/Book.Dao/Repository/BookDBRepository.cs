namespace Book.Dao.Repository
{
    using Dao;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class BookDBRepository : IBookDBRepository
    {
        private BookContext mBookContext;
        private static readonly object mSyncRoot = new object();

        public BookDBRepository()
        {
            lock (mSyncRoot)
            {
                if (mBookContext == null)
                {
                    lock (mSyncRoot)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["BookContext"]?.ToString();
                        //string connectionString = ContextString.Default();
                        mBookContext = new BookContext(connectionString);
                    }
                }
            }
        }

        public BookDBRepository(string connstr)
        {
            lock (mSyncRoot)
            {
                if (mBookContext == null)
                {
                    lock (mSyncRoot)
                    {
                        this.mBookContext = new BookContext(connstr);
                    }
                }
            }
        }

        public IEnumerable<object> EntityConfigurations
        {
            get
            {
                return new BookDsConfiguration().Populate();
            }
        }

        public IQueryable<T> All<T>() where T : class
        {
            return this.GetDbSet<T>().AsQueryable();
        }

        public void Dispose()
        {
            this.mBookContext.Dispose();
        }

        public void Execute(Action<DbContext> action)
        {
            action(this.mBookContext);
        }

        public bool Exist<T>(T model) where T : class
        {
            return this.All<T>().Contains<T>(model);
        }

        public bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.mBookContext.Set<T>().Where(predicate).Count() > 0;
        }

        public IDbSet<T> GetDbSet<T>() where T : class
        {
            return this.mBookContext.Set<T>();
        }

        public T Add<T>(T t) where T : class
        {
            mBookContext.Set<T>().Add(t);
            mBookContext.SaveChanges();
            return t;
        }

        public IEnumerable<T> AddRange<T>(IEnumerable<T> TObjects, bool isSaveChange = false) where T : class
        {
            mBookContext.Set<T>().AddRange(TObjects);
            if (isSaveChange)
            {
                this.SaveChanges();
            }
            return TObjects;
        }

        public int SaveChanges()
        {
            try
            {
                return this.mBookContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
            catch (DbUpdateException e)
            {
                throw;
            }
        }

        public IQueryable<T> Filter<T, TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> orderBy, out int total, int index = 1, int size = 1000) where T : class
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Index out of range.");
            }
            total = this.Count<T>(filter);
            return this.mBookContext.Set<T>().Where(filter).OrderBy(orderBy).Skip((index - 1) * size).Take(size);
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.mBookContext.Set<T>().Where(predicate).AsQueryable();
        }

        public IQueryable<TResult> Filter<T, TResult>(Expression<Func<T, TResult>> selector) where T : class
        {
            return this.mBookContext.Set<T>().Select(selector).AsQueryable();
        }

        private int Count<T>(Expression<Func<T, bool>> where) where T : class
        {
            return this.mBookContext.Set<T>().AsNoTracking().Where(where).Count();
        }
    }
}
