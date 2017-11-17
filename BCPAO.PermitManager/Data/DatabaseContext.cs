using BCPAO.PermitManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BCPAO.PermitManager.Data
{
	public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

		public DbSet<BuildingPermit> BuildingPermits { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);

			builder.HasDefaultSchema("bcpao");

			builder.Entity<BuildingPermit>(entity =>
			{
				entity.HasKey(e => e.PermitId);
				entity.Property(e => e.PropertyId);
				entity.Property(e => e.ParcelId);
				entity.Property(e => e.PermitNumber);
				entity.Property(e => e.PermitCode);
				entity.Property(e => e.PermitDesc);
				entity.Property(e => e.PermitValue);
				entity.Property(e => e.PermitStatus);
				entity.Property(e => e.IssueDate);
				entity.Property(e => e.FinalDate);
				entity.ToTable("Permits");
			});
        }
    }
}
