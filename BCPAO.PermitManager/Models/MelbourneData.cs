using System;

namespace BCPAO.PermitManager.Models
{
	public class MelbourneData
    {
		public string ParcelId { get; set; }
		public string PropertyId { get; set; }
		public string PermitId { get; set; }
		public string PermitStatus { get; set; } // Closed or Issued
		public string IssueDate { get; set; }
		public string PermitAmount { get; set; }
		public string FinalDate { get; set; }
		public string PermitType { get; set; }
		public string PermitCode { get; set; }
		public string ResCom { get; set; } // Residential or Commerical
		public string AddressLine { get; set; }
		public string PermitDesc { get; set; }
	}
}
