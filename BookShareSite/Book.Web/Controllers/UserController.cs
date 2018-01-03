using Book.IService.Result;
using Book.IService.WebService;
using Book.Service.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserWebService userWebService = new UserWebService();
        private IBookWebService bookWebService = new BookWebService();
        private ICollectionWebService collectionWebService = new CollectionWebService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reviews()
        {
            return View();
        }

        public ActionResult Marks()
        {
            return View();
        }

        public ActionResult Load(string id)
        {
            var result = userWebService.GetUser(id);
            return Content(result);
        }

        public ActionResult ApiSave(UserModel data)
        {
            if (data != null)
            {
                bookWebService.Save(data.Books);
                collectionWebService.Save(data.Collections);
            }
            return Content(string.Empty);
        }
    }
}