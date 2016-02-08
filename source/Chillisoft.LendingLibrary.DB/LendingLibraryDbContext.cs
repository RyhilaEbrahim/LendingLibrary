using System.Data.Common;
using System.Data.Entity;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.DB.Mappings;

namespace Chillisoft.LendingLibrary.DB
{
    public interface ILendingLibraryDbContext
    {
        int SaveChanges();
        IDbSet<Borrower> Borrowers { get; set; }
        IDbSet<Title> Titles { get; set; }
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
            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Borrower> Borrowers { get; set; }
        public IDbSet<Title> Titles { get; set; }
    }
}