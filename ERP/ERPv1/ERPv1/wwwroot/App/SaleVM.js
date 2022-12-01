
var Mapping = {
    'SalesItemDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.StoreItemId);
        },
        create: function (options) {
            return new SalesItemDetailsVM(options.data);
        }
    }
};

SaleVM= function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.AddNewItem = function () {
        var NewItem = new SalesItemDetailsVM({
            StoreItemId: 0,
            CurrentQTY: 0,
            QTY: 0,
            UnitPrice: 0,
            Total: 0,
            SN: ''
        });
        self.SalesItemDetails.push(NewItem);
    };
    self.RemoveItem = function (Sto) {
        self.SalesItemDetails.remove(Sto);
    };

    self.SalesSummary.Amount = ko.pureComputed({

        read: function () {
            let total = 0;
            for (var i = 0, len = self.SalesItemDetails().length; i < len; i++) {
                total += parseFloat((parseFloat(self.SalesItemDetails()[i].QTY()) * parseFloat(self.SalesItemDetails()[i].UnitPrice())).toFixed(2));
            }

            return total;
        },
        write: function(value) { return 0; },
        owner:self
    });
    self.SalesSummary.TotalWithVAT = ko.pureComputed({

        read: function () {
            let total = 0;
            total = parseFloat((parseFloat(self.SalesSummary.Amount()) + parseFloat(self.SalesSummary.VATAmount())).toFixed(2));
            return total;
        },
        write: function (value) { return 0 },
        owner:self

    });
    self.ApplyVAT = function () {
        if (self.SalesSummary.IsVAT() === true) {
            $.ajax({
                url: '/Expenditure/Supplier/GetVatAmount',
                success: function (data) {
                    if (data.VAT !== null && data.VAT !== undefined) {
                        self.SalesSummary.VATRate(data.VAT);
                        self.SalesSummary.VATAmount(parseFloat((self.SalesSummary.Amount() * self.SalesSummary.VATRate()).toFixed(2)));

                    }
                },
            });
        }
        else 
            self.SalesSummary.VATAmount(0);     
    }

    self.Save = function () {

        self.IsWaitingAreaVisible(true); 
        self.IsDetailAreaVisible(false);
        
      
      
        var data = ko.toJSON(self);
        $.ajax({
            
            url: self.SaveURL(),
            type: 'post',
            contentType: "application/json",
            data:data,
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


SalesItemDetailsVM = function (data) {
  
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
