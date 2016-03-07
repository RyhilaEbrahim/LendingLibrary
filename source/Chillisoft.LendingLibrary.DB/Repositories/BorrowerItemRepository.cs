using System;
using System.Collections.Generic;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;

namespace Chillisoft.LendingLibrary.DB.Repositories
{
    public class BorrowerItemRepository: IBorrowerItemRepository
    {
        private readonly ILendingLibraryDbContext _lendingLibraryDbContext;

        public BorrowerItemRepository(ILendingLibraryDbContext lendingLibraryDbContext)
        {
            if (lendingLibraryDbContext == null) throw new ArgumentNullException(nameof(lendingLibraryDbContext));
            _lendingLibraryDbContext = lendingLibraryDbContext;
        }
        public void Delete(BorrowersItem borrowerItem)
        {
            throw new System.NotImplementedException();
        }

        public BorrowersItem Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save(BorrowersItem borrowersItem)
        {
            _lendingLibraryDbContext.AttachEntity(borrowersItem);
            _lendingLibraryDbContext.SaveChanges();
        }

        public List<BorrowersItem> GetAll()
        {
            return _lendingLibraryDbContext.BorrowersItems.ToList();
        }
    }
}