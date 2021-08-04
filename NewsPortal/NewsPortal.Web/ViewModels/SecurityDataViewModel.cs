using System;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Web.ViewModels
{
    public class SecurityDataViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }

        public string Role { get; set; }
    }
}