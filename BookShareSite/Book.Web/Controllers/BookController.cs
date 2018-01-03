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
    public class BookController : Controller
    {
        private IBookWebService bookWebService = new BookWebService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult ViewBook()
        {
            return View();
        }

        public ActionResult LoadBook(string id)
        {
            var result = bookWebService.GetBook(id);
            return Content(result);
        }

        public ActionResult LoadBookAndReviews(string id)
        {
            var result = bookWebService.GetBookAndReviews(id);
            return Content(result);
        }
        public ActionResult Load(int pageIndex, int pageSize)
        {
            var result = bookWebService.load(pageIndex, pageSize, "3616295");
            return Content(result);
        }
        public ActionResult Save(BookViewModel data)
        {
            var datas = new List<BookViewModel>() { data };
            var result = bookWebService.Save(datas);
            return Content(result);
        }
    }
}