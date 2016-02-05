using System.Data.Entity;

namespace Chillisoft.LendingLibrary.DB
{
    public interface ILendingLibraryDbContext
    {
        int SaveChanges();
    }

    public class LendingLibraryDbContext : DbContext, ILendingLibraryDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}