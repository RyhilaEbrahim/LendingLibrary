using System;
using System.Collections.Generic;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.DB.Repositories;
using Chillisoft.LendingLibrary.Tests.Common.Builders;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.RandomGenerators;

namespace Chillisoft.LendingLibrary.DB.Tests.Repositories
{
    public class TestItemBorrower
    {
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new ItemRepository(Substitute.For<ILendingLibraryDbContext>()));
        }

        [Test]
        public void Construct_GivenGivenDbContextIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var exception = Assert.Throws<ArgumentNullException>(() => new ItemRepository(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("lendingLibraryDbContext", exception.ParamName);
        }

        [Test]
        public void GetAll_GivenOneItemReturnedFromDbContext_ShouldReturnThatItem()
        {
            //---------------Set up test pack-------------------
            var item = CreateRandomItem();
            var dbContext = new TestDbContextBuilder()
                .WithItems(item)
                .Build();
            var repository = CreateItemsRepositoryBuilder()
                .WithDbContext(dbContext)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var items = repository.GetAll();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, items.Count);
            
        }

        [Test]
        public void Get_GivenId_ShouldReturnThatItem()
        {
            //---------------Set up test pack-------------------
            var item = CreateRandomItem();
            item.Id = RandomValueGen.GetRandomInt();
            var dbContext = new TestDbContextBuilder()
                .WithItems(item)
                .Build();
            var repository = CreateItemsRepositoryBuilder()
                .WithDbContext(dbContext)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var items = repository.Get(item.Id+1);
            //---------------Test Result -----------------------
            Assert.IsNull(items);

        }

        [Test]
        public void Delete_GivenBorrowerExists_ShouldDeleteBorrowerAndCallSavedChanges()
        {
            //---------------Set up test pack-------------------
            var item = new ItemBuilder()
                   .WithRandomProps().Build();

            var dbContext = new TestDbContextBuilder()
                .WithItems(item)
                .Build();

            var repository = CreateItemsRepositoryBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repository.Delete(item);
            //---------------Test Result -----------------------
            Assert.AreEqual(0, dbContext.Borrowers.Count());
            dbContext.Received().SaveChanges();
        }

        [Test]
        public void Save_GivenNewBorrower_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var item = new ItemBuilder()
                   .WithRandomProps()
                   .WithNewId()
                   .Build();

            var dbContext = new TestDbContextBuilder().Build();

            var repository = CreateItemsRepositoryBuilder().WithDbContext(dbContext).Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repository.Save(item);
            //---------------Test Result -----------------------
            dbContext.Received().AttachEntity(item);
            dbContext.Received().SaveChanges();
        }

        private Item CreateRandomItem()
        {
            return CreateItemBuilder()
                .WithRandomProps()
                .Build();
        }

        private ItemRepositoryBuilder CreateItemsRepositoryBuilder()
        {
            return new ItemRepositoryBuilder();
        }

        private ItemBuilder CreateItemBuilder()
        {
            return new ItemBuilder();
        }
        public class ItemRepositoryBuilder
        {
            private ILendingLibraryDbContext _ilendingDbContext = Substitute.For<ILendingLibraryDbContext>();

            public ItemRepositoryBuilder WithDbContext(ILendingLibraryDbContext lendingLibraryDbContext)
            {
                _ilendingDbContext = lendingLibraryDbContext;
                return this;
            }

            public ItemRepository Build()
            {
                return new ItemRepository(_ilendingDbContext);
            }
        }
    }
}

    