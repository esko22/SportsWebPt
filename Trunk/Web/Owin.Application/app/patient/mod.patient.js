'use strict';

var patientModule = angular.module('patient.module', []);


patientModule.controller('PatientDashboardController', ['$scope', 'userManagementService',
    function ($scope, userManagementService) {

        userManagementService.getUser().$promise.then(function(user) {
            $scope.currentUser = user;
        });

        $scope.selectedTab = 'caseDisplay';
        $scope.onTabSelect = function (tab) {
            $scope.selectedTab = tab;
        }
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

patientModule.directive('patientCaseDisplay', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/tmpl.patient.case.display.htm',
        controller: 'PatientCaseDisplayController'
    };
}]);

patientModule.directive('patientCasePreview', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/tmpl.patient.case.preview.htm',
        controller: 'PatientCasePreviewController'
    };
}]);

patientModule.directive('patientBookmarkDisplay', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/tmpl.patient.bookmark.display.htm'
    };
}]);

patientModule.directive('patientSessionDetail', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/tmpl.patient.session.detail.htm',
        controller: 'PatientSessionDetailController',
        scope: {
            session: '='
        }
    };
}]);

patientModule.controller('PatientSessionDetailController', [
    '$scope',
    function ($scope) {
    }
]);


patientModule.directive('patientDashboard', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/patient/prtl.patient.dashboard.htm',
        controller: 'PatientDashboardController'
    };
}]);

patientModule.controller('PatientCaseListController', [
    '$scope', 'patientService', '$state',
    function ($scope, patientService, $state) {

        getActiveCaseList();

        $scope.caseGridOptions = {
            data: 'cases',
            showGroupPanel: true,
            multiSelect: false,
            afterSelectionChange: function(rowItem) {
                if (rowItem.selected) {
                    $scope.showCase(rowItem.entity);
                }
            },

            columnDefs: [
                { field: 'therapistEmail', displayName: 'Therapist' },
                { field: 'name', displayName: 'Name' },
                { field: 'clinic.name', displayName: 'Clinic' },
                { field: 'createdOn', displayName: 'Created' }
            ]
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

patientModule.controller('PatientCasePreviewController', [
    '$scope', 'patientService',
    function ($scope, patientService) {

        $scope.slyOptions = {
            horizontal: 1, // or 0 depending on sly-horizontal or sly-vertical
            itemNav: 'basic',
            smart: 1,
            activateOn: 'click',
            mouseDragging: 1,
            touchDragging: 1,
            releaseSwing: 1,
            startAt: 0,
            scrollBy: 1,
            activatePageOn: 'click',
            speed: 300,
            elasticBounds: 1,
            easing: 'swing',
            dragHandle: 1,
            dynamicHandle: 1,
            clickBar: 1,
        };

        $scope.showCaseSnapshot = function(caseSnapshot) {
            $scope.selectedPlan = null;
            $scope.caseSnapshot = caseSnapshot;
        };

        $scope.showAssignment = function(plan) {
            $scope.selectedPlan = plan;
        };

        patientService.getPatientSnapshot().$promise.then(function (snapshot) {
            $scope.snapshot = snapshot;
            if (snapshot.activeCases)
                $scope.caseSnapshot = snapshot.activeCases[0];
        });
    }
]);


patientModule.controller('PatientCaseDisplayController', [
    '$scope', 'caseService', '$stateParams',
    function ($scope, caseService, $stateParams) {

        $scope.caseId = $stateParams.caseId;

        caseService.get($stateParams.caseId).$promise.then(function (caseInstance) {
            $scope.case = caseInstance;
        });
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

        $scope.selectedTab = 'sessionDetail';
        $scope.onTabSelect = function (tab) {
            $scope.selectedTab = tab;
        }


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
            multiSelect: false,
            afterSelectionChange: function (rowItem) {
                if (rowItem.selected) {
                    $scope.showSession(rowItem.entity);
                }
            },
            columnDefs: [
                { field: 'sessionType', displayName: 'Type' },
                { field: 'scheduledStartTime', displayName: 'Scheduled' },
                { field: 'scheduledEndTime', displayName: 'End' },
                { field: 'videoMeetingUri', displayName: 'Meeting Link' }]
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