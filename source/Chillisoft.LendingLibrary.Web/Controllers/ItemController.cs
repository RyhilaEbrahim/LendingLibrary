using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMappingEngine _mappingEngine;


        // GET: BorrowerViewModel
        public ItemController(IItemRepository itemRepository, IMappingEngine mappingEngine)
        {
            _itemRepository = itemRepository;
            _mappingEngine = mappingEngine;
        }
        
        // GET: Item
        public ActionResult Index()
        {
            var items = _itemRepository.GetAll();
            var itemViewModels = _mappingEngine.Map<IEnumerable<ItemViewModel>>(items);
            return View("Index", itemViewModels);
          
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            var item = _itemRepository.Get(id);
            var itemViewModel = _mappingEngine.Map<ItemViewModel>(item);
            return View(itemViewModel);
        }

        [Authorize]
        // GET: Item/Create
        public ActionResult Create()
        {
            var viewModel = new ItemViewModel();
            return View(viewModel);
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(ItemViewModel viewModel)
        {
            var borrower = _mappingEngine.Map<Item>(viewModel);
           _itemRepository.Save(borrower);
            return RedirectToAction("Index");
        }

        // GET: Item/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {

            var item = _itemRepository.Get(id);

            var itemViewModel = _mappingEngine.Map<ItemViewModel>(item);
          
            return View(itemViewModel);
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Edit(ItemViewModel viewModel)
        {
            try
            {
                var item = _mappingEngine.Map<Item>(viewModel);
                _itemRepository.Save(item);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            var item = _itemRepository.Get(id);
            var itemViewModel = _mappingEngine.Map<ItemViewModel>(item);
            return View(itemViewModel);
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(ItemViewModel viewModel)
        {
            try
            {
                var item = _mappingEngine.Map<Item>(viewModel);
                _itemRepository.Delete(item);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
