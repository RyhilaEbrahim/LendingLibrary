using _BorrowersItems = Chillisoft.LendingLibrary.DB.DataConstants.Tables.BorrowersItem;
using FluentMigrator;

namespace Chillisoft.LendingLibrary.DB.Migrations.Migrations
{
    [Migration(201603071549)]
    public class _201603071549_CreateTableBorrowersItem: Migration
    {
        public override void Up()
        {
            Create.Table(_BorrowersItems.TableName)
             .WithColumn(_BorrowersItems.Columns.BorrowersItemId).AsInt32().PrimaryKey("PK_BorrowerItemId").Identity()
             .WithColumn(_BorrowersItems.Columns.BorrowerId).AsInt32().NotNullable()
             .WithColumn(_BorrowersItems.Columns.ItemId).AsInt32().NotNullable()
             .WithColumn(_BorrowersItems.Columns.DateBorrowed).AsDateTime().NotNullable()
             .WithColumn(_BorrowersItems.Columns.DateReturned).AsDateTime().Nullable()
            .WithDefaultEntityColumns();
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}