using System.ComponentModel.DataAnnotations;

namespace BCPAO.PermitManager.Models.AccountViewModels
{
	public class ForgotPasswordViewModel
    {
		[Required]
		[Display(Name = "Email Address")]
		public string Email { get; set; }
	}
}
