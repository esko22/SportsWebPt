'use strict';

angular.module('research.exercises', ['research.exercise.detail'])
    .controller('ExerciseListingController', ['$scope', 'configService', 'exercises', function($scope, configService, exercises) {
        
        $scope.categories = configService.exerciseCategories;
        $scope.bodyRegions = configService.bodyRegions;
        $scope.equipment = configService.equipment;
        $scope.exercises = exercises;

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

    }])
    .controller('BriefExerciseController', ['$scope', function ($scope) {
        $scope.oneAtATime = true;
    }])
    .directive("briefExerciseAccordian", function() {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/app/research/exercises/prtl.brief.exercise.accord.htm',
        controller: "BriefExerciseController"
    };
});