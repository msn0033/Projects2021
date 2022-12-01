using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Officer.Data;

namespace Officer.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext db;
        

        // GET: RoleController1

        public RoleController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {  
            return View(db.Roles.ToList());
        }

        // GET: RoleController1/Details/5
        public ActionResult Details(string id)
        {
            return View(db.Roles.Find(id));
        }

        // GET: RoleController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole role)
        {
            try
            {
               if(ModelState.IsValid)
                {
                   role.Id = new Guid(role.Id).ToString();
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(role);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController1/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                return View(db.Roles.Find(id));
            }
            catch
            {
                return View();
            }
        }

        // POST: RoleController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                var temp = db.Roles.Find(role.Id);
                temp.Name = role.Name;
                db.Update(temp);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController1/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                return View(db.Roles.Find(id));
            }
            catch
            {
                return View();
            }
        }

        // POST: RoleController1/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_confige(string id)
        {
            try
            {
                
                db.Roles.Remove(db.Roles.Find(id));
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
