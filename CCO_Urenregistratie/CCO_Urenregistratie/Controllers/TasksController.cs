using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CCO_Urenregistratie.Models;

namespace CCO_Urenregistratie.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tasks
        public ActionResult Index()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.Startdate = DateTime.Now;

            if (User.IsInRole("Admin"))
            {
                return View( db.Tasks.Include(t => t.Project).Include(t => t.User).ToList());
            }
            else
            {
                ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
                return View(user.Tasks.ToList());
            }
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // GET: Tasks/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.Startdate = DateTime.Now;
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ProjectId,Description,Startdate,Enddate")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.SetUserId(HttpContext.User.Identity.GetUserId());
                tasks.Enddate = DateTime.Now;
                db.Tasks.Add(tasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tasks.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", tasks.UserId);
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        [AuthorizeOwner]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tasks.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", tasks.UserId);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProjectId,Description,Startdate,Enddate")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tasks.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", tasks.UserId);
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        [AuthorizeOwner]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tasks tasks = db.Tasks.Find(id);
            db.Tasks.Remove(tasks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Method that is called to start the timer without it. It won't work
        public void Start()
        {
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
