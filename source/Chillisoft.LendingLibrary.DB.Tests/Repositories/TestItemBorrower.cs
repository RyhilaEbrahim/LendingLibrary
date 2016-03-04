using System;
using Chillisoft.LendingLibrary.DB.Repositories;
using Chillisoft.LendingLibrary.Tests.Common.Builders;
using NSubstitute;
using NUnit.Framework;

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

        

     
        private ItemRepositoryBuilder CreateUnitsRepositoryBuilder()
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

            public ItemRepositoryBuilder WithDbContext(ILendingLibraryDbContext _lendingLibraryDbContext)
            {
                _ilendingDbContext = _lendingLibraryDbContext;
                return this;
            }

            public ItemRepository Build()
            {
                return new ItemRepository(_ilendingDbContext);
            }
        }
    }
}

    