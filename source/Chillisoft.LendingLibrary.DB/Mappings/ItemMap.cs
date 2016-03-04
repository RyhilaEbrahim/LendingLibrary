using System.Data.Entity.ModelConfiguration;
using Chillisoft.LendingLibrary.Core.Domain;
using _Item = Chillisoft.LendingLibrary.DB.DataConstants.Tables.Item;
namespace Chillisoft.LendingLibrary.DB.Mappings
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            // Primary Key
            this.HasKey(s => s.Id);

            // Properties

            // table and column mappings
            this.ToTable(_Item.TableName);
            this.Property(p => p.Id).HasColumnName(_Item.Columns.ItemId);
            this.Property(p => p.Description).HasColumnName(_Item.Columns.Description);
            
        }
    }
}