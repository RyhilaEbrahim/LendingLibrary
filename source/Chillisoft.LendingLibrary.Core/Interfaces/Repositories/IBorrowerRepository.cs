using System.Collections.Generic;
using Chillisoft.LendingLibrary.Core.Domain;

namespace Chillisoft.LendingLibrary.Core.Interfaces.Repositories
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
}