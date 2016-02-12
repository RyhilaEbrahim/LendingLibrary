using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.Tests.Common.Builders;
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
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new BorrowerController());
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
            var borrowerViewModels = new List<BorrowerViewModel>() { new BorrowerViewModel()};
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
//
//        [Test]
//        public void Index_GivenUserId_ShouldReturnBorrowersDetails()
//        {
//            //---------------Set up test pack-------------------
//            var borrowers = new List<Borrower>
//            {
//                new BorrowerBuilder().WithRandomProps().Build(),
//            };
//            var repository = Substitute.For<IBorrowerRepository>();
//            repository.GetAll().Returns(borrowers);
//
//            var mapper = Substitute.For<IMappingEngine>();
//            var borrowerViewModels = new List<BorrowerViewModel>() { new BorrowerViewModel()};
//            mapper.Map<BorrowerViewModel>(borrowers);
//
//            var borrowerController = CreateBuilder()
//                .WithBorrowerRepository(repository)
//                .WithMappingEngine(mapper)
//                .Build();
//            //---------------Assert Precondition----------------
//            //---------------Execute Test ----------------------
//            var result = borrowerController.Details(5) as ViewResult;
//            //---------------Test Result -----------------------
//            Assert.IsNotNull(result);
//            var model = result.Model as BorrowerViewModel;
//            Assert.IsNotNull(model);
//             }
//        
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

        public BorrowerControllerBuilder CreateBuilder()
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
            public BorrowerControllerBuilder WithMappingEngine(IMappingEngine  mappingEngine)
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