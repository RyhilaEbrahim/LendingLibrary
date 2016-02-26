using FluentMigrator;
using _Borrower = Chillisoft.LendingLibrary.DB.DataConstants.Tables.Borrower;

namespace Chillisoft.LendingLibrary.DB.Migrations.Migrations
{
    [Migration(201602261539)]
    public class _201602261539_AlterTableBorrowerAddColumn: Migration
    {
        public override void Up()
        {
            Alter.Table(_Borrower.TableName)
                .AddColumn(_Borrower.Columns.ContentType).AsString(20).Nullable();

        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}