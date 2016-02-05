using System;
using FluentMigrator;
using _Title = Chillisoft.LendingLibrary.DB.DataConstants.Tables.Title;
namespace Chillisoft.LendingLibrary.DB.Migrations.Migrations
{
    [Migration(201602011707)]
    public class _201602011707_PopulateTableTitles:MigrationBase
    {
        public override void Up()
        {
            this.Insert.IntoTable(_Title.TableName).Row(new { Description = "Miss", Created = DateTime.Now.AddSeconds(-7) });
            this.Insert.IntoTable(_Title.TableName).Row(new { Description = "Ms", Created = DateTime.Now.AddSeconds(-6) });
            this.Insert.IntoTable(_Title.TableName).Row(new { Description = "Mrs", Created = DateTime.Now.AddSeconds(-5) });
            this.Insert.IntoTable(_Title.TableName).Row(new { Description = "Mr", Created = DateTime.Now.AddSeconds(-4) });
            this.Insert.IntoTable(_Title.TableName).Row(new { Description = "Dr", Created = DateTime.Now.AddSeconds(-3) });
            this.Insert.IntoTable(_Title.TableName).Row(new { Description = "Prof", Created = DateTime.Now.AddSeconds(-2) });
            this.Insert.IntoTable(_Title.TableName).Row(new { Description = "Rev", Created = DateTime.Now.AddSeconds(-1) });
        }

        public override void Down()
        {
           
        }
    }
}