using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Web.Models;
using Chillisoft.LendingLibrary.Web.Repositories;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class BorrowerController : Controller
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMappingEngine _mappingEngine;


        // GET: BorrowerViewModel
        public BorrowerController() : this(new BorrowerRepository(), Mapper.Engine)
        {
        }

        public BorrowerController(IBorrowerRepository borrowerRepository, IMappingEngine mappingEngine)
        {
            _borrowerRepository = borrowerRepository;
            _mappingEngine = mappingEngine;
        }

        public ActionResult Index()
        {
            var borrowers = _borrowerRepository.GetAll();
            var borrowerViewModels = _mappingEngine.Map<IEnumerable<BorrowerViewModel>>(borrowers);
            return View(borrowerViewModels);
        }

      
        // GET: BorrowerViewModel/Details/5
        public ActionResult Details(int id)
        {
            var borrower = _borrowerRepository.Get(id);
            var borrowViewModel = _mappingEngine.Map<BorrowerViewModel>(borrower);
            return View(borrowViewModel);
        }

        // GET: BorrowerViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BorrowerViewModel/Create
        [HttpPost]
        public ActionResult Create(BorrowerViewModel viewModel)
        {
            try
            {
                var borrower = _mappingEngine.Map<Borrower>(viewModel);
                // TODO: Add insert logic here
                _borrowerRepository.Save(borrower);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: BorrowerViewModel/Edit/5
        public ActionResult Edit(int id)
        {

            var borrower = _borrowerRepository.Get(id);
            var borrowViewModel = _mappingEngine.Map<BorrowerViewModel>(borrower);
           
            return View(borrowViewModel);
        }

        // POST: BorrowerViewModel/Edit/5
        [HttpPost]
        public ActionResult Edit(BorrowerViewModel viewModel)
        {
            try
            {
                var borrower = _mappingEngine.Map<Borrower>(viewModel);
                _borrowerRepository.Save(borrower);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BorrowerViewModel/Delete/5
        public ActionResult Delete(int id)
        {
            var borrower = _borrowerRepository.Get(id);
            var borrowViewModel = _mappingEngine.Map<BorrowerViewModel>(borrower);
            return View(borrowViewModel);
        }

        // POST: BorrowerViewModel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,BorrowerViewModel viewModel)
        {
            try
            {
                var borrower = _mappingEngine.Map<Borrower>(viewModel);
                _borrowerRepository.Delete(borrower);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
