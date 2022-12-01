using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.Model
{
    [Table("Finance_CurrentAsset_CheckHistory")]

    public class CheckHistory
    {
        public int Id { get; set; }

        [Required,StringLength(255)]
        public string ChkNum { get; set; }


        [StringLength(255)]
        public string TransID { get; set; }

        public string Description { get; set; }
        public DateTime? TransDate { get; set; }

    }
}
