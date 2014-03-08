'use strict';

angular.module('research.injuries', [])
    .controller('InjuryController', ['$scope', 'configService', 'notifierService', function ($scope, configService, notifierService) {

        $scope.signFilters = configService.signFilters;
        $scope.bodyRegions = configService.bodyRegions;

    $scope.popMsg = function() {
        notifierService.notify('test me');
    };

}]);