using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class UserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<int>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
		{
			builder.HasKey(e => new { e.LoginProvider, e.ProviderKey });
			builder.Property(e => e.LoginProvider).HasMaxLength(450);
			builder.Property(e => e.ProviderKey).HasMaxLength(450);
			builder.ToTable("UserLogins");
		}
	}
}
