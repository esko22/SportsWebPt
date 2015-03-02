'use strict';

var userDashboardModule = angular.module('user.dashboard.module', []);

userDashboardModule.controller('UserDashboardController', ['$scope', 'userManagementService',
    function ($scope, userManagementService) {

        userManagementService.getUser().$promise.then(function (user) {
            $scope.currentUser = user;
        });

    }
]);




