using BCPAO.PermitManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BCPAO.PermitManager.Data
{
	public static class SeedData
	{
		public static async Task Initialize(IServiceProvider serviceProvider, string password)
		{
			using (var context = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>()))
			{
				var adminId = await EnsureUser(serviceProvider, password, "admin");

				await EnsureRole(serviceProvider, adminId, "Admin");
				
				SeedDB(context, adminId);
			}
		}

		private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string password, string username)
		{
			var userManager = serviceProvider.GetService<UserManager<User>>();

			var user = await userManager.FindByNameAsync(username);
			if (user == null)
			{
				user = new User { UserName = username };
				await userManager.CreateAsync(user, password);
			}

			return user.Id.ToString();
		}


		private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string userId, string role)
		{
			IdentityResult IR = null;

			var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

			if (!await roleManager.RoleExistsAsync(role))
			{
				IR = await roleManager.CreateAsync(new IdentityRole(role));
			}

			var userManager = serviceProvider.GetService<UserManager<User>>();

			var user = await userManager.FindByIdAsync(userId);

			IR = await userManager.AddToRoleAsync(user, role);

			return IR;
		}


		private static void SeedDB(DatabaseContext context, string adminId)
		{
			if (context.Users.Any())
			{
				return; // DB has been seeded
			}

			//context.Users.AddRange(
			//	new User
			//	{
			//		AccessFailedCount = 0,
			//		ConcurrencyStamp = Guid.NewGuid().ToString(),
			//		EmailConfirmed = true,
			//		FirstName = "Administrator",
			//		LastName = null,
			//		LockoutEnabled = false,
			//		LockoutEnd = null,
			//		UserName = "admin",
			//		Email = "admin@bcpao.us",
			//		NormalizedEmail = "ADMIN@BCPAO.US",
			//		NormalizedUserName = "ADMIN",
			//		PasswordHash = null,
			//		PhoneNumber = null,
			//		PhoneNumberConfirmed = false,
			//		SecurityStamp = Guid.NewGuid().ToString("D"),
			//		TwoFactorEnabled = false,
			//		CreateDate = DateTime.Now
			//	}
			//);

			context.SaveChanges();
		}

	}
}
