using System;
using System.Collections.Generic;
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
    [TestFixture]
    public class TestItemController
    {
        private readonly IItemRepository itemRepository;
        private readonly IMappingEngine _mappingEngine;
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() =>new ItemController(itemRepository, _mappingEngine));
        }

        [Test]
        public void Index_ShouldReturnViewResult()
        {
            //---------------Set up test pack-------------------
            var itemController = CreateBuilder().Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = itemController.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void Index_GivenAllItemsReturnedFromRepository_ShouldReturnViewWithMapModel()
        {
            //---------------Set up test pack-------------------
            var items = new List<Item>
            {
                ItemBuilder.BuildRandom(),
                ItemBuilder.BuildRandom(),
                ItemBuilder.BuildRandom(),
            };
            var repository = Substitute.For<IItemRepository>();
            repository.GetAll().Returns(items);
            var itemViewModels = new List<ItemViewModel> {new ItemViewModel()};
            var mappingEngine = Substitute.For<IMappingEngine>();
            mappingEngine.Map<IEnumerable<ItemViewModel>>(items).Returns(itemViewModels);
            var controller = CreateBuilder()
                .WithItemRepository(repository)
                .WithMappingEngine(mappingEngine)
                .Build();

            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = controller.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result,"Expected not null ViewResult");
            Assert.AreSame(itemViewModels,result.Model);
        }

        [Test]
        public void Details_GivenItemId_ShouldReturnViewWithMapModel()
        {
            //---------------Set up test pack-------------------
            var item = new ItemBuilder().WithRandomProps().Build();
            var id = item.Id;

            var repository = Substitute.For<IItemRepository>();
   
            repository.Get(id).Returns(item);
            var mappingEngine = Substitute.For<IMappingEngine>();
            var itemViewModels = new ItemViewModel {Id= RandomValueGen.GetRandomInt()};
            mappingEngine.Map< ItemViewModel > (item).Returns(itemViewModels);
           var controller = CreateBuilder()
                .WithItemRepository(repository)
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = controller.Details(id)as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as ItemViewModel;
            Assert.IsNotNull(model);
        }

        [Test]
        public void Create_GET_ShouldReturnViewResults()
        {
          
            var controller = CreateBuilder()
             .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = controller.Create() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
          }

        [Test]
        public void Create_POST_ShouldHaveHttpPostAttribute()
        {
            var repository = Substitute.For<IItemRepository>();
            var itemController = new ItemController(repository, _mappingEngine);
            itemController.ShouldHaveAttribute<HttpPostAttribute>(() => itemController.Create(new ItemViewModel()));

        }

        [Test]
        public void Create_POST_ShouldCallSaveAndRedirectToIndex()
        {
            //---------------Set up test pack-------------------
            var items = new ItemBuilder().WithRandomProps().Build();
            var repository = Substitute.For<IItemRepository>();
            var mappingEngine = Substitute.For<IMappingEngine>();
            var itemViewModel = new ItemViewModel();
            mappingEngine.Map<Item>(itemViewModel).Returns(items);
            var itemController = CreateBuilder().WithItemRepository(repository).WithMappingEngine(mappingEngine).Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = itemController.Create(itemViewModel) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            repository.Received().Save(items);
            Assert.IsNotNull(result);
            var actionName = result.RouteValues["action"];
            Assert.AreEqual("Index", actionName);
        }

        [Test]
        public void Edit_GivenValidItemId_ShouldReturnItemViewModel()
        {
            //---------------Set up test pack-------------------
            var items = new ItemBuilder().WithRandomProps().Build();
            var Id = items.Id;
           var repository = Substitute.For<IItemRepository>();
            var mappingEngine = Substitute.For<IMappingEngine>();
            var itemViewModel = new ItemViewModel {Id = RandomValueGen.GetRandomInt()};
            mappingEngine.Map<Item>(itemViewModel).Returns(items);
            var itemController = CreateBuilder().WithItemRepository(repository).WithMappingEngine(mappingEngine).Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = itemController.Edit(Id) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
           
        }
        
        [Test]
        public void Delete_GivenBorrowerViewModel_ShouldRedirectToIndexAfterDelete()
        {
            //---------------Set up test pack-------------------
            var itemViewModel = new ItemViewModel();
            var itemController = CreateBuilder()
               .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = itemController.Delete(itemViewModel) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Delete_GivenExceptionWhenRepositoryDeleteIsCalled_ShouldReturnView()
        {
            //---------------Set up test pack-------------------
            var itemViewModel = new ItemViewModel();

            var itemRepository = Substitute.For<IItemRepository>();
            itemRepository.When(repository => repository.Delete(Arg.Any<Item>()))
                .Throw<ApplicationException>();

            var borrowerController = CreateBuilder()
                .WithItemRepository(itemRepository)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = borrowerController.Delete(itemViewModel) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        private ItemControllerBuilder CreateBuilder()
        {
            return new ItemControllerBuilder();
        }

        public class ItemControllerBuilder
        {
            private IItemRepository _itemRepository = Substitute.For<IItemRepository>();
            private IMappingEngine _mappingEngine = Substitute.For<IMappingEngine>();

            public ItemControllerBuilder WithItemRepository(IItemRepository itemRepository)
            {
                _itemRepository = itemRepository;
                return this;
            }
            public ItemControllerBuilder WithMappingEngine(IMappingEngine mappingEngine)
            {
                _mappingEngine = mappingEngine;
                return this;
            }


            public ItemController Build()
            {
                return new ItemController(_itemRepository, _mappingEngine);
            }
        }
    }
}