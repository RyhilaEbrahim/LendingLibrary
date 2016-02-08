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
            InMemoryDB.Borrowers.Remove(borrow);
            InMemoryDB.SaveChanges();
        }

        public Borrower Get(int id)
        {
            return _lendingLibraryDbContext.Borrowers.FirstOrDefault(borrower => borrower.Id == id);
        }

        public void Save(Borrower borrower)
        {
            if (borrower.Id == 0)
            {
                borrower.Id = InMemoryDB.Borrowers.Count + 1;
                InMemoryDB.Borrowers.Add(borrower);
            }
            else
            {
                var existingBorrower = Get(borrower.Id);

                existingBorrower.FirstName = borrower.FirstName;
                existingBorrower.Surname = borrower.Surname;
                existingBorrower.Email = borrower.Email;
                existingBorrower.Title = borrower.Title;
            }

            InMemoryDB.SaveChanges();
        }

        public List<Borrower> GetAll()
        {
            return _lendingLibraryDbContext.Borrowers.ToList();
        }

        public List<Title> GetAllTitles()
        {
            return InMemoryDB.Titles.ToList();
        }

        public Title GetTitleById(int titleId)
        {
            Predicate<Title> idPredicate = (Title p) => { return p.Id == titleId; };

            return InMemoryDB.Titles.Find(idPredicate);
        }
    }
}