NPVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    //طريقة دفع  نقداً - تحويل بنك - شيك 
    self.ChangePaymentMethod = function () {
        let Payment = self.PaymentDetails.PaymentMethod();
        if (Payment === "20") {
            self.PaymentDetails.IsSafe(true);
            self.PaymentDetails.IsBank(false);
            self.PaymentDetails.IsCheck(false);
        }
        if (Payment === "30") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(true);
            self.PaymentDetails.IsCheck(false);
        }
        if (Payment === "40") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(true);
            self.PaymentDetails.IsCheck(true);
        }
    }
// شيك تم تحصيلة تم دفعة للمورد
    self.Collect = function (np) {
        //np  هو الصف الذي بختارة من المصفوفة
        var data = ko.toJSON(np);
        $.ajax({

            url: '/Expenditure/NP/CollectCheck',
            type: 'POST',
            data:data,
            contentType: "application/json",
            success: function (dataBack) {
                if (dataBack.newLocation != null && dataBack.newLocation != undefined) {
                    window.location = newLocation;
                }
                if (dataBack.problem != null && dataBack.problem != undefined) {
                    self.Messages(dataBack.problem);
                }
            }
        });
    }
    //تحويل الشيك الى دفع نقدي
    self.MoveToCash = function (np) {
        //np  هو الصف الذي بختارة من المصفوفة
        var data = ko.toJSON(np);
        $.ajax({

            url: '/Expenditure/NP/MoveToCashCollection',
            type: 'POST',
            data: data,
            contentType: "application/json",
            success: function (dataBack) {
                if (dataBack.newLocation != null && dataBack.newLocation != undefined) {
                    window.location = dataBack.newLocation;
                }
                if (dataBack.problem != null && dataBack.problem != undefined) {
                    self.Messages(dataBack.problem);
                }
            }
        });
    }
  
    //تحديد الشيك النقدي الذي سادفعة
    self.SelectNote = function (np) {
        self.SelectedNote.ChkNum(np.ChkNum());
        self.SelectedNote.WritingDate(np.WritingDate());
        self.SelectedNote.DueDate(np.DueDate());
        self.SelectedNote.AmountLocal(np.AmountLocal());
        self.SelectedNote.AmountForgin(np.AmountForgin());
        self.SelectedNote.CurrencyId(np.CurrencyId());
        self.SelectedNote.CurrencyAbbrev(np.CurrencyAbbrev());
        self.SelectedNote.BankAccountNum(np.BankAccountNum());
        self.SelectedNote.BankName(np.BankName());
        self.SelectedNote.SupplierId(np.SupplierId());
        self.SelectedNote.SupplierName(np.SupplierName());
        self.SelectedNote.Paid(np.Paid());

        let Safe = $('#PaymentDetails_SafeAccNum');
        let Bank = $('#PaymentDetails_BankAccNum');
        let SafeUrl = '/List/GetSafeAccount';
        let BankUrl = '/List/GetBankAccount';
        //PaymentDetails_SafeAccNum
        //PaymentDetails_BankAccNum

        $.getJSON(SafeUrl, { Id: np.CurrencyId() }, function (response) {
            Safe.empty();//remove dorpdownlist safe
            $("<option/>", {
                val: "",
                text: "--اختر--"
            }).appendTo(Safe);

            $.each(response, function (index, item) {
                $("<option/>", {
                    val: item.Value,
                    text: item.Text
                }).appendTo(Safe);
            });


        });
        $.getJSON(BankUrl, { Id: np.CurrencyId() }, function (response) {
            Bank.empty();//remove dorpdownlist safe
            $("<option/>", {
                val: "",
                text: "--اختر--"
            }).appendTo(Bank);

            $.each(response, function (index, item) {
                $("<option/>", {
                    val: item.Value,
                    text: item.Text
                }).appendTo(Bank);
            });


        });

    }//end selectedNote

    self.Save = function () {
        self.IsWaitingAreaVisible(true);
        self.IsDetailAreaVisible(false);
        var data = ko.toJSON(self);
        $.ajax({

            url: '/Expenditure/NP/CollectCashCollection',
            type: 'POST',
            data: data,
            contentType: "application/json",
            success: function (dataBack) {
                if (dataBack.newLocation != null && dataBack.newLocation != undefined) {
                    window.location = dataBack.newLocation;
                }
                if (dataBack.Problem != null && dataBack.Problem != undefined) {
                    self.IsMessageAreaVisible(true);
                    self.IsDetailAreaVisible(true);
                    self.IsWaitingAreaVisible(false);
                    self.Messages(dataBack.Problem);
                }
            }
        });


    }//end Save
}