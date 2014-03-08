'use strict';

angular.module('research.injuries', [])
    .controller('InjuryController', [
        '$scope', 'configService', 'notifierService', 'injuries', function ($scope, configService, notifierService, injuries) {

            $scope.signFilters = configService.signFilters;
            $scope.bodyRegions = configService.bodyRegions;

            $scope.injuries = injuries;
            $scope.hasInjuries = function () { return injuries.length > 0; };

            $scope.popMsg = function() {
                notifierService.notify('test me');
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