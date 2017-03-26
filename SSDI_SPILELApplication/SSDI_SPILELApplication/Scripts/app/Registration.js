var module = angular.module("Registration", []);

var controller = module.controller("RegistrationController", function ($scope, $http, $window) {
    $scope.mydata = null;
    $scope.registerButton = {};
    $scope.registerButton.doClick = function () {
        var myobj = $scope.mydata;
        if (myobj != null) {
            
            var post = $http({
                method: "POST",
                url: "/Home/UserRegistration",
                dataType: 'json/text',
                data: myobj,
                headers: { "Content-Type": "application/json" }
            });
            post.then(function successCallback(response) {
                $scope.MessageString = response.data;
                //$window.alert(response.data);
            }), function errorCallback(response) {
                $scope.MessageString = "An error occured, Please try again.";
            };
        }
    }
});
