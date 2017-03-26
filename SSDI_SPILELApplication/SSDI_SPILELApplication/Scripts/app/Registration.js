var module = angular.module("Registration", []);

var myController2 = module.controller("RegistrationController", function ($scope, $http, $window) {
    $scope.mydata = null;
    $scope.myfunc = {};
    $scope.myfunc.doClick = function () {
        var myobj = $scope.mydata;
        if (myobj != null) {
            //$http.post('/Home/AjaxMethod', myobj).then(function successCallback(data, status) {
            //    alert("Successful");
            //});
            
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
