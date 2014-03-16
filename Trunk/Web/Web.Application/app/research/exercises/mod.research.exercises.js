'use strict';

angular.module('research.exercises', ['research.exercise.detail'])
    .controller('ExerciseListingController', ['$scope', 'configService', 'exerciseListService', function($scope, configService, exerciseListService) {
        
        $scope.categories = configService.exerciseCategories;
        $scope.bodyRegions = configService.bodyRegions;
        $scope.equipment = configService.equipment;
        $scope.isLoading = true;

        $scope.selectedCategory = "";
        $scope.selectedBodyRegion = "";
        $scope.selectedEquipment = "";

        $scope.isCategorySelected = function(category) {
            return $scope.selectedCategory === category;
        };

        $scope.isBodyRegionSelected = function (bodyRegion) {
            return $scope.selectedBodyRegion === bodyRegion;
        };

        $scope.isEquipmentSelected = function (equipment) {
            return $scope.selectedEquipment === equipment;
        };

        $scope.setCategory = function(category) {
            $scope.selectedCategory = category;
        };

        $scope.setBodyRegion = function(bodyRegion) {
            $scope.selectedBodyRegion = bodyRegion;
        };

        $scope.setEquipment = function (equipment) {
            $scope.selectedEquipment = equipment;
        };

        exerciseListService.exerciseList.$promise.then(function (exercises) {
            $scope.isLoading = false;
            $scope.exercises = exercises;
        });


    }])
    .controller('BriefExerciseController', ['$scope', function ($scope) {
        $scope.oneAtATime = true;
    }])
    .factory('exerciseListService',['$resource', 'configService', function ($resource, configService) {

        return {
            exerciseList: $resource(configService.apiUris.briefExercises).query()
        };

    }])
    .directive("briefExerciseAccordian", [function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/app/research/exercises/prtl.brief.exercise.accord.htm',
        controller: "BriefExerciseController"
    };
}]);