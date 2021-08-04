using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "Password must be at least 6 characters long!", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password do not match with password!")]
        public string ConfirmPassword { get; set; }
    }
}