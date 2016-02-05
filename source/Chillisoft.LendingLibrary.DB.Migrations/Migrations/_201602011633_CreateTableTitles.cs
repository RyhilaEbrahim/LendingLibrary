using FluentMigrator;
using _Title = Chillisoft.LendingLibrary.DB.DataConstants.Tables.Title;

namespace Chillisoft.LendingLibrary.DB.Migrations.Migrations
{
    [Migration(201602011633)]
    public class _201602011633_CreateTableTitles : Migration
    {
        public override void Up()
        {
            Create.Table(_Title.TableName)
                .WithColumn(_Title.Columns.TitleId).AsInt32().PrimaryKey("PK_TitleId").Identity()
                .WithColumn(_Title.Columns.Description).AsString(10)
                .WithDefaultEntityColumns();
        }

        public override void Down()
        {
            // No down migration
        }
    }
}