using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.Model
{
    [Table("Finance_CurrentAsset_CheckLocation")]

    public class CheckLocation
    {
        public int Id { get; set; }


        [StringLength(255)]
        public string CheckLocationEN { get; set; }


        [StringLength(255)]
        public string CheckLocationAR { get; set; }


        public bool IsDefault { get; set; }//dufault Safe

    }
}
