using Book.IService.Result;
using Book.IService.ViewModels;
using Book.IService.WebService;
using Book.Service.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Web.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewWebService reviewWebService = new ReviewWebService();
        private IUserWebService userWebService = new UserWebService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult ApiSave(ReviewModel data)
        {
            if (data != null)
            {
                reviewWebService.Save(data.Reviews);
                userWebService.Save(data.Users);
            }
            return Content(string.Empty);
        }

        public ActionResult Save(ReviewViewModel data)
        {
            if (data != null)
            {
                var reviews = new List<ReviewViewModel>() { data };
                reviewWebService.Save(reviews);
            }
            return Content(string.Empty);
        }

        public ActionResult Load(string bookid)
        {
            var result = reviewWebService.Load(bookid);
            return Content(result);
        }

        public ActionResult LoadBookAndReview(string bookId, string userId)
        {
            var result = reviewWebService.LoadBookAndReview(bookId, userId);
            return Content(result);
        }

    }
}