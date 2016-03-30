using System;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.DB.Repositories;
using Chillisoft.LendingLibrary.Tests.Common.Builders;
using NSubstitute;
using NUnit.Framework;

namespace Chillisoft.LendingLibrary.DB.Tests.Repositories
{
    public class TestBorrowerItemRepository
    {

        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new BorrowerItemRepository(Substitute.For<ILendingLibraryDbContext>()));
        }

        [Test]
        public void Construct_GivenLendingLibraryDbContextIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var exception = Assert.Throws<ArgumentNullException>(() => new BorrowerItemRepository(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("lendingLibraryDbContext", exception.ParamName);
        }

        [Test]
        public void Get_GivenBorrowerDoesNotExistForId_ShouldReturnBorrowerItem()
        {
            //---------------Set up test pack-------------------
            var borrowerItem = new BorrowerItemBuilder()
                .WithRandomProps().Build();
           
            var dbContext = new TestDbContextBuilder()
                .WithBorrowerItem(borrowerItem)
                .Build();

            var repository =  CreateBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var actualBorrower = repository.Get(borrowerItem.Id+10);
            //---------------Test Result -----------------------
            Assert.IsNull(actualBorrower);
        }

        [Test]
        public void GetAll_GivenOneBorrowerItems_ShouldReturnBorrower()
        {
            //---------------Set up test pack-------------------
            var borrowerItem = new 
                BorrowerItemBuilder()
                .WithRandomProps().Build();
           
            var dbContext = new TestDbContextBuilder()
                .WithBorrowerItem(borrowerItem)
                .Build();

            var repository =  CreateBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var actualBorrower = repository.GetAll();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, actualBorrower.Count);
            var actual = actualBorrower.First();
            Assert.AreSame(borrowerItem, actual);
        }

    [Test]
        public void GetAll_GivenTwoBorrowerItems_ShouldReturnBorrower()
        {
            //---------------Set up test pack-------------------
            var borrowerItem1 = new BorrowerItemBuilder()
                .WithRandomProps().Build();
            var borrowerItem2 = new BorrowerItemBuilder()
                .WithRandomProps().Build();
           
            var dbContext = new TestDbContextBuilder()
                .WithBorrowerItem(borrowerItem1, borrowerItem2)
                .Build();

            var repository =  CreateBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var actualBorrower = repository.GetAll();
            //---------------Test Result -----------------------
            Assert.AreEqual(2, actualBorrower.Count);
            
        }

    [Test]
        public void GetAll_GivenThreeBorrowerItems_ShouldReturnBorrower()
        {
            //---------------Set up test pack-------------------
            var borrowerItem1 = new BorrowerItemBuilder()
                 .WithRandomProps().Build();
            var borrowerItem2 = new BorrowerItemBuilder()
                .WithRandomProps().Build();
            var borrowerItem3 = new BorrowerItemBuilder()
                .WithRandomProps().Build();

            var dbContext = new TestDbContextBuilder()
            .WithBorrowerItem(borrowerItem1, borrowerItem2,borrowerItem3)
                .Build();

            var repository =  CreateBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var actualBorrower = repository.GetAll();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, actualBorrower.Count);
          
        }

        [Test]
        public void Save_GivenNewBorrower_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerItemBuilder()
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

        [Test]
        public void Delete_GivenBorrowerItemExists_ShouldDeleteBorrowerAndCallSavedChanges()
        {
            //---------------Set up test pack-------------------
            var borrowerItem = new BorrowerItemBuilder()
                   .WithRandomProps().Build();

            var dbContext = new TestDbContextBuilder()
                .WithBorrowerItem(borrowerItem)
                .Build();

            var repository = CreateBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repository.Delete(borrowerItem);
            //---------------Test Result -----------------------
            Assert.AreEqual(0, dbContext.Borrowers.Count());
            dbContext.Received().SaveChanges();
        }

        public BorrowerItemRepositoryBuilder CreateBuilder()
        {
            return new BorrowerItemRepositoryBuilder();
        }

        public class BorrowerItemRepositoryBuilder
        {
            private ILendingLibraryDbContext _lendingLibraryDbContext = Substitute.For<ILendingLibraryDbContext>();
            public BorrowerItemRepositoryBuilder WithDbContext(ILendingLibraryDbContext lendingLibraryDbContext)
            {
                _lendingLibraryDbContext = lendingLibraryDbContext;
                return this;
            }

            public IBorrowerItemRepository Build()
            {
                return new BorrowerItemRepository(_lendingLibraryDbContext);
            }
        }
    }
}