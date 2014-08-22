'use strict';

var patientModule = angular.module('patient.module', []);


patientModule.controller('PatientDashboardController', ['$scope', 'userManagementService',
    function ($scope, userManagementService) {

        userManagementService.getUser().$promise.then(function(user) {
            $scope.currentUser = user;
        });

    }
]);


patientModule.directive('patientEpisodeList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/tmpl.patient.episode.list.htm',
        controller: 'PatientEpisodeController'
    };
}]);

patientModule.controller('PatientEpisodeController', [
    '$scope', 'patientService', '$modal',
    function ($scope, patientService, $modal) {

        getActiveEpisodeList();

        $scope.episodeGridOptions = {
            data: 'episodes',
            showGroupPanel: true,
            columnDefs: [
                { field: 'therapistEmail', displayName: 'Therapist' },
                { field: 'name', displayName: 'Name' },
                { field: 'clinic', displayName: 'Clinic' },
            { field: 'createdOn', displayName: 'Created' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindSelectedEpisode(row.entity)" >View</button>' }]
        };

        $scope.bindSelectedEpisode = function (episode) {
            //$scope.selectedExercise = exercise;

            //var modalInstance = $modal.open({
            //    templateUrl: '/app/admin/exercises/tmpl.exercise.modal.htm',
            //    controller: 'ExerciseModalController',
            //    windowClass: 'xx-dialog',
            //    resolve: {
            //        selectedExercise: function () {
            //            return $scope.selectedExercise;
            //        }
            //    }
            //});

            //modalInstance.result.then(function (exerciseReturned) {
            //    getExerciseList();
            //    $scope.selectedExercise = exerciseReturned;
            //});
        }

        function getActiveEpisodeList() {
            patientService.getEpisodesForPatient($scope.currentUser.id, 'active').$promise.then(function (episodes) {
                $scope.episodes = episodes;
            });
        }
    }
]);