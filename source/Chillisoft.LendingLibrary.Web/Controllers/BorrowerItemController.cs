using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class BorrowerItemController : Controller
    {
        private readonly IBorrowerItemRepository _borrowerItemRepository;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMappingEngine _mappingEngine;

        public BorrowerItemController(IBorrowerItemRepository borrowerItemRepository, IMappingEngine mappingEngine, IBorrowerRepository borrowerRepository, IItemRepository itemRepository)
        {
            _borrowerItemRepository = borrowerItemRepository;
            _mappingEngine = mappingEngine;
            _borrowerRepository = borrowerRepository;
            _itemRepository = itemRepository;
        }
        // GET: BorrowerItem
        public ActionResult Index()
        {
            var borrowersItems = _borrowerItemRepository.GetAll();
            var borrowerItemViewModels = _mappingEngine.Map<IEnumerable<BorrowerItemRowViewModel>>(borrowersItems);
            return View("Index", borrowerItemViewModels);

        }

        // GET: BorrowerItem/Details/5
        public ActionResult Details(int id)
        {
            
            var borrowerItem = _borrowerItemRepository.Get(id);
           
            var borrowerItemViewModel = _mappingEngine.Map<BorrowerItemViewModel>(borrowerItem);
                return View(borrowerItemViewModel);
        }

        private List<SelectListItem> GetAllItems (int? titleId=null)
        {
            var selectListItems = _itemRepository.GetAll()
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Description, Selected = t.Id == titleId.GetValueOrDefault() });
            return selectListItems.ToList();
        }

        private List<SelectListItem> GetAllBorrowers (int? borrowerId=null)
        {
            var selectListItems = _borrowerRepository.GetAll()
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.FirstName+","+t.Surname, Selected = t.Id == borrowerId.GetValueOrDefault() });
            return selectListItems.ToList();
        }
        // GET: BorrowerItem/Create
        public ActionResult Create()
        {
            var viewModel = new BorrowerItemViewModel
            {
                ItemSelectListItems = GetAllItems(),
                BorrowersSelectListItems=GetAllBorrowers(),
                DateBorrowed = DateTime.Now
            };

            return View(viewModel);
        }

        private void SetSelectListToDefaults(BorrowerItemViewModel viewModel)
        {
            viewModel.ItemSelectListItems = GetAllItems();
            viewModel.BorrowersSelectListItems = GetAllBorrowers();
        }

        // POST: BorrowerItem/Create
        [HttpPost]
        public ActionResult Create(BorrowerItemViewModel viewModel)
        {
                if (ModelState.IsValid)
                {
                    var borrowersItem = _mappingEngine.Map<BorrowersItem>(viewModel);

                    var itemId = _itemRepository.Get(viewModel.ItemId);

                    borrowersItem.Item = itemId;

                    var borrowerId = _borrowerRepository.Get(viewModel.BorrowerId);
                
                    borrowersItem.Borrower = borrowerId;
                    borrowersItem.DateBorrowed = viewModel.DateBorrowed;
                    borrowersItem.DateReturned = DateTime.Now;
                    
                    _borrowerItemRepository.Save(borrowersItem);
                    return RedirectToAction("Index");
                }
                 SetSelectListToDefaults(viewModel);
                return View();
            
        }

        // GET: BorrowerItem/Edit/5
        public ActionResult Edit(int id)
        {
            var borrowerItem = _borrowerItemRepository.Get(id);
            var borrowerItemViewModel = _mappingEngine.Map<BorrowerItemViewModel>(borrowerItem);
            borrowerItemViewModel.BorrowersSelectListItems = GetAllBorrowers(borrowerItem.Borrower.Id);
            borrowerItemViewModel.ItemSelectListItems = GetAllItems(borrowerItem.Item.Id);
            borrowerItemViewModel.DateReturned=DateTime.Now;

            return View(borrowerItemViewModel);
        }

        // POST: BorrowerItem/Edit/5
        [HttpPost]
        public ActionResult Edit(BorrowerItemViewModel viewModel)
        {
            try
            {
                var borrowersItem = _mappingEngine.Map<BorrowersItem>(viewModel);
                _borrowerItemRepository.Save(borrowersItem);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BorrowerItem/Delete/5
        public ActionResult Delete(int id)
        {
            var borrowersItem = _borrowerItemRepository.Get(id);
            var borrowerItemViewModel = _mappingEngine.Map<BorrowerItemViewModel>(borrowersItem);
            return View(borrowerItemViewModel);
        }

        // POST: BorrowerItem/Delete/5
        [HttpPost]
        public ActionResult Delete(BorrowerItemViewModel viewModel)
        {
            try
            {
                var borrowersItem = _mappingEngine.Map<BorrowersItem>(viewModel);
                _borrowerItemRepository.Delete(borrowersItem);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
