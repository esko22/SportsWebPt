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


exerciseAdminModule.controller('ExerciseModalController', ['$scope', 'exerciseAdminService', '$modalInstance', 'selectedExercise','notifierService', function ($scope, exerciseAdminService, $modalInstance, selectedExercise, notifierService) {

        $scope.exericse = selectedExercise;

        $scope.ok = function () {
            if ($scope.exericse.id > 0) {
                exerciseAdminService.update($scope.exericse).$promise.then(function () {
                    notifierService.notify('Update Success!');
                });
            } else {
                exerciseAdminService.save($scope.exericse).$promise.then(function () {
                    notifierService.notify('Created Successfully!');
                });
            }
            $modalInstance.close($scope.exericse);
        };

        $scope.cancel = function () {
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

    $scope.editorOptions = configService.kendoEditorOptions;

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
            $scope.selectedExercise = exerciseReturned;
            getExerciseList();
        });
    }




}]);


exerciseAdminModule.factory('exerciseAdminService', ['$resource', function ($resource) {

    var adminExercisePath = '/admin/exercises/';


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