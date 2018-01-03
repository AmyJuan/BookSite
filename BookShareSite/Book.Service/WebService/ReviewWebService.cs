using Book.IService.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.IService.ViewModels;
using Book.Dao.Entity;
using Book.Service.ModelConverter;
using Book.Common;
using Book.Dao.Repository;
using System.Reflection;
using Book.Service.Common;

namespace Book.Service.WebService
{
    public class ReviewWebService : IReviewWebService
    {
        private Logger logger = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IBookDBRepository mDb = new BookDBRepository();

        public string Load(string bookid)
        {
            try
            {
                var reviews = this.mDb.Filter<ReviewEntity>(c => c.BookId == bookid)?.ToList().ToViewModel();
                return SerializerHelper.SerializeByJsonConvert(reviews);
            }
            catch (Exception e)
            {
                logger.Error("Load Review Error,book id is{0}, error is {1}", bookid, e.Message);
            }
            return string.Empty;
        }

        public string LoadBookAndReview(string bookId, string userId)
        {
            try
            {
                var result = new ReviewLoadResult();
                result.Book = this.mDb.Filter<BookEntity>(c => c.ID == bookId)?.ToList()?.FirstOrDefault()?.ToViewModel();
                result.Review = this.mDb.Filter<ReviewEntity>(c => c.BookId == bookId && c.UserId == userId)?.ToList()?.FirstOrDefault()?.ToViewModel();
                return SerializerHelper.SerializeByJsonConvert(result);
            }
            catch (Exception e)
            {
                logger.Error("Load Review Error,book id is{0}, user id is {1},error is {2}", bookId, userId, e.Message);
            }
            return string.Empty;
        }

        public string Save(List<ReviewViewModel> reviews)
        {
            var index = 0;
            if (reviews == null) return string.Empty;
            foreach (var item in reviews)
            {
                try
                {
                    ReviewEntity entity = item.ToEntity();
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
            return SerializerHelper.SerializeByJsonConvert(index == reviews.Count);
        }
    }

    public class ReviewLoadResult
    {
        public BookViewModel Book { get; set; }
        public ReviewViewModel Review { get; set; }
    }
}
