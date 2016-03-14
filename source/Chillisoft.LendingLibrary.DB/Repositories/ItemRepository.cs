using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<Item> GetAllItemsNotLent()
        {
            var allItemsMap = _lendingLibraryDbContext.Items.ToDictionary(item => item.Id);

            var lentItems = _lendingLibraryDbContext.BorrowersItems
                .Where(item => item.DateBorrowed != null && item.DateReturned == null)
                .ToList();

            var itemsNotLent = RemoveLentItems(lentItems, allItemsMap);


            return itemsNotLent;
        }

        private List<Item> RemoveLentItems(IEnumerable<BorrowersItem> lentItems, Dictionary<int, Item> allItemsMap)
        {
            foreach (var borrowersItem in lentItems)
            {
                allItemsMap.Remove(borrowersItem.ItemId);
            }
            return allItemsMap.Values.ToList();
        }

        public List<Item> GetAll()
        {
            var allItems=_lendingLibraryDbContext
                .Items
                .ToList();

            return allItems;
        }
    }
}