CheckCollectCash = function (data) {
    var self = this;
    ko.mapping.fromJS(data, [], self);

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
            self.PaymentDetails.IsBank(false);
            self.PaymentDetails.IsCheck(true);
        }
    }
 
    self.Save = function () {
       
        $.ajax({
            url: "/SalesArea/NR/SaveCollectCashCheck",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.newLocation !== null || data.newLocation !== undefined) {
                    window.location = data.newLocation;
                }

                if (data.errors !== null) {
                  
                }
            }
        });

    };
    
};