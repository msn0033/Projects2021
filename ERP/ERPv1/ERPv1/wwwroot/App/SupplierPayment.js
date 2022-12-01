
SupplierPaymentVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
    self.SelectBalance = function (balance) {
        self.SelectedBalance.CurrencyId(balance.CurrencyId());
        self.SelectedBalance.Amount(balance.Amount);
        self.SelectedBalance.LocalAmount(balance.LocalAmount);
        self.SelectedBalance.Rate(balance.Rate);
        self.SelectedBalance.CurrencyAbbr(balance.CurrencyAbbr);

        let Safe = $('#PaymentDetails_SafeAccNum');
        let Bank = $('#PaymentDetails_BankAccNum');
        let SafeUrl = '/List/GetSafeAccount';
        let BankUrl = '/List/GetBankAccount';
        //PaymentDetails_SafeAccNum
        //PaymentDetails_BankAccNum

        $.getJSON(SafeUrl, { Id: balance.CurrencyId() }, function (response) {
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
        $.getJSON(BankUrl, { Id: balance.CurrencyId() }, function (response) {
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

    self.Save = function () {
        self.IsWaitingAreaVisible(true);
        self.IsDetailAreaVisible(false);
        var data = ko.toJSON(self);
        $.ajax({
            url: '/Expenditure/Supplier/SaveSupplierPayment',
            type: "POST",
            data: data,
            contentType: "application/json",
            success:function (dataBack)
            {
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
    
};

