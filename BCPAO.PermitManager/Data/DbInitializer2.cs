﻿using BCPAO.PermitManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCPAO.PermitManager.Data
{
	public interface IDbInitializer
	{
		/// <summary>
		/// Creates user 'Admin' if it doesn't exist.
		/// Creates role 'Admininistrators' if it doesn't exist.
		/// Set user 'Admin' in 'Admininistrators' role.
		/// </summary>
		Task CreateAdminAccountAsync();
	}

	public class DbInitializer2
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfigurationRoot _configRoot;

		public DbInitializer2(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfigurationRoot configRoot)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configRoot = configRoot;
		}

		public DbInitializer2()
		{
		}

		public async Task CreateAdminAccountAsync()
		{
			var adminSection = _configRoot.GetSection("Data:AdminUser");
			string role = adminSection["Role"];
			string username = adminSection["Name"];
			string email = adminSection["Email"];
			string password = adminSection["Password"];
			IdentityResult result;

			if (await _roleManager.FindByNameAsync(role) == null)
			{
				result = await _roleManager.CreateAsync(new IdentityRole(role));

				if (!result.Succeeded)
					OnIdentityResultFailed(string.Format("Can't create role '{0}'", role), result.Errors);
			}

			User user = await _userManager.FindByNameAsync(username);

			if (user == null)
			{
				user = new User
				{
					UserName = username,
					Email = email
				};

				result = await _userManager.CreateAsync(user, password);

				if (!result.Succeeded)
					OnIdentityResultFailed(string.Format("Can't create user '{0}'", username), result.Errors);
			}

			if (!await _userManager.IsInRoleAsync(user, role))
			{
				result = await _userManager.AddToRoleAsync(user, role);

				if (!result.Succeeded)
					OnIdentityResultFailed(string.Format("Can't set user '{0}' in role '{1}'", username, role), result.Errors);
			}
		}

		private static void OnIdentityResultFailed(string info, IEnumerable<IdentityError> errors)
		{
			throw new Exception($"{info}: {string.Join(", ", errors.Select(e => e.Description))}");
		}
	}
}
