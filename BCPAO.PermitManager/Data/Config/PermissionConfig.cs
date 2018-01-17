using BCPAO.PermitManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class PermissionConfig : IEntityTypeConfiguration<Permission>
	{
		public void Configure(EntityTypeBuilder<Permission> builder)
		{
			builder.HasIndex(e => e.Name).HasName("UK_Permission").IsUnique();
			builder.Property(e => e.Description).IsRequired().HasColumnType("text");
			builder.Property(e => e.Group).HasMaxLength(100).IsRequired();
			builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
			builder.ToTable("Permissions");
		}
	}
}
