using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using Chillisoft.LendingLibrary.DB.Repositories;
using Chillisoft.LendingLibrary.Tests.Common.Builders;
using NSubstitute;
using NUnit.Framework;

namespace Chillisoft.LendingLibrary.DB.Tests.Repositories
{
    [TestFixture]
    public class TestBorrowerRepository
    {
        [Test]
        public void Construct()
        {
           Assert.DoesNotThrow(() => new BorrowerRepository(NSubstitute.Substitute.For<ILendingLibraryDbContext>()));
        }

        [Test]
        public void Construct_GivenLendingLibraryDbContextIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var exception = Assert.Throws<ArgumentNullException>(() => new BorrowerRepository(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("lendingLibraryDbContext",exception.ParamName);
        }

        [Test]
        public void Get_GivenBorrowerExistsForId_ShouldReturnBorrower()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerBuilder()
                .WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder()
                .WithBorrowers(borrower)
                .Build();
            
            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var actualBorrower = repository.Get(borrower.Id);
            //---------------Test Result -----------------------
            Assert.AreSame(borrower, actualBorrower);
        }

        [Test]
        public void Get_GivenBorrowerDoesNotExistForId_ShouldReturnBorrower()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerBuilder()
                .WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder()
                .WithBorrowers(borrower)
                .Build();
            
            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var actualBorrower = repository.Get(borrower.Id+1);
            //---------------Test Result -----------------------
            Assert.IsNull(actualBorrower);
        }

        [Test]
        public void GetAll_GivenOneBorrower_ShouldReturnBorrower()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerBuilder()
                .WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder()
                .WithBorrowers(borrower)
                .Build();

            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var borrowers = repository.GetAll();
            //---------------Test Result -----------------------
            Assert.AreEqual(1,borrowers.Count);
            var actual = borrowers.First();
            Assert.AreSame(borrower,actual);
        }

        [Test]
        public void GetAll_GivenTwoBorrowers_ShouldReturnBorrowers()
        {
            //---------------Set up test pack-------------------
            var borrower1 = new BorrowerBuilder()
                .WithRandomProps().Build();
            var borrower2 = new BorrowerBuilder()
                .WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder()
                .WithBorrowers(borrower1, borrower2)
                .Build();

            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var borrowers = repository.GetAll();
            //---------------Test Result -----------------------
            Assert.AreEqual(2,borrowers.Count);
            var actualFirst = borrowers.First();
            Assert.AreSame(borrower1,actualFirst);
            var actualLast = borrowers.Last();
            Assert.AreSame(borrower2, actualLast);
        }

        [Test]
        public void GetAll_GivenManyBorrowers_ShouldReturnBorrowers()
        {
            //---------------Set up test pack-------------------
            var borrower1 = new BorrowerBuilder()
                .WithRandomProps().Build();
            var borrower2 = new BorrowerBuilder()
                .WithRandomProps().Build();
            var borrower3 = new BorrowerBuilder()
                .WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder()
                .WithBorrowers(borrower1, borrower2, borrower3)
                .Build();

            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var borrowers = repository.GetAll();
            //---------------Test Result -----------------------
            Assert.AreEqual(3,borrowers.Count);
            var actualFirst = borrowers.First();
            Assert.AreSame(borrower1,actualFirst);
            var actualLast = borrowers.Last();
            Assert.AreSame(borrower3, actualLast);
        }


    }
}