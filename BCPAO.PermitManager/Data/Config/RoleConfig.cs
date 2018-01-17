using BCPAO.PermitManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class RoleConfig : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasIndex(e => e.NormalizedName).HasName("RoleNameIndex");
			builder.Property(e => e.Name).HasMaxLength(256);
			builder.Property(e => e.NormalizedName).HasMaxLength(256);
			builder.HasMany(d => d.RolePermissions).WithOne(p => p.Role).HasForeignKey(d => d.RoleId);
			builder.ToTable("Roles");
		}
	}
}
