using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Core.Interfaces;
//using Microsoft.Extensions.Hosting;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
       
        private readonly IUnitOfWork<PortfolioItem> _portfolioitem;
        private readonly IHostingEnvironment _host;

        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portfolioitem, IHostingEnvironment host)
        {
            _portfolioitem = portfolioitem;
            _host = host;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        { 
            return View(_portfolioitem.Entity.GetAll());
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolioitem.Entity.GetById(id);
               
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( PortfilioitemViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.File !=null)
                {
                    var upload = Path.Combine(_host.WebRootPath,@"img\portfolio");
                    var fullpath = Path.Combine(upload, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath,FileMode.Create));
                }

                PortfolioItem port = new PortfolioItem
                {
                    Id = model.Id,
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };

                _portfolioitem.Entity.Insert(port);
                _portfolioitem.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PortfolioItem portfolioItem = _portfolioitem.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            PortfilioitemViewModel PortfilioitemViewModel = new PortfilioitemViewModel
            {
        
                ProjectName = portfolioItem.ProjectName,
                Description = portfolioItem.Description,
                ImageUrl = portfolioItem.ImageUrl
            };
            return View(PortfilioitemViewModel);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfilioitemViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        var upload = Path.Combine(_host.WebRootPath, @"img\portfolio");
                        var fullpath = Path.Combine(upload, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }

                    PortfolioItem PortfolioItem = new PortfolioItem
                    {
                        Id=model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };
                    _portfolioitem.Entity.Update(PortfolioItem);
                    _portfolioitem.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PortfolioItem PortfolioItem = _portfolioitem.Entity.GetById(id);
            if (PortfolioItem == null)
            {
                return NotFound();
            }

            return View(PortfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfolioitem.Entity.Delete(id);
            _portfolioitem.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {
            return _portfolioitem.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
