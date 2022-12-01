using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main
{
    [Table("Finance_CurrentAsset_Inventory_Main_RepositoryStoreItem")]

    public class RepositoryStoreItem// المستودع و الصنف 
    {
        public int Id { get; set; }
        public int RepositoryId { get; set; }//حركة المستودع
        [ForeignKey("RepositoryId")]
        public Repository Repository { get; set; }

        public int StoreItemId { get; set; }// حركة الصنف
        [ForeignKey("StoreItemId")]
        public StoreItem StoreItem { get; set; }
        public int Qty { get; set; }//كمية الصنف في المستودع

        public string Location { get; set; }// موقع النصف في المستودع


    }
}
