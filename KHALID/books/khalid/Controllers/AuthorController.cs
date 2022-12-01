using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using khalid.Models;
using khalid.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace khalid.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IRepositories<Author> _Au;

        public AuthorController( IRepositories<Author>Au)
        {
            _Au = Au;
        }
        // GET: Author
        public ActionResult Index()
        {
            var obj = _Au.List();
           
            return View(obj);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            var objj = _Au.find(id);
            
            return View(objj);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author au)
        {
            try
            {
                _Au.add(au);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            var obj = _Au.find(id);
            return View(obj);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author obj)
        {
            try
            {
                // TODO: Add update logic here
                _Au.update(id, obj);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var obj = _Au.find(id);
            return View(obj);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author obj)
        {
            try
            {
                // TODO: Add delete logic here
                _Au.Del(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}