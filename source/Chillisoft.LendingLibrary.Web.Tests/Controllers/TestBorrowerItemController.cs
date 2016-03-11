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
            var borrowersItems = new List<BorrowersItem>
            {
                new BorrowerItemBuilder().WithRandomProps().Build(),
            };
            var repository = Substitute.For<IBorrowerItemRepository>();
            repository.GetAll().Returns(borrowersItems);

            var mapper = Substitute.For<IMappingEngine>();
            var borrowerItemRowViewModels = new List<BorrowerItemRowViewModel>() { new BorrowerItemRowViewModel() };
            mapper.Map<IEnumerable<BorrowerItemRowViewModel>>(borrowersItems).Returns(borrowerItemRowViewModels);

            var borrowerController = CreateBuilder()
                .WithBorrowerItemRepository(repository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as IEnumerable<BorrowerItemRowViewModel>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count());
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
            itemRepository.GetAll().Returns(items);
            
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

//        [Test]
//        public void Create_POST_ShouldCallSaveAndRedirectToIndex()
//        {
//            //---------------Set up test pack-------------------
//            var borrowerItemRepository = Substitute.For<IBorrowerItemRepository>();
//            var borrowerRepository = Substitute.For<IBorrowerRepository>();
//            var itemRepository = Substitute.For<IItemRepository>();
//            var mapper = Substitute.For<IMappingEngine>();
//
//            var borrowersItem = BorrowerItemBuilder.BuildRandom();
//            var borrower = BorrowerBuilder.BuildRandom();
//            var item = ItemBuilder.BuildRandom();
//            var borrowerItemViewModel = new BorrowerItemViewModel();
//            borrowersItem.ItemId = item.Id;
//            borrowersItem.BorrowerId = borrower.Id;
//
//            mapper.Map<BorrowersItem>(borrowerItemViewModel).Returns(borrowersItem);
//
//            borrowerItemViewModel.ItemId = item.Id;
//            borrowerItemViewModel.BorrowerId = borrower.Id;
//            borrowerItemViewModel.DateBorrowed=DateTime.Now;
//            borrowerItemViewModel.DateReturned=DateTime.Now;
//
//            itemRepository.Get(borrowerItemViewModel.ItemId).Returns(item);
//            borrowerRepository.Get(borrowerItemViewModel.BorrowerId).Returns(borrower);
//            
//            var borrowerItemController = CreateBuilder()
//                   .WithBorrowerItemRepository(borrowerItemRepository)
//                   .WithBorrowerRepository(borrowerRepository)
//                   .WithItemRepository(itemRepository)
//                   .Build();
//            //---------------Assert Precondition----------------
//            //---------------Execute Test ----------------------
//            var result = borrowerItemController.Create(borrowerItemViewModel) as RedirectToRouteResult;
//            //---------------Test Result -----------------------
//            Assert.AreSame(borrower, borrower.Id);
//            Assert.AreSame(item, item.Id);
//            borrowerItemRepository.Received().Save(borrowersItem);
//            Assert.IsNotNull(result);
//            var actionName = result.RouteValues["action"];
//            Assert.AreEqual("Index", actionName);
//        }
        

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