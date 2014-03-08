'use strict';

angular.module('research.exercises', [])
    .controller('ExerciseController', ['$scope', 'configService', function($scope, configService) {

    $scope.categories = configService.exerciseCategories;
    $scope.bodyRegions = configService.bodyRegions;
    $scope.equipment = configService.equipment;

}]);
