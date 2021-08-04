using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace NewsPortal.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AvatarUrl { get; set; }
        public bool Activated { get; set; }
        public string RoleId { get; set; }

        public virtual UserProfile Profile { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Subscription> Followers { get; set; }
        public virtual ICollection<Subscription> Followings { get; set; }

        public ApplicationUser()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
