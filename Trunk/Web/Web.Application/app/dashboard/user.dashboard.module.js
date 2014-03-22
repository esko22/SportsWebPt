'use strict';

var userDashboard = angular.module('user.dashboard', []);


userDashboard.controller('UserDashboardController', ['$scope', 'userManagementService',
    function ($scope, userManagementService) {

        userManagementService.getUser().$promise.then(function(user) {
            $scope.currentUser = user;
        });

    }
]);