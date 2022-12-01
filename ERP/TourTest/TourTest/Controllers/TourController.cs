using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourTest.DataGeneral.InterFace;
using TourTest.ToursModule.Models;

namespace TourTest.Controllers
{
    public class TourController : Controller
    {
        private readonly IGenericRepository<Tour> _tourRepo;

        public TourController(IGenericRepository<Tour> TourRepo)
        {
            _tourRepo = TourRepo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _tourRepo.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tour to)
        {
            if(ModelState.IsValid)
            {
                _tourRepo.insert(to);
                return RedirectToAction(nameof(Index));
               
            }
            return View(to);
        }
    }
}
