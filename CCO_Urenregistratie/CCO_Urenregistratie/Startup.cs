using CCO_Urenregistratie.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Web.Security;

[assembly: OwinStartupAttribute(typeof(CCO_Urenregistratie.Startup))]
namespace CCO_Urenregistratie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            createRolesandUsers();
            ConfigureAuth(app);
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // checks of the admin role is created if not it will create one and creates a admin account   
            if (!roleManager.RoleExists("Admin"))
            {

                // creating admin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //creating the admin account                

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.FirstName = "Jan";
                user.LastName = "de Admin";
                user.Email = "admin@gmail.com";

                string userPWD = "admin123";

                var chkUser = UserManager.Create(user, userPWD);

                //checks of the user is correctly created and adds it to the role 
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }
        }
    }
}
