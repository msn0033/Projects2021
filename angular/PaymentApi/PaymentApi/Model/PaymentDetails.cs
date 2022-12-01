using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Model
{
    public class PaymentDetails
    {
        [Key]
        public int paymentDetalisId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string cardOwnerName { get; set; }
        [Column(TypeName = "nvarchar(16)")]
        public string cardNumber { get; set; }
        [Column(TypeName = "nvarchar(3)")]
        public string securityCode { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string expirationDate { get; set; }
    }
}
