using System.Collections.Generic;
using Chillisoft.LendingLibrary.Core.Domain;

namespace Chillisoft.LendingLibrary.Core.Interfaces.Repositories
{
    public interface IItemRepository
    {
        void Delete(Item item);
        Item Get(int id);
        void Save(Item item);
        List<Item> GetAll();
    }
}