'use strict';

angular.module('research.exercises', [])
    .controller('ExerciseController', ['$scope', 'configService', 'exercises', function($scope, configService, exercises) {
        
    $scope.categories = configService.exerciseCategories;
    $scope.bodyRegions = configService.bodyRegions;
    $scope.equipment = configService.equipment;

    $scope.exercises = exercises;

    $scope.hasExercises = function() { return exercises.length > 0; };

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