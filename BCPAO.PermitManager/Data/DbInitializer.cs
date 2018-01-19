using BCPAO.PermitManager.Data.Entities;
using System;
using System.Linq;

namespace BCPAO.PermitManager.Data
{
	public class DbInitializer
	{
		private DatabaseContext _context;

		public DbInitializer(DatabaseContext context)
		{
			_context = context;
		}

		public async void SeedAdminUser()
		{
			_context.Database.EnsureCreated();

			if (_context.Users.Any())
			{
				return; // DB has been seeded
			}

			var roles = new Role[]
			{
				new Role
				{
					Name = "admin",
					NormalizedName = "ADMIN",
					ConcurrencyStamp = Guid.NewGuid().ToString()
				}
			};
			foreach (var role in roles)
			{
				_context.Roles.Add(role);
			}

			var users = new User[]
			{
				new User
				{
					AccessFailedCount = 0,
					ConcurrencyStamp = Guid.NewGuid().ToString(),
					EmailConfirmed = true,
					FirstName = "Administrator",
					LastName = null,
					LockoutEnabled = false,
					LockoutEnd = null,
					UserName = "admin",
					Email = "admin@bcpao.us",
					NormalizedEmail = "ADMIN@BCPAO.US",
					NormalizedUserName = "ADMIN",
					PasswordHash = null,
					PhoneNumber = null,
					PhoneNumberConfirmed = false,
					SecurityStamp = Guid.NewGuid().ToString(),
					TwoFactorEnabled = false,
					CreateDate = DateTime.Now
				}
			};
			foreach (var user in users)
			{
				_context.Users.Add(user);
			}

			await _context.SaveChangesAsync();
		}

		//public static void Initialize(DatabaseContext context)
		//{
			
		//}
	}
}
