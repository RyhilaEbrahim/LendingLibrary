using FluentMigrator;
using _Borrower = Chillisoft.LendingLibrary.DB.DataConstants.Tables.Borrower;
namespace Chillisoft.LendingLibrary.DB.Migrations.Migrations
{

    [Migration(201602011712)]
    public class _201602011712_CreateTableBorrowers:Migration
    {
        public override void Up()
        {
            Create.Table(_Borrower.TableName)
                .WithColumn(_Borrower.Columns.BorrowerId).AsInt32().PrimaryKey("PK_BorrowerID").Identity()
                .WithColumn(_Borrower.Columns.TitleId).AsInt32().NotNullable()
                .WithColumn(_Borrower.Columns.FirstName).AsString(25)
                .WithColumn(_Borrower.Columns.Surname).AsString(25)
                .WithColumn(_Borrower.Columns.Email).AsString(200)
                .WithDefaultEntityColumns();
        }

        public override void Down()
        {
            // No down migration
        }
    }
}