using Book.IService.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Dao.Repository;
using Book.Dao.Entity;
using Book.Service.Common;
using Book.IService.ViewModels;
using Book.Service.ModelConverter;
using Book.Common;
using System.Reflection;

namespace Book.Service.WebService
{
    public class BookWebService : IBookWebService
    {
        private Logger logger = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IBookDBRepository mDb = new BookDBRepository();
        public string GetBookLists()
        {
            try
            {
                var result = this.mDb.All<BookEntity>().ToList();
                return SerializerHelper.SerializeByJsonConvert(result.FirstOrDefault().ToViewModel());
            }
            catch (Exception e)
            {
                return SerializerHelper.SerializeByJsonConvert(string.Empty);
            }
        }

        public string GetBookAndReviews(string id)
        {
            var result = new GetBookResult();
            try
            {
                result.Book = this.mDb.Filter<BookEntity>(c => c.ID == id).FirstOrDefault()?.ToViewModel();
                result.Reviews = this.mDb.Filter<ReviewEntity>(c => c.BookId == id)?.ToList()?.ToViewModel();
                return SerializerHelper.SerializeByJsonConvert(result);
            }
            catch (Exception e)
            {
                logger.Error("Error in get book info and book reviews , id is {0}, error is {1}", id, e.Message);
                return string.Empty;
            }
        }

        public string load(int pageIndex, int pageSize, string userId)
        {
            var result = new LoadResult();
            try
            {
                int pageTotal = 1;
                var books = this.mDb.Filter<CollectionEntity, object>(c => c.UserId == userId, c => c.Book.Title, out pageTotal, pageIndex, pageSize).Select(c => c.Book)?.ToList().ToViewModel();
                if (books.Count > 0)
                {
                    books.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                else
                {
                    logger.Info("{0} has no book marked.", userId);
                }
                result.Books = books;
                result.PageTotal = pageTotal / pageSize;
                return SerializerHelper.SerializeByJsonConvert(result);
            }
            catch (Exception e)
            {
                logger.Error("load book error : {0}", e.Message);
            }
            return string.Empty;
        }

        public string Save(List<BookViewModel> books)
        {
            var index = 0;
            foreach (var item in books)
            {
                try
                {
                    BookEntity entity = item.ToEntity();
                    if (!this.mDb.Contains<BookEntity>(c => c.ID == entity.ID))
                    {
                        this.mDb.Add(entity);
                    }
                    index++;
                }
                catch (Exception e)
                {
                }
            }
            return SerializerHelper.SerializeByJsonConvert(index == books.Count);
        }

        public string GetBook(string id)
        {
            try
            {
                var book = this.mDb.Filter<BookEntity>(c => c.ID == id).FirstOrDefault()?.ToViewModel();

                return SerializerHelper.SerializeByJsonConvert(book);
            }
            catch (Exception e)
            {
                logger.Error("Error in get a book, id is {0}, error is {1}", id, e.Message);
                return string.Empty;
            }
        }
    }


    [Serializable]
    public class LoadResult
    {
        public List<BookViewModel> Books { get; set; }
        public int PageTotal { get; set; }
    }

    [Serializable]
    public class GetBookResult
    {
        public BookViewModel Book { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}
