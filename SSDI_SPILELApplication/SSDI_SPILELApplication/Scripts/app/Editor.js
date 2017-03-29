var module = angular.module("myapp", []);

var myController2 = module.controller("EditorController", function ($scope, $http, $window) {
    $scope.mydata = null;
    $scope.myfunc = {};
    $scope.submitForm = function (isValid) {
        // check to make sure the form is completely valid
        if (isValid) {
            var myobj = $scope.mydata;
            var post = $http({
                method: "POST",
                url: "/Home/CreateEditorStory",
                dataType: 'json/text',
                data: myobj,
                headers: { "Content-Type": "application/json" }
            });
            post.then(function successCallback(response) {
                $scope.MessageString = response.data;
            }), function errorCallback(response) {
                $scope.MessageString = "An error occured, Please try again.";
            };
        }
    }
});

