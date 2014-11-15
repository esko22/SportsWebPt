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
        controller: 'PatientEpisodeListController'
    };
}]);

patientModule.controller('PatientEpisodeListController', [
    '$scope', 'patientService', '$state',
    function ($scope, patientService, $state) {

        getActiveEpisodeList();

        $scope.episodeGridOptions = {
            data: 'episodes',
            showGroupPanel: true,
            columnDefs: [
                { field: 'therapistEmail', displayName: 'Therapist' },
                { field: 'name', displayName: 'Name' },
                { field: 'clinic.name', displayName: 'Clinic' },
            { field: 'createdOn', displayName: 'Created' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="showEpisode(row.entity)" >View</button>' }]
        };

        $scope.showEpisode = function (episode) {
            $state.go('patient.episode', { episodeId: episode.id });
        }

        function getActiveEpisodeList() {
            patientService.getEpisodesForPatient('active').$promise.then(function (episodes) {
                $scope.episodes = episodes;
            });
        }
    }
]);

patientModule.controller('PatientEpisodeController', [
    '$scope', 'episodeService', '$stateParams',
    function ($scope, episodeService, $stateParams) {

        $scope.episodeId = $stateParams.episodeId;

        episodeService.get($stateParams.episodeId).$promise.then(function (episode) {
            $scope.episode = episode;
        });
    }
]);

patientModule.controller('PatientSessionController', [
    '$scope', 'sessionService', '$stateParams',
    function ($scope, sessionService, $stateParams) {

        $scope.sessionId = $stateParams.sessionId;
        getSessionDetail();

        function getSessionDetail() {
            sessionService.get($stateParams.sessionId).$promise.then(function (session) {
                $scope.session = session;
            });
        }
    }
]);

patientModule.directive('patientEpisodeSessionList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/tmpl.patient.episode.session.list.htm',
        controller: 'PatientEpisodeSessionListController',
        scope: {
            episodeId: '='
        }
    };
}]);

patientModule.controller('PatientEpisodeSessionListController', [
    '$scope', 'episodeService', '$state',
    function ($scope, episodeService, $state) {

        getSessionList();

        $scope.sessionGridOptions = {
            data: 'sessions',
            columnDefs: [
                { field: 'sessionType', displayName: 'Type' },
                { field: 'scheduledAt', displayName: 'Scheduled' },
                { field: 'executed', displayName: 'Executed At' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="showSession(row.entity)" > View </button>' }]
        };

        $scope.showSession = function (session) {
            $state.go('patient.episode.session', { sessionId: session.id });
        }

        function getSessionList() {
            episodeService.getSessions($scope.episodeId).$promise.then(function (sessions) {
                $scope.sessions = sessions;
            });
        };
    }
]);