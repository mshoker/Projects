var myApp = angular.module('employeeApp', ['ngRoute']);



myApp.factory('employeeFactory',
    function ($http) {
        //create object
        var webAPIProvider = {};
        var url = '/api/TimeSheet';

        //create get request
        webAPIProvider.getEntries = function($scope) {
            return $http.getEntries(url, $scope.Id);
        };

        //create post request
        webAPIProvider.saveEntry = function(entry) {
            return $http.post(url, entry);
        };

        //object is ready, return to controller
        return webAPIProvider;
    });

myApp.config(function ($routeProvider)
{
    $routeProvider.when('/List',
        {
            controller: "TimeSheetController",
            templateUrl: "/AngularViews/ListWorkHistory.html"
        })
        //create .when('/Add') etc
        .otherwise({
            redirectTo: '/List'
        });
});

myApp.controller('TimeSheetController',
    function($scope, employeeFactory) {
        employeeFactory.getEntries()
            .success(function(data) {
                $scope.entries = data;
            })
            .error(function(data, status) {
                alert("Oh crap! Status: " + status);
            });
    });