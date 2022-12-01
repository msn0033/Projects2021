using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgLix.Models;
using ProgLix.ViewModels;

namespace ProgLix.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataBaseContext _context;
        

        public ProductsController(DataBaseContext context)
        {
            _context = context;
           
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (CompaniesController.username == "no")
                return RedirectToAction("Login", "Companies");
            return View(await _context.Products.Include("Category").Include("company").ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (CompaniesController.username == "no")
                return RedirectToAction("Login", "Companies");

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (CompaniesController.username == "no")
                return RedirectToAction("Login", "Companies");

            var ret = new ProductViewModel();
            ret.companyselect = new SelectList(_context.Companies.ToList(), "Com_Id", "CName");
            ret.Category = _context.Categorys.ToList();
            
            return View(ret);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProId,ProName,unitPrice,CategoryId,companyselectId,uploadFile")] ProductViewModel ProductViewModel)
        {
            if (CompaniesController.username == "no")
                return RedirectToAction("Login", "Companies");

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    ProName = ProductViewModel.ProName,
                    unitPrice= ProductViewModel.unitPrice,
                    companyId= ProductViewModel.companyselectId,
                    CategoryId= ProductViewModel.CategoryId,
                    logo= ProductViewModel.uploadFile.FileName,
                    
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ProductViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (CompaniesController.username == "no")
                return RedirectToAction("Login", "Companies");

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProId,ProName,unitPrice,logo")] Product product)
        {
            if (CompaniesController.username == "no")
                return RedirectToAction("Login", "Companies");

            if (id != product.ProId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProId))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (CompaniesController.username == "no")
                return RedirectToAction("Login", "Companies");

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProId == id);
        }
    }
}
