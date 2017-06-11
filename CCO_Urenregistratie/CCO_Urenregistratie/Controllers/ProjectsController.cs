using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CCO_Urenregistratie.Models;
using Microsoft.AspNet.Identity;
using System.Drawing;

namespace CCO_Urenregistratie.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.User);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View(projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Description,Color,Name")] Project project)
        {
            Random rnd = new Random();
            Color rndColor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
         
            if (ModelState.IsValid)
            {
                project.SetUserId(HttpContext.User.Identity.GetUserId());
                project.Color = rndColor.Name.Substring(2);
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", project.UserId);
            return View(project);
        }

        // GET: Projects/Edit/5
        [AuthorizeOwner]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", project.UserId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Description,Color,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", project.UserId);
            return View(project);
        }

        // GET: Projects/Delete/5
        [AuthorizeOwner]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Get: Project/dashboard
        public ActionResult Dashboard()
        {
            ViewBag.Projects = db.Projects.ToList();
            ViewBag.Users = db.Users.ToList();

            List<string> color = new List<string>();
            List<string> name = new List<string>();
            List<object> hours = new List<object>();
            double totalHours = 0;

            List<Project> x = db.Projects.ToList();
            x.ForEach(z =>
            {
                color.Add("#"+ z.Color);
                name.Add(z.Name);
                hours.Add(z.GetHoursForChart());
                totalHours += z.GetTotalTime();

            });
            ViewBag.Colors = color;
            ViewBag.Names = name;
            ViewBag.Hours = hours;

            TimeSpan result = TimeSpan.FromHours(totalHours);
            string fromTimeString = result.ToString("hh':'mm");

            ViewBag.TotalHours = fromTimeString;
            return View();

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
