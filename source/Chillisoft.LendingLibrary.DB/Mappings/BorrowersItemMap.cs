using System.Data.Entity.ModelConfiguration;
using Chillisoft.LendingLibrary.Core.Domain;
using _BorrowersItems = Chillisoft.LendingLibrary.DB.DataConstants.Tables.BorrowersItem;
namespace Chillisoft.LendingLibrary.DB.Mappings

{
    public class BorrowersItemMap : EntityTypeConfiguration<BorrowersItem>
    {
        public BorrowersItemMap()
        {
            // Primary Key
            this.HasKey(s => s.Id);

            // Properties

            // table and column mappings
            this.ToTable(_BorrowersItems.TableName);
            this.Property(p => p.Id).HasColumnName(_BorrowersItems.Columns.BorrowersItemId);
            this.Property(p => p.BorrowerId).HasColumnName(_BorrowersItems.Columns.BorrowerId);
            this.Property(p => p.ItemId).HasColumnName(_BorrowersItems.Columns.ItemId);
            this.Property(p => p.DateBorrowed).HasColumnName(_BorrowersItems.Columns.DateBorrowed);
            this.Property(p => p.DateReturned).HasColumnName(_BorrowersItems.Columns.DateReturned);
            

        }
    }
}