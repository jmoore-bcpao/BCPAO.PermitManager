using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCPAO.PermitManager.Data.Config
{
	public class UserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<int>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
		{
			builder.ToTable("UserClaims");
		}
	}
}
