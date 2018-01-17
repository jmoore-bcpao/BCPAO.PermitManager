namespace BCPAO.PermitManager.Data.Entities
{
	public class UserPermission
	{
		public int UserId { get; set; }
		public int PermissionId { get; set; }

		public virtual Permission Permission { get; set; }
		public virtual User User { get; set; }
	}
}
