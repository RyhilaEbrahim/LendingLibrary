using System.Data.Entity.ModelConfiguration;
using Chillisoft.LendingLibrary.Core.Domain;
using _Borrower = Chillisoft.LendingLibrary.DB.DataConstants.Tables.Borrower;
namespace Chillisoft.LendingLibrary.DB.Mappings
{
    public class BorrowerMap: EntityTypeConfiguration<Borrower>
    {
        public BorrowerMap()
        {
            // Primary Key
            this.HasKey(s => s.Id);

            // Properties

            // table and column mappings
            this.ToTable(_Borrower.TableName);
            this.Property(p => p.Id).HasColumnName(_Borrower.Columns.BorrowerId);
            this.Property(p => p.TitleId).HasColumnName(_Borrower.Columns.TitleId);
            this.Property(p => p.FirstName).HasColumnName(_Borrower.Columns.FirstName);
            this.Property(p => p.Surname).HasColumnName(_Borrower.Columns.Surname);
            this.Property(p => p.Email).HasColumnName(_Borrower.Columns.Email);
          
        }
    }
}