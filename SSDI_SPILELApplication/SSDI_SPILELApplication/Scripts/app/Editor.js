var module = angular.module("myapp", []);

 var myController2 = module.controller("EditorController", function ($scope, $http, $window) {
     $scope.mydata = {};
     $scope.mydata.doClick = function () {
         var myobj = $scope.mydata;
         //$http.post('/Home/AjaxMethod', myobj).then(function successCallback(data, status) {
         //    alert("Successful");
         //});
             var post = $http({
                 method: "POST",
                 url: "/Home/AjaxMethod",
                 dataType: 'json/text',
                 data: myobj,
                 headers: { "Content-Type": "application/json" }
             });
             post.then(function successCallback(response) {
                 $window.alert("successful");
             }),function errorCallback(response) {
                 $window.alert("error");
             };   
     }
 });
