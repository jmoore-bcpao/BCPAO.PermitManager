using BCPAO.PermitManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class PermitConfig : IEntityTypeConfiguration<Permit>
	{
		public void Configure(EntityTypeBuilder<Permit> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasAlternateKey(e => e.PermitNumber);
			builder.Property(e => e.PropertyId).HasMaxLength(10);
			builder.Property(e => e.ParcelId).HasMaxLength(30);
			builder.Property(e => e.PermitCode).HasMaxLength(30);
			builder.Property(e => e.PermitDesc).HasMaxLength(500);
			builder.Property(e => e.PermitValue).HasMaxLength(30);
			builder.Property(e => e.PermitStatus).HasMaxLength(30);
			builder.Property(e => e.IssueDate);
			builder.Property(e => e.FinalDate);
			builder.Property(e => e.DistrictAuthority).HasMaxLength(30);
			builder.Property(e => e.CreateDate);
			builder.ToTable("Permits");
		}
	}
}
