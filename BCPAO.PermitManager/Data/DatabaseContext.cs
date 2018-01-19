using BCPAO.PermitManager.Data.Config;
using BCPAO.PermitManager.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BCPAO.PermitManager.Data
{
	public class DatabaseContext : IdentityDbContext<User, Role, int>
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
		}


		public virtual new DbSet<User> Users { get; set; }
		public virtual new DbSet<Role> Roles { get; set; }

		public virtual DbSet<Permission> Permissions { get; set; }
		public virtual DbSet<RolePermission> RolePermissions { get; set; }
		public virtual DbSet<UserPermission> UserPermissions { get; set; }

		public virtual DbSet<Permit> Permits { get; set; }
		public virtual DbSet<UserProfile> UserProfiles { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);

			builder.HasDefaultSchema("bcpao");

			builder.ApplyConfiguration(new PermitConfig());

			builder.ApplyConfiguration(new PermissionConfig());

			builder.ApplyConfiguration(new RoleConfig());
			builder.ApplyConfiguration(new RoleClaimConfig());
			builder.ApplyConfiguration(new RolePermissionConfig());

			builder.ApplyConfiguration(new UserConfig());
			builder.ApplyConfiguration(new UserClaimConfig());
			builder.ApplyConfiguration(new UserLoginConfig());
			builder.ApplyConfiguration(new UserTokenConfig());
			builder.ApplyConfiguration(new UserPermissionConfig());

			builder.ApplyConfiguration(new UserRoleConfig());
			builder.ApplyConfiguration(new UserProfileConfig());

		}
	}
}
