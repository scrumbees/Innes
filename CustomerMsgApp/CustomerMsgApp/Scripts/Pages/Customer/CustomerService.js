
app.service('customerService', function ($http) {
    this.GetAllCustomer = function () {
        return $http.get("/api/CustomerAPI/GetAllCustomer");
    };

    this.SendMessage = function () {
        var request = $http({
            method: "post",
            url: "/api/CustomerAPI/SendMessage",
            data: ''
        });
        return request;
    };

});
