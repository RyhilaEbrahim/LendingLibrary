using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
           ViewBag.Message = "Hello";
            return View();
        }

        public ActionResult Get(int? id)
        {
            ViewBag.Message = id;
            return View();
        }

        public ActionResult Create()
        {
            var viewModel = new TestViewModel {Items = CreateItemsList()};
            return View(viewModel);
        }

        private static SelectList CreateItemsList()
        {
            return new SelectList (new string [] {"Apple","Pear","Bananna"});
        }


        [HttpPost]
        public ActionResult Create(TestViewModel model)
        {
            if (ModelState.IsValid) return RedirectToAction("Index");
            model.Items = CreateItemsList();
             
            return View(model);

            //do save here
        }
    }
}