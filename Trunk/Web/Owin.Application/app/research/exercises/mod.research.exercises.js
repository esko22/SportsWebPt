﻿'use strict';

angular.module('research.exercises', ['research.exercise.detail'])
    .controller('ExerciseListingController', ['$scope', 'configService', 'exerciseListService', '$filter', '$location', '$anchorScroll',
        function ($scope, configService, exerciseListService, $filter, $location, $anchorScroll) {
        
        $scope.categories = configService.lookups.exerciseCategories;
        $scope.bodyRegions = configService.bodyRegions;
        $scope.equipment = configService.equipment;
        $scope.isLoading = true;

        $scope.selectedCategory = "";
        $scope.selectedBodyRegion = "";
        $scope.selectedEquipment = "";

        // Paging controls
        $scope.pageSize = 20;
        $scope.currentPage = 1;
        $scope.firstItemPosition = 1;
        $scope.lastItemPosition = $scope.pageSize;
        $scope.onPageChanged = function () {
            $scope.firstItemPosition = (($scope.currentPage - 1) * $scope.pageSize) + 1;
            $scope.lastItemPosition = Math.min($scope.currentPage * $scope.pageSize, $scope.filteredExercises.length);
        };

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
            applyFilter();
        };

        $scope.setBodyRegion = function(bodyRegion) {
            $scope.selectedBodyRegion = bodyRegion;
            applyFilter();
        };

        $scope.setEquipment = function (equipment) {
            $scope.selectedEquipment = equipment;
            applyFilter();
        };

        exerciseListService.exerciseList.$promise.then(function (exercises) {
            $scope.isLoading = false;
            $scope.exercises = exercises;
            $scope.filteredExercises = exercises;
        });

        function applyFilter() {
            $scope.filteredExercises = $filter('filter')($scope.exercises, { categories: $scope.selectedCategory, bodyRegions: $scope.selectedBodyRegion, equipment: $scope.selectedEquipment });
            $scope.currentPage = 1;
            $scope.onPageChanged();
            $location.hash('research-exercise-panel');
            //$anchorScroll();
        }

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