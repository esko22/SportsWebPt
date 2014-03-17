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
    .controller('EquipmentModalController', ['$scope', function ($scope) {
        $scope.equipmentList = $scope.exercise.equipment;
    }])
    .controller('ExerciseDetailController', ['$scope', '$modal', function ($scope, $modal) {
        $scope.showEquipmentList = function () {
            $modal.open({
                templateUrl: '/app/research/exercises/detail/tmpl.equipment.list.modal.htm',
                controller: 'EquipmentModalController',
                scope: $scope
            });
        };

        $scope.hasEquipment = false;

        $scope.$watch('exercise', function (newVal) {
            if (newVal) {
                $scope.video = $scope.exercise.videos[0];
                $scope.code = $scope.exercise.videos[0].youtubeVideoId;
                $scope.hasEquipment = $scope.exercise.equipment.length > 0;
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

