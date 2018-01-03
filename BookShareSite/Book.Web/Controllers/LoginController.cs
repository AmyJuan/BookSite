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
    public class LoginController : Controller
    {
        private IUserWebService userWebService = new UserWebService();
        private IBookWebService bookWebService = new BookWebService();
        private ICollectionWebService collectionWebService = new CollectionWebService();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Load()
        {
            var result = "2";
            userWebService.TestCore();
            //var result = userWebService.GetUserLists();
            return Content(result);
        }

        public ActionResult LoadUsers()
        {
            var result = userWebService.GetUserLists();
            return Content(result);
        }

        public ActionResult SaveUsers(List<UserViewModel> datas)
        {
            var result = userWebService.Save(datas);
            return Content(result);
        }

        public ActionResult SaveBooks(List<BookViewModel> datas)
        {
            var result = bookWebService.Save(datas);
            return Content(result);
        }

        public ActionResult SaveCollections(List<CollectionViewModel> datas)
        {
            var result = collectionWebService.Save(datas);
            return Content(result);
        }

    }
}