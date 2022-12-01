using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages.dialog
{
    public class UserAddNewBase : ComponentBase
    {
        [Parameter]
         public bool popup { get; set; } = false;
        [Parameter]
        public EventCallback<bool> popupfalse { get; set; }
        protected void showpopup()
        {
            popup = true;
        }
        protected async Task HidepopupAsync()
        {
            popup = false;
            await popupfalse.InvokeAsync(popup);
          
        }

    }
}

