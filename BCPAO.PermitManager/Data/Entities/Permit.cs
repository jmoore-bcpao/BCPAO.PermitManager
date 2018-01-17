using System;
using System.ComponentModel.DataAnnotations;

namespace BCPAO.PermitManager.Data.Entities
{
	public class Permit
	{
		public int Id { get; set; }
		[Display(Name = "Permit ID")]
		public int PermitId { get; set; }
		[Display(Name = "Property ID")]
		public string PropertyId { get; set; }
		[Display(Name = "Parcel ID")]
		public string ParcelId { get; set; }
		[Display(Name = "Permit ID")]
		public string PermitNumber { get; set; }
		[Display(Name = "Permit Type")]
		public string PermitCode { get; set; }
		[Display(Name = "Permit Description")]
		public string PermitDesc { get; set; }
		[Display(Name = "Amount")]
		public string PermitValue { get; set; }
		[Display(Name = "Status")]
		public string PermitStatus { get; set; }
		[Display(Name = "Issue Date")]
		//[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public string IssueDate { get; set; }
		//[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		[Display(Name = "Final Date")]
		public string FinalDate { get; set; }
		[Display(Name = "District Authority")]
		public string DistrictAuthority { get; set; }
		[Display(Name = "Date Created")]
		public DateTime CreateDate { get; set; }

		public int UserId { get; set; }
		public virtual User User { get; set; }
	}
}