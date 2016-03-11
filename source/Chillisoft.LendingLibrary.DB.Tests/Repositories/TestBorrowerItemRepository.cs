using System;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;
using Chillisoft.LendingLibrary.DB.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace Chillisoft.LendingLibrary.DB.Tests.Repositories
{
    public class TestBorrowerItemRepository
    {

        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new BorrowerItemRepository(NSubstitute.Substitute.For<ILendingLibraryDbContext>()));
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