using System.Collections.Generic;
using Chillisoft.LendingLibrary.Core.Domain;

namespace Chillisoft.LendingLibrary.Core.Interfaces.Repositories
{
    public interface IBorrowerItemRepository
    {
        void Delete(BorrowersItem borrowerItem);
        BorrowersItem Get(int id);
        void Save(BorrowersItem borrowersItem);
        List<BorrowersItem> GetAll();
      
    }
}