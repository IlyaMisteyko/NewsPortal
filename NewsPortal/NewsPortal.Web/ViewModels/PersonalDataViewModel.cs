using System;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Web.ViewModels
{
    public class PersonalDataViewModel
    {
        public string UserProfileId { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters!")]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters!")]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        public bool? Gender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        public string Adress { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters!")]
        public string Country { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters!")]
        public string City { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public string AvatarUrl { get; set; }

        public string UserId { get; set; }
    }
}