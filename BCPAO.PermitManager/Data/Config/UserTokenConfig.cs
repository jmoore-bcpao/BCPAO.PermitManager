using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class UserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<int>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
		{
			builder.ToTable("UserTokens");
		}
	}
}
