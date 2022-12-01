using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TourTest.ToursModule.Models;

namespace TourTest.ToursModule.Models
{
    public class Reservation
    {
        public int ClientId { get; set; }
        public int TourId { get; set; }
        [Column(TypeName = "decimal(18,2)"), Range(50, 999999999999999999.88)]
        public decimal Paid { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client ClientDetails { get; set; }
        [ForeignKey(nameof(TourId))]
        public Tour TourDeatils { get; set; }

        
    }
}
