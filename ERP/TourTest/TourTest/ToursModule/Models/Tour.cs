using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourTest.ToursModule.Models
{
    public class Tour
    {
       
        public int Id { get; set; }
        [Required,StringLength(maximumLength:100,MinimumLength =2,ErrorMessage ="اكبر من 3 واقل من 100 حرف")]
        public string NameTour { get; set; }
        [DataType(DataType.Date,ErrorMessage ="ارجو ادخال التاريخ بشكل صحيح")]
        public DateTime TourDate { get; set; }
        [Required,Column(TypeName ="decimal(18,2)"),Range(50,999999999999999999.99)]
        
        public decimal price { get; set; }
    }
}
