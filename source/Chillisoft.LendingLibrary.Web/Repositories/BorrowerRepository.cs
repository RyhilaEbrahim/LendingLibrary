using System;
using System.Collections.Generic;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Repositories
{
    public interface IBorrowerRepository
    {
        void Delete(Borrower borrower);
        Borrower Get(int id);
        void Save(Borrower borrower);
        List<Borrower> GetAll();
        List<Title> GetAllTitles();
        Title GetTitleById(int titleId);
    }

    public class BorrowerRepository : IBorrowerRepository
    {
        public void Delete(Borrower borrower)
        {
            var borrow = Get(borrower.Id);
            InMemoryDB.Borrowers.Remove(borrow);
        }

        public Borrower Get(int id)
        {
            return InMemoryDB.Borrowers.FirstOrDefault(borrower => borrower.Id == id);
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

        }

        public List<Borrower> GetAll()
        {
            return InMemoryDB.Borrowers.ToList();
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