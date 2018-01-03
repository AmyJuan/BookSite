namespace Book.Dao.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBookDBRepository : IDisposable
    {
        IEnumerable<object> EntityConfigurations { get; }

        IQueryable<T> All<T>() where T : class;

        IDbSet<T> GetDbSet<T>() where T : class;

        bool Exist<T>(T model) where T : class;

        bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class;

        T Add<T>(T t) where T : class;

        IEnumerable<T> AddRange<T>(IEnumerable<T> TObjects, bool isSaveChange = false) where T : class;

        int SaveChanges();

        void Execute(Action<DbContext> action);

        IQueryable<T> Filter<T, TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> orderBy, out int total, int index = 0, int size = 50) where T : class;

        IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class;

        IQueryable<TResult> Filter<T, TResult>(Expression<Func<T, TResult>> selector) where T : class;
    }
}
