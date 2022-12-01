
var Mapping = {
    'PurchasesDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.StoreItemId);
        },
        create: function (options) {
            return new PurchDetails(options.data);
        }
    }
};

PurchaseVM= function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.AddNewItem = function () {
        var NewItem = new PurchDetails({
            StoreItemId: 0,
            CurrentQTY: 0,
            QTY: 0,
            UnitPrice: 0,
            Total: 0,
            SN: ''
        });
        self.PurchasesDetails.push(NewItem);
    };
    self.RemoveItem = function (Sto) {
        self.PurchasesDetails.remove(Sto);
    };

    self.PurchaseSummary.TotalAmount = ko.pureComputed({

        read: function () {
            let total = 0;
            for (var i = 0, len = self.PurchasesDetails().length; i < len; i++) {
                total += parseFloat((parseFloat(self.PurchasesDetails()[i].QTY()) * parseFloat(self.PurchasesDetails()[i].UnitPrice())).toFixed(2));
            }

            return total;
        },
        write: function(value) { return 0; },
        owner:self
    });
    self.PurchaseSummary.TotalAmountWithVAT = ko.pureComputed({

        read: function () {
            let total = 0;
            total = parseFloat((parseFloat(self.PurchaseSummary.TotalAmount()) + parseFloat(self.PurchaseSummary.VATAmount())).toFixed(2));
            return total;
        },
        write: function (value) { return 0 },
        owner:self

    });
    self.ApplyVAT = function () {
        if (self.PurchaseSummary.IsVAT() === true) {
            $.ajax({
                url: '/Expenditure/Supplier/GetVatAmount',
                success: function (data) {
                    if (data.VAT !== null && data.VAT !== undefined) {
                        self.PurchaseSummary.VATRate(data.VAT);
                        self.PurchaseSummary.VATAmount(parseFloat((self.PurchaseSummary.TotalAmount() * self.PurchaseSummary.VATRate()).toFixed(2)));

                    }
                },
            });
        }
        else 
            self.PurchaseSummary.VATAmount(0);     
    }

    self.Save = function () {

        self.IsWaitingAreaVisible(true); 
        self.IsDetailAreaVisible(false);
        
        var formData = new FormData();
        if (document.getElementById("InvoiceFile").files.length > 0) {
            var Invoice_File = $('#InvoiceFile').get(0).files[0];
            formData.append('InvoFile', Invoice_File);
        }
        var viewmodel = ko.toJSON(self);
        formData.append('vm', viewmodel);

        $.ajax({

            url: self.SaveURL(),
            type: 'post',
            contentType: false,
            processData: false,
            data: formData,
            success: function (databack) {
                if (databack.newLocation !== null && databack.newLocation !== undefined) {
                    window.location = databack.newLocation;
                }

                if (databack.Problem !== null && databack.Problem !== undefined) {

                    self.IsMessageAreaVisible(true);
                    self.IsDetailAreaVisible(true);
                    self. IsWaitingAreaVisible(false); 
                    self.Messages(databack.Problem);
                }

            }
        });
            
    }
}


PurchDetails = function (data) {
  
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.ChangeItem = function () {
        var itemId = self.StoreItemId();
        $.ajax({
            url: '/Expenditure/Supplier/GetStoreItemById/' + itemId,
            
            type: 'POST',
            contentType: 'applicatiob/json',
            data: ko.toJSON(itemId),
            success: function (data) {
                if (data.CurrentQty !== null && data.CurrentQty !== undefined) {
                    self.CurrentQTY(data.CurrentQty);
                    
                }
            },
        });
    };

    self.Total = ko.computed(function () {
        return parseFloat((parseFloat(self.QTY()) * parseFloat(self.UnitPrice())).toFixed(2));
    });
};
