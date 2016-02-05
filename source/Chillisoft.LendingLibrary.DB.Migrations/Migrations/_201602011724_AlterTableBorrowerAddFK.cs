using FluentMigrator;

namespace Chillisoft.LendingLibrary.DB.Migrations.Migrations
{
    [Migration(201602011724)]
    public class _201602011724_AlterTableBorrowerAddFK : Migration
    {
        public override void Up()
        {
            Alter.Table("Borrower")
                .AlterColumn("TitleId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("FK_Borrower_Title", "Title", "TitleId");
        }

        public override void Down()
        {
           
        }
    }
}