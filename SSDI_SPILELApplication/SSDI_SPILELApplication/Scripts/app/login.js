var module = angular.module("login", []);

var logincontrol = module.controller("loginController", function ($scope, $http, $window) {
    $scope.mydata = null;
    $scope.loginbtn = {};
    $scope.submitForm = function (isValid) {
        if (isValid) {
            var myobj = $scope.mydata;
            var post = $http({
                method: "POST",
                url: "/Home/Login",
                dataType: 'json/text',
                data: myobj,
                headers: { "Content-Type": "application/json" }
            });

            post.then(function successCallback(response) {
                if (response.data === "Writer Login Successful !!" ||
                    response.data === "Editor Login Successful !!") {
                    $window.location.href = '/Home/BrowseStories';

                } else {
                    $scope.MessageString = response.data;
                    //$window.location.href = '/Home';
                }

            }), function errorCallback(response) {
                $scope.MessageString = "An error occured, Please try again.";
            };
        }
    }
});
