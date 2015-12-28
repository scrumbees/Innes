
app.service('customerService', function ($http) {

    this.SendMessage = function (Customer) {
        var request = $http({
            method: "post",
            url: "/api/CustomerAPI/SendMessage",
            data: Customer
        })
        return request;
    };

});
