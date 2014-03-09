'use strict';

angular.module('research.exercise.detail', [])
    .controller('ExerciseViewController', [
        '$scope', 'exercise','navBarService', 'configService', function($scope, exercise, navBarService, configService) {
            $scope.exercise = exercise;

            navBarService.entityType = 'exercises';
            navBarService.entityId = exercise.id;
            navBarService.returnUri = configService.returnUris.researchExercise;

            $scope.navBarService = navBarService;
        }
    ])
    .controller('ExerciseDescriptionController',function ($scope) {

    })
    .controller('ExerciseDetailController', function ($scope) {

        $scope.video = $scope.exercise.videos[0];
        $scope.code = $scope.exercise.videos[0].youtubeVideoId;
    })
    .directive("exerciseDescription", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'ExerciseDescriptionController',
            templateUrl: '/app/research/exercises/detail/tmpl.exercise.description.htm'
        };
    })
    .directive("exerciseDetail", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'ExerciseDetailController',
            templateUrl: '/app/research/exercises/detail/tmpl.exercise.detail.htm'
        };
    })
    .factory('exerciseDetailService', function ($resource, $q, configService) {
        var resource = $resource(configService.apiUris.exerciseDetail + ':id', { id: '@id' });
        return {
            getExercise: function(exerciseId) {
                var deferred = $q.defer();
                resource.get({ id: exerciseId },
                    function(exercise) {
                        deferred.resolve(exercise);
                    },
                    function(response) {
                        deferred.reject(response);
                    });

                return deferred.promise;
            }
        };
    });

