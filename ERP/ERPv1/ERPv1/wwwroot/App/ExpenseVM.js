ExpenseVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.SelectCurrency = function (balance) {


        let Safe = $('#PaymentDetails_SafeAccNum');
        let Bank = $('#PaymentDetails_BankAccNum');
        let SafeUrl = '/List/GetSafeAccount';
        let BankUrl = '/List/GetBankAccount';
        //PaymentDetails_SafeAccNum
        //PaymentDetails_BankAccNum

        $.getJSON(SafeUrl, { Id: self.ExpenseDetails.CurrencyId() }, function (response) {
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
        $.getJSON(BankUrl, { Id: self.ExpenseDetails.CurrencyId() }, function (response) {
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


    }

    self.ChangePaymentMethod = function () {
        let Payment = self.PaymentDetails.PaymentMethod();
        if (Payment === "20") {
            self.PaymentDetails.IsSafe(true);
            self.PaymentDetails.IsBank(false);
            self.PaymentDetails.IsCheck(false);
            self.PaymentDetails.IsCustody(false);
        }
        if (Payment === "30") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(true);
            self.PaymentDetails.IsCheck(false);
            self.PaymentDetails.IsCustody(false);
        }
        if (Payment === "40") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(true);
            self.PaymentDetails.IsCheck(true);
            self.PaymentDetails.IsCustody(false);
        }
        if (Payment === "50") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(false);
            self.PaymentDetails.IsCheck(false);
            self.PaymentDetails.IsCustody(true);
        }

    }
    self.ExpenseDetails.TotalWithVAT = ko.pureComputed({
        read: function () {
            let total = 0;
            total = parseFloat((parseFloat(self.ExpenseDetails.Amount()) + parseFloat(self.ExpenseDetails.VATAmount())).toFixed(2));
            return total;
        },
        write: function (value) { return 0; },
        owner:self
    });
    self.ApplyVAT = function () {
        if (self.VAT.IsVAT() === true) {
            $.ajax({
                url: '/Expenditure/Supplier/GetVatAmount',
                success: function (data) {
                    if (data.VAT !== null && data.VAT !== undefined) {
                        self.VAT.VATRate(data.VAT);
                        self.ExpenseDetails.VATAmount(parseFloat((self.ExpenseDetails.Amount() * self.VAT.VATRate()).toFixed(2)));

                    }
                },
            });
        }
        else
            self.ExpenseDetails.VATAmount(0);
    }


    self.Save = function () {
        self.IsWaitingAreaVisible(true);
        self.IsDetailAreaVisible(false);
        var data = ko.toJSON(self);
        $.ajax({
            url: self.SaveURL(),
            type: "POST",
            data: data,
            contentType: "application/json",
            success: function (dataBack) {
                if (dataBack.newLocation !== null && dataBack.newLocation !== undefined) {
                    window.location = dataBack.newLocation;
                }
                if (dataBack.Problem !== null && dataBack.Problem !== undefined) {
                    self.IsMessageAreaVisible(true);
                    self.IsDetailAreaVisible(true);
                    self.IsWaitingAreaVisible(false);
                    self.Messages(dataBack.Problem);
                }
            }

        });
    };

}
