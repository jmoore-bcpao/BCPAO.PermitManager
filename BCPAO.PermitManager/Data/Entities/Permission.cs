using System.Collections.Generic;

namespace BCPAO.PermitManager.Data.Entities
{
	public class Permission
    {
		public Permission()
		{
			RolePermissions = new HashSet<RolePermission>();
			UserPermissions = new HashSet<UserPermission>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Group { get; set; }
		public string Description { get; set; }

		public virtual ICollection<UserPermission> UserPermissions { get; set; }
		public virtual ICollection<RolePermission> RolePermissions { get; set; }
	}
}
