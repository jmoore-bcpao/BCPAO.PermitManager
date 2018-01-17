using BCPAO.PermitManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
	{
		public void Configure(EntityTypeBuilder<UserProfile> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.FirstName).HasMaxLength(30);
			builder.Property(e => e.LastName).HasMaxLength(30);
			builder.ToTable("UserProfiles");
		}
	}
}
