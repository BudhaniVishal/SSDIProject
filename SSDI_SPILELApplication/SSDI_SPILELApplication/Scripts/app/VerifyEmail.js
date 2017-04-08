var module = angular.module("verifyEmail", []);

var logincontrol = module.controller("verifyEmailController", function ($scope, $http, $window) {
    $scope.mydata = null;
    $scope.verifyEmailbtn = {};
    $scope.submitForm = function (isValid) {
        if (isValid) {
            var myobj = $scope.mydata;
            var post = $http({
                method: "POST",
                url: "/Home/verifyEmail",
                dataType: 'json/text',
                data: myobj,
                headers: { "Content-Type": "application/json" }
            });

            post.then(function successCallback(response) {
                if (response.data === "Registered User !!") {
                    $window.location.href = '/Account/ForgotPasswordConfirmation';

                } else {
                    $scope.MessageString = response.data+"Enter registered Email Address";
                    //$window.location.href = '/Home/Index';
                    //$window.location.href = '/Home';
                }

            }), function errorCallback(response) {
                $scope.MessageString = "An error occured, Please try again.";
            };
        }
    }
});