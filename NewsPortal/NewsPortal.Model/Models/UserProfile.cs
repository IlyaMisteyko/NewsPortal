using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Model.Models
{
    public class UserProfile
    {
        [ForeignKey("User")]
        public string UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Skype { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
