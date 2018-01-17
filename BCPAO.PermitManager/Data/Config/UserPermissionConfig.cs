using BCPAO.PermitManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class UserPermissionConfig : IEntityTypeConfiguration<UserPermission>
	{
		public void Configure(EntityTypeBuilder<UserPermission> builder)
		{
			builder.HasKey(e => new { e.UserId, e.PermissionId });
			builder.HasOne(d => d.Permission).WithMany(p => p.UserPermissions).HasForeignKey(d => d.PermissionId);
			builder.HasOne(d => d.User).WithMany(p => p.UserPermissions).HasForeignKey(d => d.UserId);
			builder.ToTable("UserPermissions");
		}
	}
}
