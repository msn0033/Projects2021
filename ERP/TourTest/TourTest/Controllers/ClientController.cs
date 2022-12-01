using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourTest.DataGeneral.InterFace;

using TourTest.ToursModule.Models;

namespace TourTest.Controllers
{
    public class ClientController : Controller
    {

        private readonly IGenericRepository<Client> _clientRepo;

        public ClientController(IGenericRepository<Client> clientRepo)
        {
            _clientRepo = clientRepo;
        }
        public async Task <IActionResult> Index()
        {
            return View(await _clientRepo.GetAllAsync());
        }
        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientRepo.insert(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

    }
}
