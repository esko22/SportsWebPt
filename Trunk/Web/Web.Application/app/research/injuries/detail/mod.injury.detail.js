'use strict';

angular.module('research.injury.detail', [])
    .controller('InjuryViewController', [
        '$scope','navBarService', 'configService', 'injuryDetailService', '$stateParams', function($scope, navBarService, configService, injuryDetailService, $stateParams) {

            $scope.isLoading = true;

            injuryDetailService.getInjury($stateParams.injuryId).$promise.then(function (injury) {
                $scope.injury = injury;
                navBarService.entityId = injury.id;
                $scope.isLoading = false;
            });

            navBarService.entityType = 'injuries';
            navBarService.returnUri = configService.returnUris.researchInjuries;

            $scope.navBarService = navBarService;
        }
    ])
    .controller('InjuryDescriptionController', ['$scope', function ($scope) {
        $scope.$watch('injury', function (injury) {
            if (injury) {
                $scope.animationTag = $scope.injury.animationTag;
            }
        });
    }])
    .controller('InjuryPlanDetailController', [function () {

    }])
    .controller('InjuryPlanListingController', ['$scope', function ($scope) {
        $scope.$watch('injury', function (injury) {
            if (injury) {
                $scope.oneAtATime = true;
                $scope.plans = $scope.injury.plans;
            }
        });
    }])
    .directive("injuryDescription", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryDescriptionController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.description.htm'
        };
    }])
    .directive("injuryPlanListing", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryPlanListingController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.plan.listing.htm'
        };
    }])
    .directive("injuryPlanDetail", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryPlanDetailController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.plan.detail.htm'
        };
    }])
    .factory('injuryDetailService',['$resource', 'configService', function ($resource, configService) {

        var resource = $resource(configService.apiUris.injuryDetail + ':id', { id: '@id' });

        var getInjury = function (injuryId) {
            return resource.get({ id: injuryId });
        };

        return {
            getInjury: getInjury
        };
    }]);

