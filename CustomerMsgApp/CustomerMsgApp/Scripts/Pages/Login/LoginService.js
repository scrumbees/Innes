
app.service('loginService', function ($http) {
    this.Login = function (email, password) {
        var Login = {
            UserName: email,
            Password: password
        };

        var request = $http({
            method: "post",
            url: "/api/CustomerAPI/UserLogin",
            data: Login
        });
        return request;      
    }
});