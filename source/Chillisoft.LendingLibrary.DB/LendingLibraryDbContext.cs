using System.Data.Common;
using System.Data.Entity;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.DB.Mappings;

namespace Chillisoft.LendingLibrary.DB
{
    public interface ILendingLibraryDbContext
    {
        int SaveChanges();
        void AttachEntity(EntityBase entity);
        IDbSet<Borrower> Borrowers { get; set; }
        IDbSet<Title> Titles { get; set; }
        IDbSet<Item> Items { get; set; }
        IDbSet<Roles> Roles { get; set; }
        IDbSet<BorrowersItem> BorrowersItems { get; set; }
        void SetStateToDelete(EntityBase entityList);
    }

    public class LendingLibraryDbContext : DbContext, ILendingLibraryDbContext
    {
        public LendingLibraryDbContext(DbConnection connection) : base(connection, true)
        {
            
        }
        public LendingLibraryDbContext(string nameOrConnectionString = "DefaultConnection") : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var config = modelBuilder.Configurations;
            config.Add(new BorrowerMap());
            config.Add(new TitleMap());
            config.Add(new ItemMap());
            config.Add(new BorrowersItemMap());
            config.Add(new RoleMap());
            base.OnModelCreating(modelBuilder);
        }

        public void AttachEntity(EntityBase entity)
        {
            Entry(entity).State = entity.IsNew() ? EntityState.Added : EntityState.Modified;
        }

        public IDbSet<Borrower> Borrowers { get; set; }
        public IDbSet<Title> Titles { get; set; }
        public IDbSet<Item> Items { get; set; }
        public IDbSet<Roles> Roles { get; set; }
        public IDbSet<BorrowersItem> BorrowersItems { get; set; }
        public void SetStateToDelete(EntityBase entityList)
        {
            Entry(entityList).State = entityList.Id == 0 ? EntityState.Detached : EntityState.Deleted;
        }
    }
}