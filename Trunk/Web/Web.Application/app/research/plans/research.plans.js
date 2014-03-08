'use strict';

angular.module('research.plans', [])
    .controller('PlanController', ['$scope', 'configService', function ($scope, configService) {

        $scope.categories = configService.planCategories;
        $scope.bodyRegions = configService.bodyRegions;

    }]);