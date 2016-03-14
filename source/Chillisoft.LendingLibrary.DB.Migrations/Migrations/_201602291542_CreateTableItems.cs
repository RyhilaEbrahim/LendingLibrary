using FluentMigrator;
using _Item = Chillisoft.LendingLibrary.DB.DataConstants.Tables.Item;
namespace Chillisoft.LendingLibrary.DB.Migrations.Migrations
{
    [Migration(201602291542)]
    public class _201602291542_CreateTableItems : Migration
    {
        public override void Up()
        {
            Create.Table(_Item.TableName)
                .WithColumn(_Item.Columns.ItemId).AsInt32().PrimaryKey("PK_ItemId").Identity()
                .WithColumn(_Item.Columns.Description).AsString(55)
                .WithDefaultEntityColumns();
        }

        public override void Down()
        {
            // No down migration
        }
    }
}