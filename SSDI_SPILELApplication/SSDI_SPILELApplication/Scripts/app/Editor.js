var module = angular.module("myapp", []);

var myController2 = module.controller("EditorController", function ($scope, $http) {
    $scope.mydata = null;
    $scope.myfunc = {};
    $scope.checkErr = function (startDate, endDate) {
        $scope.errMessage = '';
        $scope.curDate = new Date();
        //debugger;
        var difference = Date.parse($scope.curDate) - Date.parse(startDate);
        if (Date.parse(endDate) < Date.parse(startDate)) {
            $scope.errMessage = 'End Date should be greater than start date';
            $scope.isValid = false;
        }
        else if (Date.parse(startDate) < Date.parse($scope.curDate)) {
            if (!(difference < 86400000)) {
                $scope.errMessage = 'Start date should not be lesser than today';
                $scope.isValid = false;
            }
        }
        else if (Date.parse(startDate) > Date.parse($scope.curDate)) {
            $scope.errMessage = 'Start date cannot be greater than today';
            $scope.isValid = false;
        }

    };
    $scope.submitForm = function (isValid) {
        //debugger;
        // check to make sure the form is completely valid
        if ($scope.errMessage === '') {
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
        $scope.MessageString = $scope.errMessage;
    }
});

