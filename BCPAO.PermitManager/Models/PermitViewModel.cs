using System;

namespace BCPAO.PermitManager.Models
{
	public class PermitViewModel
	{
		public int PermitId { get; set; }
		public int PropertyId { get; set; }
		public string ParcelId { get; set; }
		public string PermitNumber { get; set; }
		public string PermitCode { get; set; }
		public string PermitDesc { get; set; }
		public decimal PermitValue { get; set; }
		public string PermitStatus { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime FinalDate { get; set; }
		public string DistrictAuthority { get; set; }
		public string CreateDate { get; set; }
	}
}