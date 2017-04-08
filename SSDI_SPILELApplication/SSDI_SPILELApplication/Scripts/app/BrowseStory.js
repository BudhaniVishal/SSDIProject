var module = angular.module("BrowseStory", []);

var myController2 = module.controller("BrowseStoryController", function ($scope, $http, $window) {
    $scope.mydata = null;
    $scope.myfunc = {};
    $scope.myfunc.doClick = function (){
        // check to make sure the form is completely valid
        var myobj = $scope.mydata;
        var post = $http({
            method: "POST",
            url: "/Home/FilterStories",
            dataType: 'json/text',
            data: myobj,
            headers: { "Content-Type": "application/json" }
        });
        
    }
});

