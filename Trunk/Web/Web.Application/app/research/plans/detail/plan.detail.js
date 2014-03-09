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

    })
    .controller('PlanDetailController', function ($scope) {

    })
    .directive("planDescription", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'PlanDescriptionController',
            templateUrl: '/app/research/plans/detail/tmpl.plan.description.htm'
        };
    })
    //.directive("exerciseDetail", function () {
    //    return {
    //        restrict: 'E',
    //        replace: true,
    //        controller: 'ExerciseDetailController',
    //        templateUrl: '/app/research/exercises/detail/tmpl.exercise.detail.htm'
    //    };
    //})
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

