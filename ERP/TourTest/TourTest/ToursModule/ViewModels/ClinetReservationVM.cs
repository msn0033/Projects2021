using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TourTest.ToursModule.Models;

namespace TourTest.ToursModule.ViewModels
{
    public class ClinetReservationVM:IValidatableObject
    {
        public ClinetReservationVM()
        {
            Tour = new Tour();
        }
        [Required]
        public Tour Tour { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [Range(100,Double.MaxValue)]
        public decimal PaymentAmount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if(this.PaymentAmount>Tour.price)
            {
                errors.Add(new ValidationResult("المبلغ المدفوع اكبر من سعر الرحلة"));
            }
            //if (this.PaymentAmount < 1)
            //{
            //    errors.Add(new ValidationResult("المبلغ غير صحيح"));
            //}
            return errors;
        }
    }
}
