using System.Data.Entity.ModelConfiguration;
using Chillisoft.LendingLibrary.Core.Domain;

namespace Chillisoft.LendingLibrary.DB.Mappings
{
    public class RoleMap : EntityTypeConfiguration<Roles>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(s => s.Id);

            // Properties

            // table and column mappings
            this.ToTable("AspNetRoles");
            this.Property(p => p.Id).HasColumnName("Id");
            this.Property(p => p.Name).HasColumnName("Name");

        }
    }
}