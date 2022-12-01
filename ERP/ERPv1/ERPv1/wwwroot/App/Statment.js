StatmentContainerVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
    self.GetStatment = function () {
        var data = self.StatmentParams;
        $.ajax({

            url: self.ReportURL(),
            type: 'POST',
            data: ko.toJSON(data),
            contentType: "application/json",
            success: function (data) {
                self.StatmentTransaction.removeAll();//مسح الجدول
                ko.mapping.fromJS(data.result, {}, self);

            }
        });
    };



}
