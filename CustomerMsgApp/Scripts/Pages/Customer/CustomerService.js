
app.service('customerService', function ($http) {

    this.SendMessage = function (Customer) {
        var request = $http({
            method: "post",
            url: "/api/CustomerAPI/CheckPassword",
            data: Customer
        })
        return request;
    };

    this.GetTourOpCode = function () {
        return $http.get("/api/CustomerAPI/GetTourOpCode");
    };
    this.GetDeparturePoint = function () {
        return $http.get("/api/CustomerAPI/GetDeparturePoint");
    };
    this.GetArrivalPoint = function () {
        return $http.get("/api/CustomerAPI/GetArrivalPoint");
    };
    this.GetTravelDirection = function () {
        return $http.get("/api/CustomerAPI/GetTravelDirection");
    };
    this.GetTransportCarrier = function () {
        return $http.get("/api/CustomerAPI/GetTransportCarrier");
    };
    this.GetTransportNumber = function () {
        return $http.get("/api/CustomerAPI/GetTransportNumber");
    };
    this.GetTransportType = function () {
        return $http.get("/api/CustomerAPI/GetTransportType");
    };
    this.GetTransportChain = function () {
        return $http.get("/api/CustomerAPI/GetTransportChain");
    };
    this.GetCountryName = function () {
        return $http.get("/api/CustomerAPI/GetCountryName");
    };
    this.GetResortName = function () {
        return $http.get("/api/CustomerAPI/GetResortName");
    };
    this.GetAccommodationName = function () {
        return $http.get("/api/CustomerAPI/GetAccommodationName");
    };
});
