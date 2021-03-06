﻿'use strict';

angular.module('research.plan.detail', [])
    .controller('PlanViewController', [
        '$scope', 'navBarService', 'configService', 'planDetailService', '$stateParams', function ($scope, navBarService, configService, planDetailService, $stateParams) {
            $scope.isLoading = true;
            $scope.animationTag = null;

            //TODO: move this out of controller
            var planDump = $('#selected-plan').val();

            if (planDump) {
                onPlanLoadComplete(JSON.parse(planDump));
            } else {
                planDetailService.getPlan($stateParams.planId).$promise.then(function (plan) {
                    onPlanLoadComplete(plan);
                });
            }

            function onPlanLoadComplete(plan) {
                $scope.plan = plan;
                navBarService.entityId = plan.id;
                $scope.isFavorite = navBarService.isFavorite();
                $scope.isLoading = false;
            };

            navBarService.entityType = 'plan';
            navBarService.returnUri = configService.returnUris.researchPlans;

            $scope.navBarService = navBarService;
        }
    ])
    .controller('PlanDescriptionController', ['$scope', function ($scope) {
        $scope.$watch('plan', function (plan) {

            $scope.hasAnimationTag = function () {
                return $scope.animationTag !== null;
            };

            if (plan) {
                if ($scope.plan.structuresInvolved) {
                    $scope.structuresInvolved = $scope.plan.structuresInvolved.split(',');
                }
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
                            if ($scope.exercises && $scope.exercises.length > 0)
                                $scope.exercise = $scope.plan.exercises[0];
                        });
                }

                $scope.exercises = $scope.plan.exercises;
                if ($scope.exercises && $scope.exercises.length > 0)
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

