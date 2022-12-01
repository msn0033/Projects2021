using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.Model
{
    [Table("Finance_CurrentAsset_CheckStatus")]

    public class CheckStatus
    {
        public int Id { get; set; }


        [Required, StringLength(255)]
        public string CheckStatusEN { get; set; }


        [Required, StringLength(255)]
        public string CheckStatusAR { get; set; }


        public bool IsDefault { get; set; }//dufault Under Collaction
    }
}
