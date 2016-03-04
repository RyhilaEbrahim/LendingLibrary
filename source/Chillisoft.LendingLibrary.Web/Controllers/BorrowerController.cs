using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.DB;
using Chillisoft.LendingLibrary.DB.Repositories;
using Chillisoft.LendingLibrary.Web.Models;
using IBorrowerRepository = Chillisoft.LendingLibrary.Core.Interfaces.Repositories.IBorrowerRepository;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class BorrowerController : Controller
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMappingEngine _mappingEngine;


        // GET: BorrowerViewModel
        public BorrowerController(IBorrowerRepository borrowerRepository, IMappingEngine mappingEngine)
        {
            _borrowerRepository = borrowerRepository;
            _mappingEngine = mappingEngine;
        }

        public ActionResult Index()
        {
            var borrowers = _borrowerRepository.GetAll();
            var borrowerViewModels = _mappingEngine.Map<IEnumerable<BorrowerViewModel>>(borrowers);
            return View("Index", borrowerViewModels);
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
            var viewModel = new BorrowerViewModel();
            viewModel.TitlesSelectList = GetTitles(null);
            return View(viewModel);
        }

        private List<SelectListItem> GetTitles(int? titleId)
        {
            var selectListItems = _borrowerRepository.GetAllTitles()
                .Select(t => new SelectListItem {Value = t.Id.ToString(), Text = t.Description, Selected = t.Id == titleId.GetValueOrDefault()});
            return selectListItems.ToList();
        }

        // POST: BorrowerViewModel/Create
        [HttpPost]
        public ActionResult Create(BorrowerViewModel viewModel, HttpPostedFileBase file = null)
        {
            if (file != null)
            {
                var photo = ReadFully(file.InputStream);
                viewModel.Photo = photo;
              
            }
            var borrower = _mappingEngine.Map<Borrower>(viewModel);
            var title = _borrowerRepository.GetTitleById(viewModel.TitleId);
            borrower.Title = title;
            if (file != null) borrower.ContentType = file.ContentType.ToLower();
            _borrowerRepository.Save(borrower);
            return RedirectToAction("Index");
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        // GET: BorrowerViewModel/Edit/5
        public ActionResult Edit(int id)
        {

            var borrower = _borrowerRepository.Get(id);
           
            var borrowViewModel = _mappingEngine.Map<BorrowerViewModel>(borrower);
            borrowViewModel.TitlesSelectList = GetTitles(borrowViewModel.TitleId);
            return View(borrowViewModel);
        }

        // POST: BorrowerViewModel/Edit/5
        [HttpPost]
        public ActionResult Edit(BorrowerViewModel viewModel, HttpPostedFileBase file = null)
        {
            try
            {
                if (file != null)
                {
                    var photo = ReadFully(file.InputStream);
                    viewModel.Photo = photo;

                }
                var borrower = _mappingEngine.Map<Borrower>(viewModel);
                var title = _borrowerRepository.GetTitleById(viewModel.TitleId);
                borrower.Title = title;
                if (file != null) borrower.ContentType = file.ContentType.ToLower();
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
        public ActionResult Delete(BorrowerViewModel viewModel)
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
