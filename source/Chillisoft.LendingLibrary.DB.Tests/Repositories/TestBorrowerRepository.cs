using System;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
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
            Assert.AreEqual("lendingLibraryDbContext", exception.ParamName);
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
            var actualBorrower = repository.Get(borrower.Id + 1);
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
            Assert.AreEqual(1, borrowers.Count);
            var actual = borrowers.First();
            Assert.AreSame(borrower, actual);
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
            Assert.AreEqual(2, borrowers.Count);
            var actualFirst = borrowers.First();
            Assert.AreSame(borrower1, actualFirst);
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
            Assert.AreEqual(3, borrowers.Count);
            var actualFirst = borrowers.First();
            Assert.AreSame(borrower1, actualFirst);
            var actualLast = borrowers.Last();
            Assert.AreSame(borrower3, actualLast);
        }

        [Test]
        public void GetAllTitles_GivenOneTitle_ShouldReturnTitle()
        {
            //---------------Set up test pack-------------------
            var title1 = new TitleBuilder()
                .WithRandomProps().Build();


            var dbContext = new TestDbContextBuilder()
                .WithTitles(title1)
                .Build();

            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var titles = repository.GetAllTitles();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, titles.Count);
            var actualFirst = titles.First();
            Assert.AreSame(title1, actualFirst);

        }

        [Test]
        public void GetAllTitles_GivenTwoTitle_ShouldReturnTitle()
        {
            //---------------Set up test pack-------------------
            var title = new TitleBuilder()
                .WithRandomProps().Build();
            var title2 = new TitleBuilder()
               .WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder()
                .WithTitles(title, title2)
                .Build();

            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var titles = repository.GetAllTitles();
            //---------------Test Result -----------------------
            Assert.AreEqual(2, titles.Count);
            var actualFirst = titles.First();
            Assert.AreSame(title, actualFirst);
            var actualLast = titles.Last();
            Assert.AreSame(title2, actualLast);

        }

        [Test]
        public void GetAllTitles_GivenManyTitle_ShouldReturnTitle()
        {
            //---------------Set up test pack-------------------
            var title1 = new TitleBuilder()
                .WithRandomProps().Build();
            var title2 = new TitleBuilder()
               .WithRandomProps().Build();
            var title3 = new TitleBuilder()
              .WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder()
                .WithTitles(title1, title2, title3)
                .Build();

            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var titles = repository.GetAllTitles();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, titles.Count);
            var actualFirst = titles.First();
            Assert.AreSame(title1, actualFirst);
            var actualLast = titles.Last();
            Assert.AreSame(title3, actualLast);
        }

        [Test]
        public void GetTitleByID_GivenSingleTitleWithMatchingId_ShouldReturnTitle()
        {
            //---------------Set up test pack-------------------
            var title = new TitleBuilder()
                .WithRandomProps().Build();

            var dbContext = new TestDbContextBuilder()
                .WithTitles(title)
                .Build();

            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var actualTitle = repository.GetTitleById(title.Id);
            //---------------Test Result -----------------------
            Assert.IsNotNull(actualTitle);
            Assert.AreSame(title, actualTitle);

        }

        [Test]
        public void GetTitleByID_GivenSingleTitleWithNoMatchingId_ShouldReturnNull()
        {
            //---------------Set up test pack-------------------
            var title = new TitleBuilder()
                .WithRandomProps().Build();

            var dbContext = new TestDbContextBuilder()
                .WithTitles(title)
                .Build();

            var repository = new BorrowerRepository(dbContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var actualTitle = repository.GetTitleById(title.Id + 1);
            //---------------Test Result -----------------------
            Assert.IsNull(actualTitle);
        }

        [Test]
        public void Delete_GivenBorrowerExists_ShouldDeleteBorrowerAndCallSavedChanges()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerBuilder()
                   .WithRandomProps().Build();

            var dbContext = new TestDbContextBuilder()
                .WithBorrowers(borrower)
                .Build();

            var repository = CreateBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repository.Delete(borrower);
            //---------------Test Result -----------------------
            Assert.AreEqual(0, dbContext.Borrowers.Count());
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void Save_GivenNewBorrower_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerBuilder()
                   .WithRandomProps()
                   .WithNewId()
                   .Build();

            var dbContext = new TestDbContextBuilder().Build();

            var repository = CreateBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repository.Save(borrower);
            //---------------Test Result -----------------------
            dbContext.Received().AttachEntity(borrower);
            dbContext.Received().SaveChanges();
        }

        public BorrowerRepositoryBuilder CreateBuilder()
        {
            return new BorrowerRepositoryBuilder();
        }

        public class BorrowerRepositoryBuilder
        {
            private ILendingLibraryDbContext _lendingLibraryDbContext = Substitute.For<ILendingLibraryDbContext>();
            public BorrowerRepositoryBuilder WithDbContext(ILendingLibraryDbContext lendingLibraryDbContext)
            {
                _lendingLibraryDbContext = lendingLibraryDbContext;
                return this;
            }

            public IBorrowerRepository Build()
            {
                return new BorrowerRepository(_lendingLibraryDbContext);
            }
        }

    }
}