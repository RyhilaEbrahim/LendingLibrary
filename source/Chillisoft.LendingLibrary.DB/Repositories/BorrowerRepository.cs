using System;
using System.Collections.Generic;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;

namespace Chillisoft.LendingLibrary.DB.Repositories
{

    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly ILendingLibraryDbContext _lendingLibraryDbContext;

        public BorrowerRepository(ILendingLibraryDbContext lendingLibraryDbContext)
        {
            if (lendingLibraryDbContext == null) throw new ArgumentNullException(nameof(lendingLibraryDbContext));
            _lendingLibraryDbContext = lendingLibraryDbContext;
        }

        public void Delete(Borrower borrower)
        {
            var borrow = Get(borrower.Id);
            _lendingLibraryDbContext.Borrowers.Remove(borrow);
            _lendingLibraryDbContext.SaveChanges();
        }

        public Borrower Get(int id)
        {
            return _lendingLibraryDbContext.Borrowers.FirstOrDefault(borrower => borrower.Id == id);
        }

        public void Save(Borrower borrower)
        {
            _lendingLibraryDbContext.AttachEntity(borrower);
            _lendingLibraryDbContext.SaveChanges();
        }

        public List<Borrower> GetAll()
        {
            return _lendingLibraryDbContext.Borrowers.ToList();
        }

        public List<Title> GetAllTitles()
        {
            return _lendingLibraryDbContext.Titles.ToList();
        }

        public Title GetTitleById(int titleId)
        {
            return _lendingLibraryDbContext.Titles.FirstOrDefault(title => title.Id == titleId);
        }

        public byte[] GetPhoto(int id)
        {
            var borrower = _lendingLibraryDbContext.Borrowers.FirstOrDefault(b => b.Id==id);
            return borrower?.Photo;
        }
    }
}