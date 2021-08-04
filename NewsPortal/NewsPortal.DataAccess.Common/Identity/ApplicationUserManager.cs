using Microsoft.AspNet.Identity;
using NewsPortal.Model.Models;

namespace NewsPortal.DataAccess.Common.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {

        }
    }
}