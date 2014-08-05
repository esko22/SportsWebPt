'use strict';

var therapistModule = angular.module('therapist.module', []);


therapistModule.controller('TherapistDashboardController', [
    '$scope', 'therapistManagementService', 'userManagementService','$modal',
    function($scope, therapistManagementService, userManagementService, $modal) {

       

       

    }
]);

therapistModule.controller('TherapistPlanController', [
    '$scope', 'therapistManagementService', '$modal',
    function ($scope, therapistManagementService, userManagementService, $modal) {

        getPlanList();

        $scope.planGridOptions = {
            data: 'plans',
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'routineName', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindSelectedPlan(row.entity)" >Edit</button> <button id="editBtn" type="button" class="btn-small" ng-click="" >Share</button>' }]
        };

        $scope.bindSelectedPlan = function (plan) {
            $scope.selectedPlan = plan;

            var modalInstance = $modal.open({
                templateUrl: '/app/admin/content/tmpl.plan.modal.htm',
                controller: 'TherapistPlanModalController',
                windowClass: 'xx-dialog',
                resolve: {
                    selectedPlan: function () {
                        return $scope.selectedPlan;
                    }
                }
            });

            modalInstance.result.then(function (planReturned) {
                getPlanList();
                $scope.selectedPlan = planReturned;
            });
        }


        function getPlanList() {
            therapistManagementService.getPlans($scope.currentUser.id).$promise.then(function (plans) {
                $scope.plans = plans;
            });
        }
    }
]);

therapistModule.controller('TherapistExerciseController', [
    '$scope', 'therapistManagementService', '$modal',
    function ($scope, therapistManagementService, userManagementService, $modal) {

        getExerciseList();

        $scope.exerciseGridOptions = {
            data: 'exercises',
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'name', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindPublishExercise(row.entity)" >Edit</button> ' }]
        };

        $scope.bindSelectedExercise = function (exercise) {
            $scope.selectedExercise = exercise;

            var modalInstance = $modal.open({
                templateUrl: '/app/admin/content/tmpl.exercise.modal.htm',
                controller: 'ExerciseModalController',
                windowClass: 'xx-dialog',
                resolve: {
                    selectedExercise: function () {
                        return $scope.selectedExercise;
                    }
                }
            });

            modalInstance.result.then(function (exerciseReturned) {
                getExerciseList();
                $scope.selectedExercise = exerciseReturned;
            });
        }

        function getExerciseList() {
            therapistManagementService.getExercises($scope.currentUser.id).$promise.then(function (exercises) {
                $scope.exercises = exercises;
            });
        }
    }
]);


therapistModule.controller('TherapistPlanModalController', [
    '$scope', 'planAdminService', '$modalInstance', 'selectedPlan', 'notifierService',
    'configService', 'exerciseAdminService', 'bodyRegionAdminService',
    function ($scope, planAdminService, $modalInstance, selectedPlan, notifierService, configService, exerciseAdminService, bodyRegionAdminService) {

        $scope.plan = {};
        if (selectedPlan) {
            planAdminService.get(selectedPlan.id).$promise.then(function(result) {
                $scope.plan = result;
            });
        }

        //lookups
        $scope.categories = configService.lookups.functionPlanCategories;
        $scope.holdTypes = configService.lookups.holdTypes;
        $scope.repetitionRangeValues = configService.lookups.repetitionRangeValues;
        $scope.editorOptions = configService.kendoEditorOptions;

        bodyRegionAdminService.getAll().$promise.then(function (results) {
            $scope.availableBodyRegions = results;
            if (selectedPlan) {
                var currentBodyRegions = [];
                _.each(selectedPlan.bodyRegions, function (bodyRegion) {
                    currentBodyRegions.push(_.findWhere($scope.availableBodyRegions, { id: bodyRegion.id }));
                });
                selectedPlan.bodyRegions = currentBodyRegions;
            }
        });

        exerciseAdminService.getAll().$promise.then(function (results) {
            $scope.availableExercises = results;
            if (selectedPlan) {
                _.each(selectedPlan.exercises, function (planExercise) {
                    planExercise.exercise = _.findWhere($scope.availableExercises, { id: planExercise.id });
                });
            }
        });


        $scope.addExercise = function () {
            if (!$scope.plan.exercises) {
                $scope.plan.exercises = [];
            }

            $scope.plan.exercises.push({});
        }

        $scope.removeExercise = function (index) {
            $scope.plan.exercises.splice(index, 1);
        }


        $scope.submit = function () {
            if ($scope.plan && $scope.plan.id > 0) {
                planAdminService.update($scope.plan).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close($scope.plan);
                });
            } else {
                planAdminService.save($scope.plan).$promise.then(function () {
                    notifierService.notify('Created Successfully!');
                    $modalInstance.close($scope.plan);
                });
            }
        };

        $scope.reset = function () {
            $scope.plan = null;
            $modalInstance.dismiss('cancel');
        };
    }
]);


therapistModule.directive('therapistPlanList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.plan.list.htm',
        controller: 'TherapistPlanController'
    };
}]);

therapistModule.directive('therapistExerciseList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.exercise.list.htm',
        controller: 'TherapistExerciseController'
    };
}]);

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