var module = angular.module("updatepassword", []);

var logincontrol = module.controller("resetpasswordController", function ($scope, $http, $window) {
    $scope.mydata = null;
    $scope.updatepasswordbtn = {};
    $scope.submitForm = function (isValid) {
        if (isValid) {
            var myobj = $scope.mydata;
            var post = $http({
                method: "POST",
                url: "/Home/updatepassword",
                dataType: 'json/text',
                data: myobj,
                headers: { "Content-Type": "application/json" }
            });

            post.then(function successCallback(response) {
                if (response.data === "Password Updated Successfully !!") {
                    $scope.MessageString = response.data;
                    //window.alert("Password Updated Successfully !! Please login.");                    
                    $window.location.href = '/Account/ResetPasswordConfirmation';
                } else {
                    $scope.MessageString = response.data;
                    //$window.location.href = '/Home/index';
                    //$window.location.href = '/Home';
                }

            }), function errorCallback(response) {
                $scope.MessageString = "An error occured, Please try again.";
            };
        }
    }
});