'use strict';

var therapistModule = angular.module('therapist.module', []);


therapistModule.controller('TherapistDashboardController', [
    '$scope', 'therapistService',
    function ($scope, therapistService) {

        therapistService.get($scope.currentUser.id).$promise.then(function(therapistDetails) {
            $scope.clinics = therapistDetails.clinics;
        });
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

therapistModule.controller('TherapistPlanController', [
    '$scope', 'planAdminService', 'userManagementService', '$modal',
    function ($scope, planAdminService, userManagementService, $modal) {

        getPlanList();

        $scope.planGridOptions = {
            data: 'plans',
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'routineName', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindSelectedPlan(row.entity)" >Edit</button> <button id="editBtn" type="button" class="btn-small" ng-click="setSharedPlanSettings(row.entity)" >Share</button>' }]
        };

        $scope.bindSelectedPlan = function (plan) {
            $scope.selectedPlan = plan;

            var modalInstance = $modal.open({
                templateUrl: '/app/admin/plans/tmpl.plan.modal.htm',
                controller: 'PlanModalController',
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


        $scope.setSharedPlanSettings = function (plan) {
            $scope.selectedPlan = plan;

            $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.shard.plan.modal.htm',
                controller: 'SharedPlanModalController',
                resolve: {
                    clinics: function () {
                        return $scope.clinics;
                    },
                    selectedPlan: function () {
                        return $scope.selectedPlan;
                    }
                }
            });
        }


        function getPlanList() {
            planAdminService.getPlansForTherapist($scope.currentUser.id).$promise.then(function (plans) {
                $scope.plans = plans;
            });
        }
    }
]);

therapistModule.controller('SharedPlanModalController', [
    '$scope', 'therapistService', '$modal', 'selectedPlan', 'clinics','$rootScope','notifierService','$modalInstance',
    function ($scope, therapistService, $modal, selectedPlan, clinics, $rootScope, notifierService, $modalInstance) {

        $scope.sharedPlans = [];

        therapistService.getSharedPlansForTherapist($rootScope.currentUser.id, selectedPlan.id).$promise.then(function(results) {
            angular.forEach(clinics, function (clinic) {
                var sharedPlanRecord = _.findWhere(results, { clinicId: clinic.id });
                if (sharedPlanRecord) {
                    $scope.sharedPlans.push(sharedPlanRecord);
                } else {
                    $scope.sharedPlans.push({ clinicName : clinic.name, clinicId : clinic.id, isActive : false, planId : selectedPlan.id });
                }
            });
        });

        $scope.submit = function () {
            if ($scope.sharedPlans.length > 0) {
                therapistService.updateSharedPlans($rootScope.currentUser.id, $scope.sharedPlans).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

therapistModule.controller('SharedExerciseModalController', [
    '$scope', 'therapistService', '$modal', 'selectedExercise', 'clinics', '$rootScope', 'notifierService','$modalInstance',
    function ($scope, therapistService, $modal, selectedExercise, clinics, $rootScope, notifierService, $modalInstance) {

        $scope.sharedExercises = [];

        therapistService.getSharedExercisesForTherapist($rootScope.currentUser.id, selectedExercise.id).$promise.then(function (results) {
            angular.forEach(clinics, function (clinic) {
                var sharedExerciseRecord = _.findWhere(results, { clinicId: clinic.id });
                if (sharedExerciseRecord) {
                    $scope.sharedExercises.push(sharedExerciseRecord);
                } else {
                    $scope.sharedExercises.push({ clinicName: clinic.name, clinicId: clinic.id, isActive: false, exerciseId: selectedExercise.id });
                }
            });
        });

        $scope.submit = function () {
            if ($scope.sharedExercises.length > 0) {
                therapistService.updateSharedExercises($rootScope.currentUser.id, $scope.sharedExercises).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

therapistModule.controller('TherapistExerciseController', [
    '$scope', 'exerciseAdminService', '$modal',
    function ($scope, exerciseAdminService, $modal) {

        getExerciseList();

        $scope.exerciseGridOptions = {
            data: 'exercises',
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'name', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindSelectedExercise(row.entity)" >Edit</button> <button id="editBtn" type="button" class="btn-small" ng-click="setSharedExerciseSettings(row.entity)" >Share</button>' }]
        };

        $scope.bindSelectedExercise = function (exercise) {
            $scope.selectedExercise = exercise;

            var modalInstance = $modal.open({
                templateUrl: '/app/admin/exercises/tmpl.exercise.modal.htm',
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

        $scope.setSharedExerciseSettings = function (exercise) {
            $scope.selectedExercise = exercise;

            $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.shard.exercise.modal.htm',
                controller: 'SharedExerciseModalController',
                resolve: {
                    clinics: function () {
                        return $scope.clinics;
                    },
                    selectedExercise: function () {
                        return $scope.selectedExercise;
                    }
                }
            });
        }


        function getExerciseList() {
            exerciseAdminService.getExercisesForTherapist($scope.currentUser.id).$promise.then(function (exercises) {
                $scope.exercises = exercises;
            });
        }
    }
]);

therapistModule.directive('therapistExerciseList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.exercise.list.htm',
        controller: 'TherapistExerciseController'
    };
}]);


therapistModule.factory('therapistService', ['$resource', 'configService', function ($resource, configService) {

    var therapistPath = configService.apiUris.therapists;

    return {
        get: function (id) {
            var resource = $resource(therapistPath);
            return resource.get({ id: id });
        },
        getSharedPlansForTherapist: function (therapistId, planId) {
            var resource = $resource(configService.apiUris.therapistSharedPlans);
            return resource.query({ id: therapistId, planId: planId });
        },
        getSharedExercisesForTherapist: function (therapistId, exerciseId) {
            var resource = $resource(configService.apiUris.therapistSharedExercises);
            return resource.query({ id: therapistId, exerciseId: exerciseId });
        },
        updateSharedPlans: function (therapistId, sharedPlans) {
            var resource = $resource(configService.apiUris.therapistSharedPlans, null, {'update': { method: 'PUT' }});
            return resource.update({ id: therapistId }, sharedPlans);
        },
        updateSharedExercises: function (therapistId, sharedExercises) {
            var resource = $resource(configService.apiUris.therapistSharedExercises, null, {'update': { method: 'PUT' }});
            return resource.update({ id: therapistId }, sharedExercises);
        }
    }
}]);

