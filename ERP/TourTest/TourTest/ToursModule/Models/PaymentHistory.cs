using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TourTest.ToursModule.Models;

namespace TourTest.ToursModule.Models
{
    public class PaymentHistory
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public int TourId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client ClientDetails { get; set; }
        [ForeignKey(nameof(TourId))]
        public Tour TourDeatils { get; set; }

        public DateTimeOffset PaymentDate { get; set; }

        [Column(TypeName = "decimal(18,2)"),Range(50,999999999999999999.88)]
        public decimal PaidAmount { get; set; }
    }
}
    

