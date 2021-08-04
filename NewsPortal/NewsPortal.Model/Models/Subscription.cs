using System;

namespace NewsPortal.Model.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public DateTime CreatedDate { get; set; }

        public string FollowerId { get; set; }
        public virtual ApplicationUser Follower { get; set; }

        public string FollowingId { get; set; }
        public virtual ApplicationUser Following { get; set; }

        public Subscription()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
