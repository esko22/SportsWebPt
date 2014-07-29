'use strict';

var therapistModule = angular.module('therapist.module', []);


therapistModule.controller('TherapistDashboardController', [
    '$scope', 'therapistManagementService', 'userManagementService',
    function($scope, therapistManagementService, userManagementService) {

        userManagementService.getUser().$promise.then(function (user) {
            $scope.currentUser = user;
            therapistManagementService.getPlans(user.id).$promise.then(function(plans) {
                $scope.plans = plans;
            });
            therapistManagementService.getExercises(user.id).$promise.then(function (exercises) {
                $scope.exercises = exercises;
            });
        });

        $scope.planGridOptions = {
            data: 'plans',
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'routineName', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' },
            { field: 'visible', displayName: 'Visible' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindPublishPlan(row.entity)" >Edit</button> ' }]
        };

        $scope.exerciseGridOptions = {
            data: 'exercises',
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'name', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' },
            { field: 'visible', displayName: 'Visible' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindPublishExercise(row.entity)" >Edit</button> ' }]
        };

    }
]);

therapistModule.factory('therapistManagementService', [
    '$resource', 'configService', '$http', function($resource, configService, $http) {

        var getPlans = function(therapistId) {
            var resource = $resource(configService.apiUris.therapistPlans, { id: '@id' });
            return resource.query({ id: therapistId });
        };

        var getExercises = function (therapistId) {
            var resource = $resource(configService.apiUris.therapistExercises, { id: '@id' });
            return resource.query({ id: therapistId });
        };

        return {
            getPlans: getPlans,
            getExercises: getExercises
        };
    }
]);