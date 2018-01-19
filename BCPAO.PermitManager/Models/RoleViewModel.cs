using System.Collections.Generic;

namespace BCPAO.PermitManager.Models
{
	public class RoleViewModel
	{
		public RoleViewModel()
		{
			PermissionIds = new List<int>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string ConcurrencyStamp { get; set; }

		public List<int> PermissionIds { get; set; }
	}
}
