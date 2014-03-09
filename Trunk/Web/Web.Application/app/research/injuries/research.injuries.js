'use strict';

angular.module('research.injuries', [])
    .controller('InjuryController', [
        '$scope', 'configService', 'notifierService', 'injuries', function ($scope, configService, notifierService, injuries) {

            $scope.signFilters = configService.signFilters;
            $scope.bodyRegions = configService.bodyRegions;

            $scope.injuries = injuries;

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


        }
    ])
    .controller('BriefInjuryController', [
        '$scope', function($scope) {
            $scope.oneAtATime = true;
        }
    ])
    .directive("briefInjuryAccordian", function() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/research/injuries/prtl.brief.injury.accord.htm',
            controller: "BriefInjuryController"
        };
    });