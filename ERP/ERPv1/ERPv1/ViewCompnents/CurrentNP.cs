using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Interfaces;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ViewCompnents
{
    public class CurrentNP:ViewComponent
    {
        private readonly INotesPayableManager _notesPayableManager;

        public CurrentNP(INotesPayableManager notesPayableManager)
        {
            _notesPayableManager = notesPayableManager;     
        }
        public IViewComponentResult Invoke()
        {

            var vm = _notesPayableManager.GetNPWithStatus(NotesPayableStatusEnum.UnderCollection);
            return View(vm);
        }
    }
}
