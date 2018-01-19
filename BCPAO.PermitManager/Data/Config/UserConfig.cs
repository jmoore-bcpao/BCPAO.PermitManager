using BCPAO.PermitManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class UserConfig : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");
			builder.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex");
			builder.Property(e => e.FirstName).HasMaxLength(30);
			builder.Property(e => e.LastName).HasMaxLength(30);
			builder.Property(e => e.Email).HasMaxLength(256);
			builder.Property(e => e.NormalizedEmail).HasMaxLength(256);
			builder.Property(e => e.NormalizedUserName).HasMaxLength(256);
			builder.Property(e => e.UserName).HasMaxLength(256);
			builder.Property(e => e.CreateDate);
			builder.HasMany(d => d.UserPermissions).WithOne(p => p.User).HasForeignKey(d => d.UserId);

			// Prevents duplicate foreign keys when running EF Core Migrations
			// docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x
			builder.HasMany(e => e.Claims).WithOne().HasForeignKey(e => e.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(e => e.Logins).WithOne().HasForeignKey(e => e.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(e => e.Roles).WithOne().HasForeignKey(e => e.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

			//builder.HasMany(d => d.DeliveredPackages).WithOne(p => p.DeliveredBy).HasForeignKey(d => d.DeliveredByUserId);
			//builder.HasMany(d => d.AssignedPackages).WithOne(p => p.AssignedTo).HasForeignKey(d => d.AssignedToUserId);
			//builder.HasMany(d => d.Payments).WithOne(p => p.User).HasForeignKey(d => d.UserId);
			//builder.HasMany(d => d.CreatedBookings).WithOne(p => p.CreatedBy).HasForeignKey(d => d.CreatedByUserId);

			builder.ToTable("Users");
		}
	}
}
