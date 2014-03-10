'use strict';

angular.module('research.injury.detail', [])
    .controller('InjuryViewController', [
        '$scope', 'injury','navBarService', 'configService', function($scope, injury, navBarService, configService) {
            $scope.injury = injury;

            navBarService.entityType = 'injuries';
            navBarService.entityId = injury.id;
            navBarService.returnUri = configService.returnUris.researchInjuries;

            $scope.navBarService = navBarService;
        }
    ])
    .controller('InjuryDescriptionController', function ($scope) {
        $scope.animationTag = $scope.injury.animationTag;
    })
    .controller('InjuryPlanDetailController', function ($scope) {

    })
    .controller('InjuryPlanListingController', function ($scope, planDetailService) {
        $scope.oneAtATime = true;

        $scope.$watch('injury', function (newVal) {
            if (newVal) {
                $scope.plans = $scope.injury.plans;

                angular.forEach($scope.plans, function (plan) {
                    planDetailService.getPlan(plan.id).
                        then(function(fetchedPlan) {
                            plan.exercises = fetchedPlan.exercises;
                    });
                });
            }
        });
    })
    .directive("injuryDescription", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryDescriptionController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.description.htm'
        };
    })
    .directive("injuryPlanListing", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryPlanListingController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.plan.listing.htm'
        };
    })
    .directive("injuryPlanDetail", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryPlanDetailController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.plan.detail.htm'
        };
    })
    .factory('injuryDetailService', function ($resource, $q, configService) {
        var resource = $resource(configService.apiUris.injuryDetail + ':id', { id: '@id' });
        return {
            getInjury: function(injuryId) {
                var deferred = $q.defer();
                resource.get({ id: injuryId },
                    function (injury) {

                        deferred.resolve(injury);
                    },
                    function (response) {
                        deferred.reject(response);
                    });

                return deferred.promise;
            }
        };
    });

