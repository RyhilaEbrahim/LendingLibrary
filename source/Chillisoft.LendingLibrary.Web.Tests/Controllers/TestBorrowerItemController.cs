using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.Tests.Common.Builders;
using Chillisoft.LendingLibrary.Tests.Common.Extensions;
using Chillisoft.LendingLibrary.Web.Controllers;
using Chillisoft.LendingLibrary.Web.Models;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.RandomGenerators;

namespace Chillisoft.LendingLibrary.Web.Tests.Controllers
{
    public class TestBorrowerItemController
    {

        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IBorrowerItemRepository _borrowerItemRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMappingEngine _mappingEngine;
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new BorrowerItemController(Substitute.For<IBorrowerItemRepository>(),Substitute.For<IMappingEngine>(), Substitute.For<IBorrowerRepository>(),Substitute.For<IItemRepository>()));
        }

        [Test]
        public void Index_ShouldReturnViewResult()
        {
            //---------------Set up test pack-------------------
            var borrowerItemController = CreateBuilder().Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerItemController.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void Index_GivenAllUsersReturnedFromRepository_ShouldReturnViewWithMapModel()
        {
            //---------------Set up test pack-------------------
            var borrowersItem = new BorrowerItemBuilder()
                .WithRandomProps()
                .WithDateReturnedAsNull()
                .Build();
            var borrowersItems = new List<BorrowersItem>() { borrowersItem };
            var repository = Substitute.For<IBorrowerItemRepository>();
            repository.GetAll().Returns(borrowersItems);

            var borrowerItemRowViewModels = new List<BorrowerItemRowViewModel> { new BorrowerItemRowViewModel() };

            var mappingEngine =Substitute.For<IMappingEngine>();

            mappingEngine.Map<IEnumerable<BorrowerItemRowViewModel>>(borrowersItems)
                .Returns(borrowerItemRowViewModels);
            var borrowerItemRowViewModel = borrowerItemRowViewModels.FirstOrDefault();
            borrowerItemRowViewModel.Id = borrowersItem.Id;
            borrowerItemRowViewModel.BorrowerId = borrowersItem.BorrowerId;
            borrowerItemRowViewModel.ItemDescription = borrowersItem.Item.Description;
            borrowerItemRowViewModel.DateBorrowed = borrowersItem.DateBorrowed;
            borrowerItemRowViewModel.DateReturned = borrowersItem.DateReturned;


            var controller = CreateBuilder()
                .WithBorrowerItemRepository(repository)
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = controller.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var viewModels = result.Model as IEnumerable<BorrowerItemRowViewModel>;
            CollectionAssert.AreEqual(borrowerItemRowViewModels, viewModels);
        }

        [Test]
        public void Details_ShouldReturnViewResult()
        {
            //---------------Set up test pack-------------------
            var borrowerItemController = CreateBuilder().Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerItemController.Details(1) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_GET_ShouldReturnViewResultWithTitles()
        {
            var borrowerItemRepository = Substitute.For<IBorrowerItemRepository>();
            var borrowerRepository = Substitute.For<IBorrowerRepository>();
            var itemRepository = Substitute.For<IItemRepository>();
            var borrower = BorrowerBuilder.BuildRandom();
            var borrowers = new List<Borrower> { borrower };
            var item = ItemBuilder.BuildRandom();
            var items = new List<Item> { item };

            borrowerRepository.GetAll().Returns(borrowers);
            itemRepository.GetAllItemsNotLent().Returns(items);
            
            var borrowerItemController = CreateBuilder()
                .WithBorrowerItemRepository(borrowerItemRepository)
                .WithBorrowerRepository(borrowerRepository)
                .WithItemRepository(itemRepository)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerItemController.Create() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as BorrowerItemViewModel;
            Assert.IsNotNull(model);

        }

        [Test]
        public void Create_POST_ShouldHaveHttpPostAttribute()
        {
            var borrowerController = new BorrowerItemController(_borrowerItemRepository, _mappingEngine, _borrowerRepository,_itemRepository);
            borrowerController.ShouldHaveAttribute<HttpPostAttribute>(() => borrowerController.Create((BorrowerItemViewModel)_borrowerItemRepository));

        }

        [Test]
        public void Create_POST_ShouldCallSaveAndRedirectToIndex()
        {
            //---------------Set up test pack-------------------
            var borrowerItemRepository = Substitute.For<IBorrowerItemRepository>();
            var borrowerRepository = Substitute.For<IBorrowerRepository>();
            var itemRepository = Substitute.For<IItemRepository>();
            var mapper = Substitute.For<IMappingEngine>();

            var borrowersItem = new BorrowerItemBuilder().WithRandomProps().Build();
            var borrower = new BorrowerBuilder().WithRandomProps().Build();
            var item = new ItemBuilder().WithRandomProps().Build();

            var borrowerItemViewModel = new BorrowerItemViewModel();

            mapper.Map<BorrowersItem>(borrowerItemViewModel).Returns(borrowersItem);

            itemRepository.Get(borrowerItemViewModel.ItemId).Returns(item);

            borrowersItem.ItemId = item.Id;
            borrowersItem.BorrowerId = borrower.Id;
            borrowerRepository.Get(borrowerItemViewModel.BorrowerId).Returns(borrower);

            borrowerItemViewModel.ItemId = item.Id;
            borrowerItemViewModel.BorrowerId = borrower.Id;
            borrowerItemViewModel.DateBorrowed=DateTime.Now;
            borrowerItemViewModel.DateReturned=DateTime.Now.ToString();

           
            
            var borrowerItemController = CreateBuilder()
                   .WithBorrowerItemRepository(borrowerItemRepository)
                   .WithBorrowerRepository(borrowerRepository)
                   .WithItemRepository(itemRepository)
                   .WithMappingEngine(mapper)
                   .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = borrowerItemController.Create(borrowerItemViewModel) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            borrowerItemRepository.Received().Save(borrowersItem);
            Assert.IsNotNull(result);
            var actionName = result.RouteValues["action"];
            Assert.AreEqual("Index", actionName);
        }

        [Test]
        public void EditGET_GivenValidBorrowerItemId_ShouldReturnBorrowerViewModel()
        {
            //---------------Set up test pack-------------------
            var borrowerItems= new BorrowerItemBuilder().WithRandomProps().Build();
            var id = borrowerItems.Id;

            var repository = Substitute.For<IBorrowerItemRepository>();
            var itemRepository = Substitute.For<IItemRepository>();
            var borrowerRepository = Substitute.For<IBorrowerRepository>();
            var mapper = Substitute.For<IMappingEngine>();

            repository.Get(id).Returns(borrowerItems);

            var item = new Item { Id = RandomValueGen.GetRandomInt(), Description = RandomValueGen.GetRandomString() };
            var items = new List<Item> { item };
            var borrower = new BorrowerBuilder().WithRandomProps().Build();
            var borrowers = new List<Borrower> { borrower };


            itemRepository.GetAll().Returns(items);
            borrowerRepository.GetAll().Returns(borrowers);
          

            
            var borrowerItemViewModel = new BorrowerItemViewModel { Id = RandomValueGen.GetRandomInt() };
            
            mapper.Map<BorrowerItemViewModel>(borrowerItems).Returns(borrowerItemViewModel);

            var borrowerItemController = CreateBuilder()
                .WithBorrowerItemRepository(repository)
                .WithBorrowerRepository(borrowerRepository)
                .WithItemRepository(itemRepository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerItemController.Edit(id) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as BorrowerItemViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.BorrowersSelectListItems.Count);
            Assert.AreEqual(1, model.ItemSelectListItems.Count);
        }

        [Test]
        public void EditPOST_GivenValidBorrowerId_ShouldCallSaveandRedirect()

        {
            var borrowerItems = new BorrowerItemBuilder().WithRandomProps().Build();
            
            var repository = Substitute.For<IBorrowerItemRepository>();
            var itemRepository = Substitute.For<IItemRepository>();
            var borrowerRepository = Substitute.For<IBorrowerRepository>();
            var mapper = Substitute.For<IMappingEngine>();
            
            var borrowerItemViewModel = new BorrowerItemViewModel
            {
                Id = RandomValueGen.GetRandomInt(),
                ItemId = RandomValueGen.GetRandomInt(),
                BorrowerId = RandomValueGen.GetRandomInt()
            };

            mapper.Map<BorrowersItem>(borrowerItemViewModel).Returns(borrowerItems);
            repository.Save(borrowerItems);
            var borrowerItemController = CreateBuilder()
                .WithBorrowerItemRepository(repository)
                .WithBorrowerRepository(borrowerRepository)
                .WithItemRepository(itemRepository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = borrowerItemController.Edit(borrowerItemViewModel) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            repository.Received().Save(borrowerItems);
            var actionName = result.RouteValues["action"];
            Assert.AreEqual("Index", actionName);

        }

        [Test]
        public void DeleteGET_GivenBorrowerItemViewModel_ShouldMapToBorrowerAndDelete()

        {
            var borrowerItems = new BorrowerItemBuilder().WithRandomProps().Build();
            
            var repository = Substitute.For<IBorrowerItemRepository>();
            var itemRepository = Substitute.For<IItemRepository>();
            var borrowerRepository = Substitute.For<IBorrowerRepository>();
            var mapper = Substitute.For<IMappingEngine>();
            
            var borrowerItemViewModel = new BorrowerItemViewModel
            {
                Id = RandomValueGen.GetRandomInt(),
                ItemId = RandomValueGen.GetRandomInt(),
                BorrowerId = RandomValueGen.GetRandomInt()
            };

            mapper.Map<BorrowerItemViewModel>(borrowerItems).Returns(borrowerItemViewModel);
            repository.Get(borrowerItems.Id);
            var borrowerItemController = CreateBuilder()
                .WithBorrowerItemRepository(repository)
                .WithBorrowerRepository(borrowerRepository)
                .WithItemRepository(itemRepository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = borrowerItemController.Delete(borrowerItems.Id) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            repository.Received().Get(borrowerItems.Id);
          

        }
        [Test]
        public void DeletePOST_GivenBorrowerItemViewModel_ShouldMapToBorrowerAndDelete()

        {
            var borrowerItems = new BorrowerItemBuilder().WithRandomProps().Build();
            
            var repository = Substitute.For<IBorrowerItemRepository>();
            var itemRepository = Substitute.For<IItemRepository>();
            var borrowerRepository = Substitute.For<IBorrowerRepository>();
            var mapper = Substitute.For<IMappingEngine>();
            
            var borrowerItemViewModel = new BorrowerItemViewModel
            {
                Id = RandomValueGen.GetRandomInt(),
                ItemId = RandomValueGen.GetRandomInt(),
                BorrowerId = RandomValueGen.GetRandomInt()
            };

            mapper.Map<BorrowersItem>(borrowerItemViewModel).Returns(borrowerItems);
            repository.Delete(borrowerItems);
            var borrowerItemController = CreateBuilder()
                .WithBorrowerItemRepository(repository)
                .WithBorrowerRepository(borrowerRepository)
                .WithItemRepository(itemRepository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = borrowerItemController.Delete(borrowerItemViewModel);
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            repository.Received().Delete(borrowerItems);
          

        }
        
        private BorrowerItemControllerBuilder CreateBuilder()
        {
            return new BorrowerItemControllerBuilder();
        }

        public class BorrowerItemControllerBuilder
        {
            private IBorrowerItemRepository _borrowerItemRepository = Substitute.For<IBorrowerItemRepository>();
            private IBorrowerRepository _borrowerRepository = Substitute.For<IBorrowerRepository>();
            private IItemRepository _itemRepository = Substitute.For<IItemRepository>();
            private IMappingEngine _mappingEngine = Substitute.For<IMappingEngine>();

            public BorrowerItemControllerBuilder WithBorrowerItemRepository(IBorrowerItemRepository borrowerItemRepository)
            {
                _borrowerItemRepository = borrowerItemRepository;
                return this;
            }
            public BorrowerItemControllerBuilder WithMappingEngine(IMappingEngine mappingEngine)
            {
                _mappingEngine = mappingEngine;
                return this;
            }
            public BorrowerItemControllerBuilder WithBorrowerRepository(IBorrowerRepository borrowerRepository)
            {
                _borrowerRepository = borrowerRepository;
                return this;
            }
            public BorrowerItemControllerBuilder WithItemRepository(IItemRepository itemRepository)
            {
                _itemRepository = itemRepository;
                return this;
            }


            public BorrowerItemController Build()
            {
                return new BorrowerItemController(_borrowerItemRepository, _mappingEngine,_borrowerRepository, _itemRepository);
            }
        }
    }
}