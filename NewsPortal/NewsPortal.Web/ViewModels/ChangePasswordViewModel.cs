using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [StringLength(20, ErrorMessage = "Password must be at least 6 characters long!", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "Confirm password do not match with password!")]
        public string ConfirmPassword { get; set; }
    }
}