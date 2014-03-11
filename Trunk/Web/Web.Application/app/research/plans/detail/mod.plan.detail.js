'use strict';

angular.module('research.plan.detail', [])
    .controller('PlanViewController', [
        '$scope', 'plan','navBarService', 'configService', function($scope, plan, navBarService, configService) {
            $scope.plan = plan;

            navBarService.entityType = 'plans';
            navBarService.entityId = plan.id;
            navBarService.returnUri = configService.returnUris.researchPlans;

            $scope.navBarService = navBarService;
        }
    ])
    .controller('PlanDescriptionController', function ($scope) {
        $scope.structuresInvolved = $scope.plan.structuresInvolved.split(',');
        $scope.animationTag = $scope.plan.animationTag;
   })
    .controller('PlanDetailController', function ($scope) {

    })
    .controller('PlanExerciseListingController', function ($scope, planDetailService) {

        $scope.$watch('plan', function (newVal) {
            if (newVal) {
                if (newVal.exercises.length === 0) {
                    planDetailService.getPlan(newVal.id).
                        then(function (fetchedPlan) {
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
    })
    .directive("planDescription", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'PlanDescriptionController',
            templateUrl: '/app/research/plans/detail/tmpl.plan.description.htm'
        };
    })
    .directive("planExerciseListing", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'PlanExerciseListingController',
            templateUrl: '/app/research/plans/detail/tmpl.plan.exercise.listing.htm'
        };
    })
    .factory('planDetailService', function ($resource, $q, configService) {
        var resource = $resource(configService.apiUris.planDetail + ':id', { id: '@id' });
        return {
            getPlan: function(planId) {
                var deferred = $q.defer();
                resource.get({ id: planId },
                    function(plan) {
                        deferred.resolve(plan);
                    },
                    function(response) {
                        deferred.reject(response);
                    });

                return deferred.promise;
            }
        };
    });

