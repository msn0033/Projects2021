using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.ERPSettings.Model
{
    [Table(name: "Finance_Settings_Branch")]
    public class Branch//الفرع
    {
        public int Id { get; set; }
        public string BranchName { get; set; }//اسم الفرع

    }
}
