using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.DB;
using NSubstitute;

namespace Chillisoft.LendingLibrary.Tests.Common.Builders
{
    public class TestDbContextBuilder
    {
        private List<Borrower> _borrower = new List<Borrower>();
        private List<Title> _title = new List<Title>();
        private List<Item> _item = new List<Item>();
        private List<BorrowersItem> _borrowerItems = new List<BorrowersItem>();

        public TestDbContextBuilder WithBorrowers(params Borrower[] borrowers)
        {
            _borrower = borrowers.ToList();
            return this;
        }
        public TestDbContextBuilder WithTitles(params Title[] titles)
        {
            _title = titles.ToList();
            return this;
        }
        public TestDbContextBuilder WithItems (params Item[] items)
        {
            _item = items.ToList();
            return this;
        }
        public TestDbContextBuilder WithBorrowerItem (params BorrowersItem[] borrowerItems)
        {
            _borrowerItems = borrowerItems.ToList();
            return this;
        }
        public ILendingLibraryDbContext Build()
        {
            var lendingLibraryDbContext = Substitute.For<ILendingLibraryDbContext>();
            SetupTitles(lendingLibraryDbContext);
            SetupBorrowers(lendingLibraryDbContext);
            SetUpItems(lendingLibraryDbContext);
            SetUpBorrowerItems(lendingLibraryDbContext);
            return lendingLibraryDbContext;
        }

        private void SetUpItems(ILendingLibraryDbContext lendingLibraryDbContext)
        {
            if (_item != null)
            {
                var set = GetSubstituteDbSet<Item>().SetupData(_item);
                lendingLibraryDbContext.Items.Returns(set);
            }
        }
        private void SetUpBorrowerItems(ILendingLibraryDbContext lendingLibraryDbContext)
        {
            if (_borrowerItems != null)
            {
                var set = GetSubstituteDbSet<BorrowersItem>().SetupData(_borrowerItems);
                lendingLibraryDbContext.BorrowersItems.Returns(set);
            }
        }
        private void SetupBorrowers(ILendingLibraryDbContext lendingLibraryDbContext)
        {
            if (_borrower != null)
            {
                var set = GetSubstituteDbSet<Borrower>().SetupData(_borrower);
                lendingLibraryDbContext.Borrowers.Returns(set);
            }
        }

        private void SetupTitles(ILendingLibraryDbContext lendingLibraryDbContext)
        {
            if (_title != null)
            {
                var set = GetSubstituteDbSet<Title>().SetupData(_title);
                lendingLibraryDbContext.Titles.Returns(set);
            }
        }

        
        private DbSet<T> GetSubstituteDbSet<T>() where T : class
        {
            return Substitute.For<DbSet<T>, IQueryable<T>, IDbAsyncEnumerable<T>>();
        }

    }
}