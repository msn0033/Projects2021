using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace PragimTech.Components
{
    public class ConfirmBase : ComponentBase
    {
        [Parameter]
        public string ConfirmationTitle { get; set; }// message title
        [Parameter]
        public string ConfirmationMessage { get; set; }//message Body 
        public bool ShowConfirmation { get; set; }
        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }


        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }
        protected async  Task OnConfirmationChanged(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);

        }  
    }
}
