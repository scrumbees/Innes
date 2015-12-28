

var _CustomorList = [];
app.controller('customercontroller', function ($scope, customerService, $cookies) {

    GetAllCustomer();
    function GetAllCustomer() {
        var promisePost = customerService.GetAllCustomer();
        promisePost.then(function (p1) { $scope.Customers = p1.data }, function (err) {
            console.log("Error");
        });
    }

    $scope.SendMessage = function () {
        var send = customerService.SendMessage();
    }

});
