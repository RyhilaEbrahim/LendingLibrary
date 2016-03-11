﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var borrowersItem = Get(borrowerItem.Id);
            if (borrowersItem == null) return;
           _lendingLibraryDbContext.SetStateToDelete(borrowersItem);
            _lendingLibraryDbContext.SaveChanges();
        }

        public BorrowersItem Get(int id)
        {
            return _lendingLibraryDbContext.BorrowersItems
               .Include(client => client.Borrower)
               .Include(client => client.Item)
               .FirstOrDefault(x => x.Id == id);
        }

        public void Save(BorrowersItem borrowersItem)
        {
            _lendingLibraryDbContext.AttachEntity(borrowersItem);
            _lendingLibraryDbContext.SaveChanges();
        }

        public List<BorrowersItem> GetAll()
        {
            var borroweItems = _lendingLibraryDbContext.BorrowersItems
                .Include(borrower => borrower.Item)
                .Include(borrower => borrower.Borrower)
                .ToList();
            return borroweItems;
        }
    }
}