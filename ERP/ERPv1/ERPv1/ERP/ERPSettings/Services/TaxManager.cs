using ERPv1.Data;
using ERPv1.ERP.ERPSettings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.ERPSettings.Services
{
    public class TaxManager : ITaxManager
    {
        private readonly ApplicationDbContext _db;

        public TaxManager(ApplicationDbContext db)
        {

            _db = db;
        }
        public decimal VatAmount() => _db.VATs.FirstOrDefault(x => x.Id == 1).VatRate;//ارجاع الضريبة

    }
}
