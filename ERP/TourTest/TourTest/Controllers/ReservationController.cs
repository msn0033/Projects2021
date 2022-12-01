using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourTest.ToursModule.InterFace;
using TourTest.ToursModule.ViewModels;

namespace TourTest.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationManager _reservationManager;

        public ReservationController(IReservationManager reservationManager)
        {
            this._reservationManager = reservationManager;
        }
        public IActionResult TourList()
        {
            return View(_reservationManager.GetAllTours());
        }
        [HttpGet]
        public IActionResult AddReservation(int id)
        {
            return View(_reservationManager.NewReservation(id));
        }
        [HttpPost]
        public IActionResult AddReservation(ClinetReservationVM vm)
        {
            if (ModelState.IsValid)
            {
                _reservationManager.SaveNewReservation(vm);
                return RedirectToAction(nameof(TourList));
            }
            return View(vm);
            
        }
    }
}
