var module = angular.module("login", []);

var logincontrol = module.controller("loginController", function ($scope, $http, $window) {
    $scope.mydata = null;

    $scope.loginbtn = {};
    $scope.loginbtn.click = function () {
        var myobj = $scope.mydata;
        if (myobj != null) {
            //$http.post('/Home/AjaxMethod', myobj).then(function successCallback(data, status) {
            //    alert("Successful");
            //});
            var post = $http({
                method: "POST",
                url: "/Home/Login",
                dataType: 'json/text',
                data: myobj,
                headers: { "Content-Type": "application/json" }
            });
            post.then(function successCallback(response) {
                $window.alert("successful");
            }), function errorCallback(response) {
                $window.alert("error");
            };
        }
    }
});
