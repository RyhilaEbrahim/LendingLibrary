using System;
using System.Collections.Generic;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Core.Interfaces.Repositories;

namespace Chillisoft.LendingLibrary.DB.Repositories
{
    public class ItemRepository:IItemRepository
    {
        private readonly ILendingLibraryDbContext _lendingLibraryDbContext;

        public ItemRepository(ILendingLibraryDbContext lendingLibraryDbContext)
        {
            if (lendingLibraryDbContext == null) throw new ArgumentNullException(nameof(lendingLibraryDbContext));
            _lendingLibraryDbContext = lendingLibraryDbContext;
        }

        public void Delete(Item item)
        {
            var items = Get(item.Id);
            _lendingLibraryDbContext.Items.Remove(items);
            _lendingLibraryDbContext.SaveChanges();
        }

        public Item Get(int id)
        {
            return _lendingLibraryDbContext.Items.FirstOrDefault(item => item.Id == id);
        }

        public void Save(Item item)
        {
            _lendingLibraryDbContext.AttachEntity(item);
            _lendingLibraryDbContext.SaveChanges();
        }

        public List<Item> GetAll()
        {
            return _lendingLibraryDbContext.Items.ToList();
        }
    }
}