using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgLix.Models;
using System.IO;
using Microsoft.AspNetCore.Session;

using Microsoft.AspNetCore.Http;


using Microsoft.AspNetCore.Hosting;
using StackExchange.Redis;
using FluentNHibernate.Conventions.Inspections;
using System.Security.Cryptography.X509Certificates;
using FluentNHibernate.Conventions;
using System.ServiceModel.Security;

namespace ProgLix.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IHostingEnvironment host;
        public static string username = "no";
        

        public CompaniesController(DataBaseContext context, IHostingEnvironment host)
        {
            _context = context;
            this.host = host;
        
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            if (username == "no")
                return RedirectToAction(nameof(Login));

            return View(await _context.Companies.ToListAsync());
        }    

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (username == "no")

                return RedirectToAction(nameof(Login));
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Com_Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            if (username == "no")
                return RedirectToAction(nameof(Login));

            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Com_Id,CName,Ctype," +
                                                      "Email,Curl,Edate,Password," +
                                                      "Capital," +"cLogo,confEmail," +
                                                      "confPassword")]Company company,
                                                                     IFormFile imgFile)
        {
            if (username == "no")
                return RedirectToAction(nameof(Login));
            if (ModelState.IsValid)
            {
                string path = "";
                string fullpath = "";
                if (imgFile.FileName.Length > 0)
                {
                    path = Path.Combine(host.WebRootPath + "\\images\\");
                    fullpath = Path.Combine(path + imgFile.FileName);
                    imgFile.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                company.cLogo = imgFile.FileName;
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (username == "no")
                return RedirectToAction(nameof(Login));
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(include:"Com_Id,CName,Ctype," +
                                                      "Email,Curl,Edate,Password," +
                                                      "Capital," +"cLogo,confEmail," +
                                                      "confPassword")] Company company, IFormFile imgFile)
        {
            if (username == "no")
                return RedirectToAction(nameof(Login));
            if (id != company.Com_Id)
            {
                return NotFound();
            }
            string path = "";
            if (imgFile.FileName.Length > 0)
            {
                path = Path.Combine(host.WebRootPath + "\\images\\" + imgFile.FileName);

                await imgFile.CopyToAsync(new FileStream(path, FileMode.Create));
            }
            company.cLogo = imgFile.FileName;

            var before = _context.Companies.AsNoTracking().Where(x => x.Com_Id == company.Com_Id).FirstOrDefault();
            company.confEmail = before.Email;
            company.confPassword = before.Password;
            company.Password = before.Password;

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    //_context.Entry(company).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Com_Id))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (username == "no")
                return RedirectToAction(nameof(Login));
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Com_Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (username == "no")
                return RedirectToAction(nameof(Login));

            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Com_Id == id);
        }

        public int Countt()
        {
            return _context.Companies.Count();
        }

        [HttpGet]
        public ActionResult Login()
        {
            username = "no";
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind("Email", "Password")] Company company)
        {
            var rec = _context.Companies.Where(x => x.Email == company.Email && x.Password == company.Password).ToList().FirstOrDefault();
            if(rec !=null)
            {
                username = rec.CName;
                return RedirectToAction(nameof(Index));
            }
            else
            ViewBag.error = "الرجاء التاكد من البريد الالكتروني او كلمة المرور";
            return View(company);
        }

        public ActionResult HomePage()
        {
            var recAll = _context.Companies.ToList();
            return View(recAll);
        }
        
    }
}
