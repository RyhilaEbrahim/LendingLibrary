using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class BorrowerController : Controller
    {
        private Dictionary<int, BorrowerViewModel> _borrowers = new Dictionary<int, BorrowerViewModel>
        {
            {
                1,new BorrowerViewModel {Id =1,FirstName = "Ryhila",Surname = "Ebrahim",Email = "Ryhila@test.com"}
            }
        }; 

        // GET: Borrower
        public ActionResult Index()
        {
            var borrowerViewModels = _borrowers.Values.ToList();
            return View(borrowerViewModels);
        }

        // GET: Borrower/Details/5
        public ActionResult Details(int id)
        {
            var borrowViewModel = _borrowers[id];
            return View(borrowViewModel);
        }

        // GET: Borrower/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Borrower/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Borrower/Edit/5
        public ActionResult Edit(int id)
        {
            var borrowViewModel = _borrowers[id];
            return View(borrowViewModel);
        }

        // POST: Borrower/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Borrower/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Borrower/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
