using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
    public class TestBorrowerController
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IMappingEngine _mappingEngine;
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new BorrowerController(_borrowerRepository,_mappingEngine));
        }

        [Test]
        public void Index_ShouldReturnViewResult()
        {
            //---------------Set up test pack-------------------
            var borrowerController = CreateBuilder().Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void Index_GivenAllUsersReturnedFromRepository_ShouldReturnViewWithMapModel()
        {
            //---------------Set up test pack-------------------
            var borrowers = new List<Borrower>
            {
                new BorrowerBuilder().WithRandomProps().Build(),
            };
            var repository = Substitute.For<IBorrowerRepository>();
            repository.GetAll().Returns(borrowers);

            var mapper = Substitute.For<IMappingEngine>();
            var borrowerViewModels = new List<BorrowerViewModel>() { new BorrowerViewModel() };
            mapper.Map<IEnumerable<BorrowerViewModel>>(borrowers).Returns(borrowerViewModels);

            var borrowerController = CreateBuilder()
                .WithBorrowerRepository(repository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as IEnumerable<BorrowerViewModel>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count());

        }

        [Test]
        public void Details_ShouldReturnViewResult()
        {
            //---------------Set up test pack-------------------
            var borrowerController = CreateBuilder().Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Details(1) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }


        [Test]
        public void Details_GivenValidUserId_ShouldReturnBorrowersDetails()
        {
            //---------------Set up test pack-------------------

            var borrower = new BorrowerBuilder().WithRandomProps().Build();
            var id = borrower.Id;

            var repository = Substitute.For<IBorrowerRepository>();
            repository.Get(id).Returns(borrower);

            var mapper = Substitute.For<IMappingEngine>();
            var borrowerViewModel = new BorrowerViewModel { Id = RandomValueGen.GetRandomInt() };
            mapper.Map<BorrowerViewModel>(borrower).Returns(borrowerViewModel);

            var borrowerController = CreateBuilder()
                .WithBorrowerRepository(repository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = borrowerController.Details(id) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as BorrowerViewModel;
            Assert.IsNotNull(model);
        }

        [Test]
        public void Create_GET_ShouldReturnViewResultWithTitles()
        {
            var repository = Substitute.For<IBorrowerRepository>();
            var title = new Title { Id=RandomValueGen.GetRandomInt(), Description = RandomValueGen.GetRandomString()};
            var titles = new List<Title> {title};

            repository.GetAllTitles().Returns(titles);

            var borrowerController = CreateBuilder()
                .WithBorrowerRepository(repository)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Create() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as BorrowerViewModel;
            Assert.IsNotNull(model);
            Assert.IsInstanceOf<List<SelectListItem>>(model.TitlesSelectList);
            Assert.AreEqual(1, model.TitlesSelectList.Count);
            var selectListItem = model.TitlesSelectList.First();
            Assert.AreEqual(title.Id.ToString(), selectListItem.Value);
            Assert.AreEqual(title.Description, selectListItem.Text);
        }

        [Test]
        public void Create_POST_ShouldHaveHttpPostAttribute()
        {
            var borrowerController = new BorrowerController(_borrowerRepository,_mappingEngine);
            borrowerController.ShouldHaveAttribute<HttpPostAttribute>(() => borrowerController.Create((BorrowerViewModel)null,null));

        }

        [Test]
        public void Create_POST_ShouldCallSaveAndRedirectToIndex()
        {
            var borrower = new BorrowerBuilder()
                .WithRandomProps()
                .Build();
           
            var file = Substitute.For<HttpPostedFileBase>();
            file.FileName.Returns("somefileName");
            file.ContentLength.Returns(Int32.MaxValue);
            file.ContentType.Returns(String.Empty);
            file.InputStream.Returns(Stream.Null);
            
            var repository = Substitute.For<IBorrowerRepository>();
            var title = new TitleBuilder().WithRandomProps().Build();
            borrower.TitleId = title.Id;
            repository.GetTitleById(borrower.TitleId).Returns(title);

            var mapper = Substitute.For<IMappingEngine>();
            var borrowerViewModel = new BorrowerViewModel ();

            mapper.Map<Borrower>(borrowerViewModel).Returns(borrower);
            borrowerViewModel.Id = borrower.Id;
            borrowerViewModel.ContactNumber = borrower.ContactNumber;
            borrowerViewModel.Email = borrower.Email;
            borrowerViewModel.FirstName = borrower.FirstName;
            borrowerViewModel.Surname = borrower.Surname;
            borrowerViewModel.Photo = borrower.Photo;
            borrowerViewModel.TitleId = borrower.TitleId;

            var borrowerController = CreateBuilder()
                .WithBorrowerRepository(repository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = borrowerController.Create(borrowerViewModel, file) as RedirectToRouteResult;

            //---------------Test Result -----------------------
            Assert.AreSame(title, borrower.Title);
            repository.Received().Save(borrower);

            Assert.IsNotNull(result);
            var actionName = result.RouteValues["action"];
            Assert.AreEqual("Index", actionName);
        }

        [Test]
        public void Edit_GivenValidBorrowerId_ShouldReturnBorrowerViewModelWithTitles()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerBuilder().WithRandomProps().Build();
            var id = borrower.Id;

            var repository = Substitute.For<IBorrowerRepository>();
            repository.Get(id).Returns(borrower);
            var title = new Title { Id = RandomValueGen.GetRandomInt(), Description = RandomValueGen.GetRandomString() };
            var titles = new List<Title> { title };
            repository.GetAllTitles().Returns(titles);

            var mapper = Substitute.For<IMappingEngine>();
            var borrowerViewModel = new BorrowerViewModel { Id = RandomValueGen.GetRandomInt() };

         
            mapper.Map<BorrowerViewModel>(borrower).Returns(borrowerViewModel);
            var borrowerController = CreateBuilder()
                .WithBorrowerRepository(repository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Edit(id) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as BorrowerViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(1,model.TitlesSelectList.Count);
        }

        [Test]
        public void Edit_GivenValidBorrowerId_ShouldReturnBorrowerViewModelWithTitlesSelected()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerBuilder().WithRandomProps().Build();
            var id = borrower.Id;

            var repository = Substitute.For<IBorrowerRepository>();
            repository.Get(id).Returns(borrower);

            var title1 = TitleBuilder.BuildRandom();
            var title2 = TitleBuilder.BuildRandom();
            var title3 = TitleBuilder.BuildRandom();
            var titles = new List<Title> { title1, title2, title3 };

            repository.GetAllTitles().Returns(titles);

            var mapper = Substitute.For<IMappingEngine>();

            var borrowerViewModel = new BorrowerViewModel { Id = RandomValueGen.GetRandomInt() ,TitleId = title2.Id};         
            mapper.Map<BorrowerViewModel>(borrower).Returns(borrowerViewModel);

            var borrowerController = CreateBuilder()
                .WithBorrowerRepository(repository)
                .WithMappingEngine(mapper)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Edit(id) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as BorrowerViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(3,model.TitlesSelectList.Count);

            var selectListItem2 = model.TitlesSelectList[1];
            Assert.AreEqual(title2.Id.ToString(), selectListItem2.Value);
            Assert.AreEqual(title2.Description, selectListItem2.Text);
            Assert.IsTrue(selectListItem2.Selected);

        }

        [Test]
        public void Delete_GivenBorrowerViewModel_ShouldMapToBorrowerAndDelete()
        {
            //---------------Set up test pack-------------------
            var borrowerViewModel = new BorrowerViewModel();
            var borrower = BorrowerBuilder.BuildRandom();
            var mappingEngine = Substitute.For<IMappingEngine>();
            mappingEngine.Map<Borrower>(borrowerViewModel).Returns(borrower);

            var borrowerRepository = Substitute.For<IBorrowerRepository>();
            var borrowerController = CreateBuilder()
                .WithMappingEngine(mappingEngine)
                .WithBorrowerRepository(borrowerRepository)
                .Build();

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Delete(borrowerViewModel);
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            borrowerRepository.Received(1).Delete(borrower);
            
        }

        [Test]
        public void Delete_GivenBorrowerViewModel_ShouldRedirectToIndexAfterDelete()
        {
            //---------------Set up test pack-------------------
            var borrowerViewModel = new BorrowerViewModel();
            var borrowerController = CreateBuilder()
               .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = borrowerController.Delete(borrowerViewModel) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            Assert.AreEqual("Index",result.RouteValues["action"]);
        }

        [Test]
        public void Delete_GivenExceptionWhenRepositoryDeleteIsCalled_ShouldReturnView()
        {
            //---------------Set up test pack-------------------
            var borrowerViewModel = new BorrowerViewModel();

            var borrowerRepository = Substitute.For<IBorrowerRepository>();
            borrowerRepository.When(repository => repository.Delete(Arg.Any<Borrower>()))
                .Throw<ApplicationException>();

            var borrowerController = CreateBuilder()
                .WithBorrowerRepository(borrowerRepository)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var result = borrowerController.Delete(borrowerViewModel) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
       }

        private BorrowerControllerBuilder CreateBuilder()
        {
            return new BorrowerControllerBuilder();
        }

        public class BorrowerControllerBuilder
        {
            private IBorrowerRepository _borrowerRepository = Substitute.For<IBorrowerRepository>();
            private IMappingEngine _mappingEngine = Substitute.For<IMappingEngine>();

            public BorrowerControllerBuilder WithBorrowerRepository(IBorrowerRepository borrowerRepository)
            {
                _borrowerRepository = borrowerRepository;
                return this;
            }
            public BorrowerControllerBuilder WithMappingEngine(IMappingEngine mappingEngine)
            {
                _mappingEngine = mappingEngine;
                return this;
            }


            public BorrowerController Build()
            {
                return new BorrowerController(_borrowerRepository, _mappingEngine);
            }
        }
    }
}