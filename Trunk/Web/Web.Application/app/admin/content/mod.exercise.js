'use strict';

var exerciseAdminModule = angular.module('exercise.admin.module', []);

exerciseAdminModule.directive('exerciseList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/content/tmpl.exercise.list.htm'
    };
}]);


exerciseAdminModule.controller('ExerciseModalController', [
    '$scope', 'exerciseAdminService', '$modalInstance', 'selectedExercise', 'notifierService',
    'configService', 'equipmentAdminService', 'bodyRegionAdminService', 'videoAdminService',
    function ($scope, exerciseAdminService, $modalInstance, selectedExercise, notifierService, configService, equipmentAdminService, bodyRegionAdminService, videoAdminService) {

        $scope.exercise = {};
        if (selectedExercise) {
            $scope.exercise = selectedExercise;
        }

        //lookups
        $scope.categories = configService.lookups.functionExerciseCategories;
        $scope.holdTypes = configService.lookups.holdTypes;
        $scope.repetitionRangeValues = configService.lookups.repetitionRangeValues;
        $scope.editorOptions = configService.kendoEditorOptions;
        $scope.difficulties = configService.lookups.difficulties;

        equipmentAdminService.getAll().$promise.then(function (results){
            $scope.availableEquipment = results;
            //have to get items from available collection
            if (selectedExercise) {
                var currentEquipment = [];
                _.each(selectedExercise.equipment, function(equipment) {
                    currentEquipment.push(_.findWhere($scope.availableEquipment, { id: equipment.id }));
                });
                selectedExercise.equipment = currentEquipment;
            }
        });

        bodyRegionAdminService.getAll().$promise.then(function (results) {
            $scope.availableBodyRegions = results;
            if (selectedExercise) {
                var currentBodyRegions = [];
                _.each(selectedExercise.bodyRegions, function (bodyRegion) {
                    currentBodyRegions.push(_.findWhere($scope.availableBodyRegions, { id: bodyRegion.id }));
                });
                selectedExercise.bodyRegions = currentBodyRegions;
            }
        });

        videoAdminService.getAll().$promise.then(function (results) {
            $scope.availableVideos = results;
            if (selectedExercise) {
                var currentVideos = [];
                _.each(selectedExercise.videos, function(video) {
                    currentVideos.push(_.findWhere($scope.availableVideos, { id: video.id }));
                });
                selectedExercise.videos = currentVideos;
            }
        });

        $scope.submit = function () {
            if ($scope.exercise && $scope.exercise.id > 0) {
                exerciseAdminService.update($scope.exercise).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close($scope.exercise);
                });
            } else {
                exerciseAdminService.save($scope.exercise).$promise.then(function () {
                    notifierService.notify('Created Successfully!');
                    $modalInstance.close($scope.exercise);
                });
            }
        };

        $scope.reset = function () {
            $scope.exercise = null;
            $modalInstance.dismiss('cancel');
        };
    }
]);

exerciseAdminModule.controller('ExerciseController', ['$scope', 'exerciseAdminService', 'configService', '$modal', function ($scope, exerciseAdminService, configService, $modal) {

    function getExerciseList() {
        exerciseAdminService.getAll().$promise.then(function (results) {
            $scope.exercises = results;
        });
    }

    $scope.categories = configService.lookups.functionExerciseCategories;
    getExerciseList();


    $scope.bindSelectedExercise = function (exercise) {
        $scope.selectedExercise = exercise;

        var modalInstance = $modal.open({
            templateUrl: '/app/admin/content/tmpl.exercise.modal.htm',
            controller: 'ExerciseModalController',
            windowClass: 'xx-dialog',
            resolve: {
                selectedExercise: function () {
                    return $scope.selectedExercise;
                }
            }
        });

        modalInstance.result.then(function (exerciseReturned) {
            getExerciseList();
            $scope.selectedExercise = exerciseReturned;
        });
    }




}]);


exerciseAdminModule.factory('exerciseAdminService', ['$resource', function ($resource) {

    var adminExercisePath = '/admin/exercises';


    return {
        get: function (id) {
            var resource = $resource(adminExercisePath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function () {
            return $resource(adminExercisePath).query();
        },
        save: function (exercise) {
            return $resource(adminExercisePath).save(exercise);
        },
        update: function (exercise) {
            var resource = $resource(adminExercisePath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: exercise.id }, exercise);
        }
    }



}]);