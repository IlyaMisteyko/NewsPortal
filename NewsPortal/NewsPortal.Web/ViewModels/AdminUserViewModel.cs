using System.Web.Mvc;

namespace NewsPortal.Web.ViewModels
{
    public class AdminUserViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool State { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }
        public SelectList Roles { get; set; }
    }
}