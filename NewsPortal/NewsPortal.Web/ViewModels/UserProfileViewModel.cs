using System;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Web.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Profile created date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Profile activated")]
        public bool Activated { get; set; }

        public bool? Gender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        public string Adress { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        [Display(Name = "Avatar")]
        public string AvatarUrl { get; set; }

        public bool IsFollowing { get; set; }
    }
}