﻿'use strict';

var patientModule = angular.module('patient.module', []);


patientModule.controller('PatientDashboardController', ['$scope', 'userManagementService',
    function ($scope, userManagementService) {

        userManagementService.getUser().$promise.then(function(user) {
            $scope.currentUser = user;
        });

    }
]);


patientModule.directive('patientCaseList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/tmpl.patient.case.list.htm',
        controller: 'PatientCaseListController'
    };
}]);

patientModule.controller('PatientCaseListController', [
    '$scope', 'patientService', '$state',
    function ($scope, patientService, $state) {

        getActiveCaseList();

        $scope.caseGridOptions = {
            data: 'cases',
            showGroupPanel: true,
            columnDefs: [
                { field: 'therapistEmail', displayName: 'Therapist' },
                { field: 'name', displayName: 'Name' },
                { field: 'clinic.name', displayName: 'Clinic' },
            { field: 'createdOn', displayName: 'Created' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="showCase(row.entity)" >View</button>' }]
        };

        $scope.showCase = function (caseInstance) {
            $state.go('patient.case', { caseId: caseInstance.id });
        }

        function getActiveCaseList() {
            patientService.getCasesForPatient('active').$promise.then(function (cases) {
                $scope.cases = cases;
            });
        }
    }
]);

patientModule.controller('PatientCaseController', [
    '$scope', 'caseService', '$stateParams',
    function ($scope, caseService, $stateParams) {

        $scope.caseId = $stateParams.caseId;

        caseService.get($stateParams.caseId).$promise.then(function (caseInstance) {
            $scope.case = caseInstance;
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

patientModule.directive('patientCaseSessionList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/tmpl.patient.case.session.list.htm',
        controller: 'PatientCaseSessionListController',
        scope: {
            caseId: '='
        }
    };
}]);

patientModule.controller('PatientCaseSessionListController', [
    '$scope', 'caseService', '$state',
    function ($scope, caseService, $state) {

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
            $state.go('patient.case.session', { sessionId: session.id });
        }

        function getSessionList() {
            caseService.getSessions($scope.caseId).$promise.then(function (sessions) {
                $scope.sessions = sessions;
            });
        };
    }
]);