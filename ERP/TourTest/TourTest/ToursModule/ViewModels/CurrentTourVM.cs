using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourTest.ToursModule.ViewModels
{
    public class CurrentTourVM
    {
        public int Id { get; set; }
        
        public string NameTour { get; set; }
     
        public string TourDate { get; set; }
       
        public decimal Price { get; set; }
        public int ReservationCount { get; set; }
    }
}
