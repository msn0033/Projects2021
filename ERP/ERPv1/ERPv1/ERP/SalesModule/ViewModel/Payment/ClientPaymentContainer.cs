using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel.Payment
{
    public class ClientPaymentContainer: IValidatableObject
    {
        public ClientPaymentContainer()
        {
            ClientData = new ClientData();
            ClientBalanceDetails = new List<ClientBalanceDetails>();
            SelectedBalance = new ClientBalanceDetails();
            PaymentDetails = new ClientPaymentDetails();
            PaymentDetails.PaymentMethod = Model.ClientPaymentMethodEnum.Safe;
            PaymentDetails.IsSafe = true;// defualt =>IsSafe=true  اظهار جميع الخزنة تلقائي 

            Messages = new List<string>();
            IsDetailAreaVisible = true;
            IsWaitingAreaVisible = false;
            IsMessageAreaVisible = false;
        }
        public ClientData ClientData { get; set; }
        public List<ClientBalanceDetails> ClientBalanceDetails { get; set; }
        public ClientBalanceDetails SelectedBalance { get; set; }
        public ClientPaymentDetails PaymentDetails { get; set; }


        public List<string> Messages { get; set; }//حفظ الاخطاء فيها

        public bool IsWaitingAreaVisible { get; set; }//Upload Data =>load
        public bool IsDetailAreaVisible { get; set; }// Normal View
        public bool IsMessageAreaVisible { get; set; } //Show In Case an Error

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var mes = new List<ValidationResult>();
            if (string.IsNullOrEmpty(PaymentDetails.CheckNum))
                mes.Add(new ValidationResult("رجاء ادخال رقم الشيك"));
                   
            return mes;
        }
    }


}
