// app.js
// create angular app
var validationApp = angular.module('ContributeToStory', []);

// create angular controller
validationApp.controller('ContributeToStoryController', function ($scope, $http) {
    $scope.mydata = null;
    $scope.reset = function () {
       
        // Todo: Reset field to pristine state, its initial state!
    };
    // function to submit the form after all validation has occurred            
    $scope.submitForm = function (isValid) {
        
        // check to make sure the form is completely valid
        if (isValid) {
            
                var myobj = $scope.mydata;
                var post = $http({
                    method: "POST",
                    url: "/Home/ContributeStorySave",
                    dataType: 'json/text',
                    data: myobj,
                    headers: { "Content-Type": "application/json" }
                });
            post.then(function successCallback(response) {
                $scope.mydata.textAnswer = '';
                    $scope.MessageString = "Content for the story saved Successfully!";
                }), function errorCallback(response) {
                    $scope.MessageString = "An error occured, Please try again.";
                };
            }
            

    };

});