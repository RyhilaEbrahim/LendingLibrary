using System;
using FluentMigrator;
using  _Borrower= Chillisoft.LendingLibrary.DB.DataConstants.Tables.Borrower;

namespace Chillisoft.LendingLibrary.DB.Migrations.Migrations
{
    [Migration(201602221731)]
    public class _201602221731_AlterTableBorrower: Migration
    {
        public override void Up()
        {
            Alter.Table(_Borrower.TableName)
                .AddColumn(_Borrower.Columns.ContactNumber).AsString(20).Nullable()
                .AddColumn(_Borrower.Columns.Photo).AsBinary(int.MaxValue).Nullable();
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}