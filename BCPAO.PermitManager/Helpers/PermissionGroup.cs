using System.Collections.Generic;

namespace BCPAO.PermitManager.Helpers
{
	public class PermissionGroup
    {
		public string Id { get; set; }
		public string Name { get; set; }

		public static List<PermissionGroup> GetList()
		{
			var groups = new List<PermissionGroup>() {
				new PermissionGroup() { Id = "admin", Name = "Administration" },
				new PermissionGroup() { Id = "user_management", Name = "User Management" },
				new PermissionGroup() { Id = "settings", Name = "Settings" },
				new PermissionGroup() { Id = "general", Name = "General" }
			};

			return groups;
		}
	}
}
