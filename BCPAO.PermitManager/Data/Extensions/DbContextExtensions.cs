using BCPAO.PermitManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BCPAO.PermitManager.Data.Extensions
{
	public static class DbContextExtensions
	{
		public static bool AllMigrationsApplied(this DbContext context)
		{
			return !context.Database.GetPendingMigrations().Any();

			//var applied = context.GetService<IHistoryRepository>()
			//	 .GetAppliedMigrations()
			//	 .Select(m => m.MigrationId);

			//var total = context.GetService<IMigrationsAssembly>()
			//	 .Migrations
			//	 .Select(m => m.Key);

			//return !total.Except(applied).Any();
		}

		public static void EnsureSeeded(this DatabaseContext context)
		{
			// Ensure we create initial admin user
			if (!context.Users.Any())
			{
				//var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "users.json"));
				//context.AddRange(users);
				//context.SaveChanges();
			}

			// Ensure we create initial admin role
			if (!context.Roles.Any())
			{
				//var roles = JsonConvert.DeserializeObject<List<Role>>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "roles.json"));
				//context.AddRange(roles);
				//context.SaveChanges();
			}

			// Ensure we create initial admin profile
			if (!context.UserProfiles.Any())
			{
				//List<UserProfile> profiles = JsonConvert.DeserializeObject<List<UserProfile>>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "profiles.json"));
				//context.UserProfiles.AddRange(profiles);
				//context.SaveChanges();
			}
		}
	}
}
