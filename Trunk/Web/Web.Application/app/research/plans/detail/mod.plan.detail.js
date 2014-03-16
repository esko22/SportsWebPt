'use strict';

angular.module('research.plan.detail', [])
    .controller('PlanViewController', [
        '$scope', 'navBarService', 'configService', 'planDetailService', '$stateParams', function ($scope, navBarService, configService, planDetailService, $stateParams) {
            $scope.isLoading = true;

            planDetailService.getPlan($stateParams.planId).$promise.then(function(plan) {
                $scope.plan = plan;
                $scope.isLoading = false;
                navBarService.entityId = plan.id;
            });

            navBarService.entityType = 'plans';
            navBarService.returnUri = configService.returnUris.researchPlans;

            $scope.navBarService = navBarService;
        }
    ])
    .controller('PlanDescriptionController', ['$scope', function ($scope) {
        $scope.$watch('plan', function(plan) {
            if (plan) {
                $scope.structuresInvolved = $scope.plan.structuresInvolved.split(',');
                $scope.animationTag = $scope.plan.animationTag;
            }
        });
    }])
    .controller('PlanDetailController',[ function () {

    }])
    .controller('PlanExerciseListingController',['$scope', 'planDetailService', function ($scope, planDetailService) {

        $scope.$watch('plan', function (newVal) {
            if (newVal) {
                if (newVal.exercises.length === 0) {
                    planDetailService.getPlan(newVal.id).
                        $promise.then(function (fetchedPlan) {
                            $scope.plan.exercises = fetchedPlan.exercises;
                            $scope.exercises = $scope.plan.exercises;
                            $scope.exercise = $scope.plan.exercises[0];
                        });
                }

                $scope.exercises = $scope.plan.exercises;
                $scope.exercise = $scope.plan.exercises[0];
            }
        });


        $scope.onSelectExercise = function (exerciseIndex) {
            $scope.exercise = $scope.plan.exercises[exerciseIndex];
        };
    }])
    .directive("planDescription",[ function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'PlanDescriptionController',
            templateUrl: '/app/research/plans/detail/tmpl.plan.description.htm'
        };
    }])
    .directive("planExerciseListing",[ function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'PlanExerciseListingController',
            templateUrl: '/app/research/plans/detail/tmpl.plan.exercise.listing.htm'
        };
    }])
    .factory('planDetailService',['$resource', 'configService', function ($resource, configService) {
        var resource = $resource(configService.apiUris.planDetail + ':id', { id: '@id' });

        var getPlan = function (planId) {
            return resource.get({ id: planId });
        };

        return {
            getPlan: getPlan
        };
    }]);

