'use strict';

angular.module('research.injuries', ['research.injury.detail'])
    .controller('InjuryController', [
        '$scope', 'configService', 'injuryListService', function ($scope, configService, injuryListService) {

            $scope.signFilters = configService.signFilters;
            $scope.bodyRegions = configService.bodyRegions;
            $scope.isLoading = true;

            $scope.selectedSign = "";
            $scope.selectedBodyRegion = "";

            $scope.isSignSelected = function (sign) {
                return $scope.selectedSign === sign;
            };

            $scope.isBodyRegionSelected = function (bodyRegion) {
                return $scope.selectedBodyRegion === bodyRegion;
            };

            $scope.setSign = function (sign) {
                $scope.selectedSign = sign;
            };

            $scope.setBodyRegion = function (bodyRegion) {
                $scope.selectedBodyRegion = bodyRegion;
            };

            injuryListService.injuryList.$promise.then(function (injuries) {
                $scope.isLoading = false;
                $scope.injuries = injuries;
            });


        }
    ])
    .controller('BriefInjuryController', [
        '$scope', function($scope) {
            $scope.oneAtATime = true;
        }
    ])
    .factory('injuryListService', ['$resource', 'configService', function ($resource, configService) {

        return {
            injuryList: $resource(configService.apiUris.briefInjuries).query()
        };

    }])
    .directive("briefInjuryAccordian", [function() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/research/injuries/prtl.brief.injury.accord.htm',
            controller: "BriefInjuryController"
        };
    }]);