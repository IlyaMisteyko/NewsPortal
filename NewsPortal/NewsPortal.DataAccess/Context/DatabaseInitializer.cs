using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsPortal.DataAccess.Common.Identity;
using NewsPortal.DataAccess.Repositories;
using NewsPortal.Model.Models;

namespace NewsPortal.DataAccess.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<NewsPortalEntities>
    {
        protected override void Seed(NewsPortalEntities context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var userProfileRepo = new UserProfileRepository(context);
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            var adminRole = new ApplicationRole { Name = "admin" };
            var userRole = new ApplicationRole { Name = "user" };

            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            var admin = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", Activated = true, RoleId = roleManager.FindByName("admin").Id };
            string password = "!QAZ2wsx!@#";

            //-------------------------------------
            string p = "123456";
            var p1 = new ApplicationUser { UserName = "p1@test.test", Email = "p1@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p2 = new ApplicationUser { UserName = "p2@test.test", Email = "p2@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p3 = new ApplicationUser { UserName = "p3@test.test", Email = "p3@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p4 = new ApplicationUser { UserName = "p4@test.test", Email = "p4@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p5 = new ApplicationUser { UserName = "p5@test.test", Email = "p5@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p6 = new ApplicationUser { UserName = "p6@test.test", Email = "p6@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p7 = new ApplicationUser { UserName = "p7@test.test", Email = "p7@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p8 = new ApplicationUser { UserName = "p8@test.test", Email = "p8@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p9 = new ApplicationUser { UserName = "p9@test.test", Email = "p9@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p10 = new ApplicationUser { UserName = "p10@test.test", Email = "p10@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p11 = new ApplicationUser { UserName = "p11@test.test", Email = "p11@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
            var p12 = new ApplicationUser { UserName = "p12@test.test", Email = "p12@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };

            userManager.Create(p1, p);
            userManager.Create(p2, p);
            userManager.Create(p3, p);
            userManager.Create(p4, p);
            userManager.Create(p5, p);
            userManager.Create(p6, p);
            userManager.Create(p7, p);
            userManager.Create(p8, p);
            userManager.Create(p9, p);
            userManager.Create(p10, p);
            userManager.Create(p11, p);
            userManager.Create(p12, p);

            userManager.AddToRole(p1.Id, userRole.Name);
            userManager.AddToRole(p2.Id, userRole.Name);
            userManager.AddToRole(p3.Id, userRole.Name);
            userManager.AddToRole(p4.Id, userRole.Name);
            userManager.AddToRole(p5.Id, userRole.Name);
            userManager.AddToRole(p6.Id, userRole.Name);
            userManager.AddToRole(p7.Id, userRole.Name);
            userManager.AddToRole(p8.Id, userRole.Name);
            userManager.AddToRole(p9.Id, userRole.Name);
            userManager.AddToRole(p10.Id, userRole.Name);
            userManager.AddToRole(p11.Id, userRole.Name);
            userManager.AddToRole(p12.Id, userRole.Name);
            //-----------------------------------------------------

            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                var userProfile = new UserProfile{ UserProfileId = admin.Id };
                userProfileRepo.Create(userProfile);
                userManager.AddToRole(admin.Id, adminRole.Name);
            }

            base.Seed(context);
        }
    }
}
