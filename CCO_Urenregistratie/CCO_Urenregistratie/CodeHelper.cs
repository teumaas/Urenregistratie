using CCO_Urenregistratie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CCO_Urenregistratie
{
    /// <summary>
    /// atribute for authentication for owner of the project or task.
    /// </summary>
    public class AuthorizeOwnerAttribute : AuthorizeAttribute
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            System.Web.Routing.RouteData rd = httpContext.Request.RequestContext.RouteData;

            string id = rd.Values["id"].ToString();
            string type = rd.Values["controller"].ToString();
            string userId = httpContext.User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);//.FirstOrDefault(x => x.UserName == userName); 
            object hasItem = null;

            if (user != null)
            {
                if (type == "projects")
                    hasItem = user.Projects.FirstOrDefault(x => x.Id.ToString() == id);

                else if (type == "tasks")
                    hasItem = user.Tasks.FirstOrDefault(x => x.Id.ToString() == id);
            }
            return hasItem != null | httpContext.User.IsInRole("Admin");
        }
    }
}