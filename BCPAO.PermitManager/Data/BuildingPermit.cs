using System;
using System.ComponentModel.DataAnnotations;

namespace BCPAO.PermitManager.Data
{
	public class BuildingPermit
	{
		[Display(Name = "Permit ID")]
		public int PermitId { get; set; }
		[Display(Name = "Property ID")]
		public int PropertyId { get; set; }
		[Display(Name = "Parcel ID")]
		public string ParcelId { get; set; }
		[Display(Name = "Permit Number")]
		public string PermitNumber { get; set; }
		[Display(Name = "Permit Code")]
		public string PermitCode { get; set; }
		[Display(Name = "Permit Description")]
		public string PermitDesc { get; set; }
		[Display(Name = "Permit Value")]
		public decimal PermitValue { get; set; }
		[Display(Name = "Permit Status")]
		public string PermitStatus { get; set; }
		[Display(Name = "Issue Date")]
		public DateTime IssueDate { get; set; }
		[Display(Name = "Final Date")]
		public DateTime FinalDate { get; set; }
		[Display(Name = "District Authority")]
		public string DistrictAuthority { get; set; }
		[Display(Name = "Date Created")]
		public string CreateDate { get; set; }
	}
}