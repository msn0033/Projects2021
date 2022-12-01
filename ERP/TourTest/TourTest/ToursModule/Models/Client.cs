using com.sun.org.apache.xerces.@internal.impl.xpath.regex;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourTest.ToursModule.Models
{
    [Table("ClientTbl")]
    public class Client
    {
        
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 100, MinimumLength = 3, ErrorMessage = "اكثر من 3 واقل من 100 حرف")]
        public string NameClient { get; set; }
        [Required, RegularExpression("^[0-9]*$")]
        public string Phone { get; set; }

    }
}
