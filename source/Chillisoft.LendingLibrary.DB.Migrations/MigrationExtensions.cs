using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Chillisoft.LendingLibrary.DB.Migrations
{
    public static class MigrationExtensions
    {
        public static ICreateTableWithColumnSyntax WithDefaultEntityColumns(this ICreateTableWithColumnSyntax root)
        {
            return root.WithColumn("Created").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                        .WithColumn("LastModified").AsDateTime().Nullable()
                        .WithColumn("Enabled").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}