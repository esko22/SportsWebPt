'use strict';

angular.module('research.exercise.detail', [])
    .controller('ExerciseViewController', [
        '$scope', '$stateParams', 'navBarService', 'configService', 'exerciseDetailService', function ($scope, $stateParams, navBarService, configService, exerciseDetailService) {

            $scope.exercise = null;


            exerciseDetailService.getExercise($stateParams.exerciseId).$promise.then(function (exercise) {
                $scope.exercise = exercise;
                navBarService.entityId = exercise.id;
                $scope.isLoading = false;
            });

            navBarService.entityType = 'exercises';
            navBarService.returnUri = configService.returnUris.researchExercise;

            $scope.navBarService = navBarService;
            $scope.isLoading = true;
        }
    ])
    .controller('ExerciseDescriptionController',[function () {

    }])
    .controller('ExerciseDetailController',['$scope', function ($scope) {

        $scope.$watch('exercise', function (newVal) {
            if (newVal) {
                $scope.video = $scope.exercise.videos[0];
                $scope.code = $scope.exercise.videos[0].youtubeVideoId;
            }
        });
    }])
    .directive("exerciseDescription", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'ExerciseDescriptionController',
            templateUrl: '/app/research/exercises/detail/tmpl.exercise.description.htm'
        };
    }])
    .directive("exerciseDetail", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'ExerciseDetailController',
            templateUrl: '/app/research/exercises/detail/tmpl.exercise.detail.htm'
        };
    }])
    .factory('exerciseDetailService',['$resource', 'configService', function ($resource, configService) {
        var resource = $resource(configService.apiUris.exerciseDetail + ':id', { id: '@id' });

            var getExercise = function(exerciseId) {
                return resource.get({ id: exerciseId });
            };

            return {
                getExercise: getExercise
            };
    }]);

