
var Mapping = {
    'JournalDetailsVM': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.JournalDetailsId);
        },
        create: function (options) {
            return new JournalDetails(options.data);
        }
    }
};



JournalDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);


    self.changeAccount = function () {// action  GetAccountDetails يتم استدعاء DropDownList عند تغيير

        var data = self;
        $.ajax({
            url: '/GLArea/Journal/GetAccountDetails/',
            type: 'POST',
            data: ko.toJSON(self.AccNum),
            contentType: "application/json",
            success: function (data) {
                if (data.Account !== null && data.Account !== undefined) {
                    self.UsedRate(data.Account.Currency.Rate);
                    self.CurrencyAbbr(data.Account.Currency.CurrencyAbbrev);
                    self.CurrencyId(data.Account.Currency.Id);
                }
                if (data.errors !== null) {
                    var ac = data.errors;
                }
            }
        });

    };

    self.ChangeDebit = function () {
        self.Side(0);
    };
    self.ChangeCredit = function () {
        self.Side(1);
    };


};

Journal = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.TotalDebit = ko.computed(function () {
        var total = 0;
        for (var i = 0, len = self.JournalDetailsVM().length; i < len; ++i) {
            if (self.JournalDetailsVM()[i].Side() === 0) {
                total = total + ((parseFloat(self.JournalDetailsVM()[i].Debit()) * parseFloat(self.JournalDetailsVM()[i].UsedRate())));

            }
        }

        return total;
    });

    self.TotalCredit = ko.computed(function () {
        var total = 0;
        for (var i = 0, len = self.JournalDetailsVM().length; i < len; ++i) {
            if (self.JournalDetailsVM()[i].Side() === 1) {
                total += ((parseFloat(self.JournalDetailsVM()[i].Credit()) * parseFloat(self.JournalDetailsVM()[i].UsedRate())));
            }
        }
        return total;
    });

    self.AddAccount = function () {
        var Acc = new JournalDetails({
            JournalDetailsId:0,
            AccNum: '',
            CurrencyAbbr: '',
            UsedRate: 0,
            Side: '',
            Debit: 0,
            Credit: 0,
            CurrencyId: 0,
            
        });
        self.JournalDetailsVM.push(Acc);
    }
   
    self.RemoveAcc = function (AccDetails) {
        self.JournalDetailsVM.remove(AccDetails);

    };

    self.Save = function () {
        var data = self;
        $.ajax({

            url: '/GLArea/Journal/SaveJournal/',
            type: 'POST',
            data: ko.toJSON(data),
            contentType: "application/json",
            success: function (data) {
                if (data.newLocation !== null && data.new_Location !== undefined)
                { window.new_Location = data.new_Location }
                if (data.eror !== null && data.eror !== undefined)
                { self.Messages(data.eror) }

            }
        });
    };

}