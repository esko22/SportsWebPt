'use strict';

var therapistModule = angular.module('therapist.module', []);


therapistModule.controller('TherapistDashboardController', [
    '$scope', 'therapistService','$rootScope',
    function($scope, therapistService, $rootScope) {


        $rootScope.$watch('currentUser', function (currentUser) {
            if (currentUser) {
                therapistService.get().$promise.then(function (therapistDetails) {
                    $scope.clinics = therapistDetails.clinics;
                });
            }
        });
    }
]);

therapistModule.controller('TherapistCaseController', [
    '$scope', 'caseService', '$stateParams',
    function ($scope, caseService, $stateParams) {

        $scope.caseId = $stateParams.caseId;

        caseService.get($stateParams.caseId).$promise.then(function (caseInstance) {
            $scope.case = caseInstance;
        });
    }
]);

therapistModule.directive('therapistPlanList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.plan.list.htm',
        controller: 'TherapistPlanController'
    };
}]);

therapistModule.controller('TherapistPlanController', [
    '$scope', 'planAdminService', 'userManagementService', '$modal','$rootScope',
    function ($scope, planAdminService, userManagementService, $modal, $rootScope) {

        $rootScope.$watch('currentUser', function (currentUser) {
            if(currentUser)
              getPlanList(currentUser);
        });

        $scope.planGridOptions = {
            data: 'plans',
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'routineName', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindSelectedPlan(row.entity)" >Edit</button> <button id="editBtn" type="button" class="btn-small" ng-click="setSharedPlanSettings(row.entity)" >Share</button>' }]
        };

        $scope.bindSelectedPlan = function (plan) {
            $scope.selectedPlan = plan;

            var modalInstance = $modal.open({
                templateUrl: '/app/admin/plans/tmpl.plan.modal.htm',
                controller: 'PlanModalController',
                windowClass: 'xx-dialog',
                resolve: {
                    selectedPlan: function () {
                        return $scope.selectedPlan;
                    }
                }
            });

            modalInstance.result.then(function (planReturned) {
                getPlanList();
                $scope.selectedPlan = planReturned;
            });
        }


        $scope.setSharedPlanSettings = function (plan) {
            $scope.selectedPlan = plan;

            $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.shard.plan.modal.htm',
                controller: 'SharedPlanModalController',
                resolve: {
                    clinics: function () {
                        return $scope.clinics;
                    },
                    selectedPlan: function () {
                        return $scope.selectedPlan;
                    }
                }
            });
        }


        function getPlanList() {
            planAdminService.getPlansForTherapist().$promise.then(function (plans) {
                $scope.plans = plans;
            });
        }
    }
]);

therapistModule.controller('SharedPlanModalController', [
    '$scope', 'therapistService', '$modal', 'selectedPlan', 'clinics','$rootScope','notifierService','$modalInstance',
    function ($scope, therapistService, $modal, selectedPlan, clinics, $rootScope, notifierService, $modalInstance) {

        $scope.sharedPlans = [];

        therapistService.getSharedPlansForTherapist(selectedPlan.id).$promise.then(function(results) {
            angular.forEach(clinics, function (clinic) {
                var sharedPlanRecord = _.findWhere(results, { clinicId: clinic.id });
                if (sharedPlanRecord) {
                    $scope.sharedPlans.push(sharedPlanRecord);
                } else {
                    $scope.sharedPlans.push({ clinicName : clinic.name, clinicId : clinic.id, isActive : false, planId : selectedPlan.id });
                }
            });
        });

        $scope.submit = function () {
            if ($scope.sharedPlans.length > 0) {
                therapistService.updateSharedPlans($scope.sharedPlans).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

therapistModule.controller('SharedExerciseModalController', [
    '$scope', 'therapistService', '$modal', 'selectedExercise', 'clinics', '$rootScope', 'notifierService','$modalInstance',
    function ($scope, therapistService, $modal, selectedExercise, clinics, $rootScope, notifierService, $modalInstance) {

        $scope.sharedExercises = [];

        therapistService.getSharedExercisesForTherapist(selectedExercise.id).$promise.then(function (results) {
            angular.forEach(clinics, function (clinic) {
                var sharedExerciseRecord = _.findWhere(results, { clinicId: clinic.id });
                if (sharedExerciseRecord) {
                    $scope.sharedExercises.push(sharedExerciseRecord);
                } else {
                    $scope.sharedExercises.push({ clinicName: clinic.name, clinicId: clinic.id, isActive: false, exerciseId: selectedExercise.id });
                }
            });
        });

        $scope.submit = function () {
            if ($scope.sharedExercises.length > 0) {
                therapistService.updateSharedExercises($scope.sharedExercises).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

therapistModule.controller('TherapistExerciseController', [
    '$scope', 'exerciseAdminService', '$modal','$rootScope',
    function ($scope, exerciseAdminService, $modal, $rootScope) {

        $rootScope.$watch('currentUser', function (currentUser) {
            if (currentUser)
                getExerciseList();
        });


        $scope.exerciseGridOptions = {
            data: 'exercises',
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'name', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindSelectedExercise(row.entity)" >Edit</button> <button id="editBtn" type="button" class="btn-small" ng-click="setSharedExerciseSettings(row.entity)" >Share</button>' }]
        };

        $scope.bindSelectedExercise = function (exercise) {
            $scope.selectedExercise = exercise;

            var modalInstance = $modal.open({
                templateUrl: '/app/admin/exercises/tmpl.exercise.modal.htm',
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

        $scope.setSharedExerciseSettings = function (exercise) {
            $scope.selectedExercise = exercise;

            $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.shard.exercise.modal.htm',
                controller: 'SharedExerciseModalController',
                resolve: {
                    clinics: function () {
                        return $scope.clinics;
                    },
                    selectedExercise: function () {
                        return $scope.selectedExercise;
                    }
                }
            });
        }


        function getExerciseList() {
            exerciseAdminService.getExercisesForTherapist().$promise.then(function (exercises) {
                $scope.exercises = exercises;
            });
        }
    }
]);

therapistModule.directive('therapistExerciseList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.exercise.list.htm',
        controller: 'TherapistExerciseController'
    };
}]);


therapistModule.directive('therapistCaseList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.case.list.htm',
        controller: 'TherapistCaseListController'
    };
}]);

therapistModule.directive('therapistCaseSessionList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.case.session.list.htm',
        controller: 'TherapistCaseSessionListController',
        scope: {
            caseId : '='
        }
    };
}]);

therapistModule.controller('TherapistCaseSessionListController', [
    '$scope', 'caseService', '$modal','$state',
    function ($scope, caseService, $modal, $state) {

        getSessionList();

        $scope.sessionGridOptions = {
            data: 'sessions',
            showGroupPanel: true,
            columnDefs: [
                { field: 'sessionType', displayName: 'Type' },
                { field: 'scheduledStartTime', displayName: 'Scheduled' },
                { field: 'scheduledEndTime', displayName: 'End' },
                { field: 'notes', displayName: 'notes' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="showSession(row.entity)" > View </button>' }]
        };

        $scope.addSession = function() {
            var modalInstance = $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.session.modal.htm',
                controller: 'TherapistSessionModalController',
                resolve: {
                    caseId: function () {
                        return $scope.caseId;
                    }
                }
            });

            modalInstance.result.then(function () {
                getSessionList();
            });
        }

        $scope.showSession = function (session) {
            $state.go('therapist.session', { sessionId: session.id });
        }

        function getSessionList() {
            caseService.getSessions($scope.caseId).$promise.then(function (sessions) {
                $scope.sessions = sessions;
            });
        };
    }
]);


therapistModule.controller('TherapistSessionModalController', [
    '$scope', 'sessionService', '$modal', 'caseId', '$rootScope', 'notifierService', '$modalInstance', '$http','$timeout',
    function($scope, sessionService, $modal, caseId, $rootScope, notifierService, $modalInstance, $http, $timeout) {

        $scope.session = {
            time: new Date(),
            duration: 30
        };

        $scope.gtmData = {};
        //TODO: move these to config
        $scope.timePicker = {
            showMeridian: true,
            meridians: ['AM', 'PM'],
            mStep: 15,
            hStep: 1
        };

        $scope.open = function($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.opened = true;
        };

        $scope.session.time.setMinutes(0);
        $scope.session.time.setSeconds(0);

        $scope.datePicker = function() {
            return {
                clear: function() {
                    $scope.dt = null;
                },
                disabled: function(date, mode) {
                    return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
                },
                toggleMin: function() {
                    $scope.minDate = $scope.minDate ? null : new Date();
                },
                dateOptions: {
                    formatYear: 'yy',
                    startingDay: 1
                }
            }
        }

        $scope.launchGTM = function() {
            var authWindow = window.open('https://api.citrixonline.com/oauth/authorize?client_id=uDtwgMdiQaxYomZuIR5nncG1otbpnTVp', 'gtmAuthWindow', 'width=800, height=600');

            var pollTimer = window.setInterval(function() {
                try {
                    if (authWindow.document.URL.indexOf('code') != -1) {
                        window.clearInterval(pollTimer);
                        var url = authWindow.document.URL;
                        $scope.responseKey = gup(url, 'code');
                        $scope.getAccessToken($scope.responseKey);
                        authWindow.close();
                    }
                } catch (err) {
                    console.log(err);
                }
            }, 500);
        }

        $scope.addVideoMeeting = function() {
            var endTime = new Date($scope.session.time.getTime() + $scope.session.duration * 60000);
            var meeting = {
                "subject": "Patient Session",
                "starttime": $scope.session.time.toISOString(),
                "endtime": endTime.toISOString(),
                "passwordrequired": "false",
                "conferencecallinfo": "Hybrid",
                "timezonekey": "",
                "meetingtype": "Scheduled"
            }

            return $http.post('https://api.citrixonline.com/G2M/rest/meetings', meeting, {
                headers: {
                    'Authorization': 'OAuth oauth_token=' + $scope.gtmData.access_token
                }
            });
        }

        $scope.postSession = function() {
            if ($scope.session) {
                var endTime = new Date($scope.session.time.getTime() + $scope.session.duration * 60000);

                $scope.session.caseId = caseId;
                $scope.session.scheduledStartTime = $scope.session.time.toUTCString();
                $scope.session.scheduledEndTime = endTime.toUTCString();

                if ($scope.session.videoMeetingDetail) {
                    $scope.session.videoMeetingUri = $scope.session.videoMeetingDetail.joinURL;
                }
            }

            $scope.session.scheduledWithId = $rootScope.currentUser.id;
            sessionService.addSession($scope.session).$promise.then(function() {
                notifierService.notify('Session Create Success!');
                $modalInstance.close();
            });
        };

        $scope.getAccessToken = function(responseKey) {
            $http({ method: 'GET', url: 'https://api.citrixonline.com/oauth/access_token?grant_type=authorization_code&code=' + responseKey + '&client_id=uDtwgMdiQaxYomZuIR5nncG1otbpnTVp' }).
                success(function(data, status, headers, config) {
                    $scope.gtmData.access_token = data.access_token;
                }).
                error(function(data, status, headers, config) {
                    console.log(data);
                });
        };

        //credits: http://www.netlobo.com/url_query_string_javascript.html
        function gup(url, name) {
            name = name.replace(/[[]/, "\[").replace(/[]]/, "\]");
            var regexS = "[\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(url);
            if (results == null)
                return "";
            else
                return results[1];
        }

        $scope.submit = function() {
            if ($scope.session.sessionType === 'Video') {
                $scope.addVideoMeeting().success(function(data) {
                    $scope.session.videoMeetingDetail = data[0];
                    $scope.postSession();
                }).error(function (err) {
                    notifierService.error('Error Creating GTM Instance!');
                    console.log(err);
                });
            } else {
                $scope.postSession();
            }
        };
    }
]);

therapistModule.controller('TherapistSessionPlanModalController', [
    '$scope', 'sessionService', '$modal', 'sessionId', '$rootScope', 'notifierService', '$modalInstance', 'planAdminService',
    function($scope, sessionService, $modal, sessionId, $rootScope, notifierService, $modalInstance, planAdminService) {

        $scope.selectedPlans = [];
        $scope.sessionId = sessionId;

        $rootScope.$watch('currentUser', function(currentUser) {
            if (currentUser)
                getPlanList();
        });


        $scope.planGridOptions = {
            data: 'plans',
            selectedItems: $scope.selectedPlans,
            showGroupPanel: true,
            columnDefs: [
                { field: 'id', displayName: 'Id' },
                { field: 'routineName', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
                { field: 'categories', displayName: 'Category' }
            ]
        };

        $scope.submit = function() {
            if ($scope.sessionId > 0) {
                sessionService.addSessionPlans($scope.sessionId, _.pluck($scope.selectedPlans, 'id')).$promise.then(function() {
                    notifierService.notify('Session Plans Added!');
                    $modalInstance.close();
                });
            }
        }

        function getPlanList() {
            planAdminService.getPlansForTherapist().$promise.then(function(plans) {
                $scope.plans = plans;
            });
        }
    }
]);

therapistModule.controller('TherapistCaseModalController', [
    '$scope', 'caseService', '$modal', '$rootScope', 'notifierService', '$modalInstance', 'clinics', 'clinicService',
    function ($scope, caseService, $modal, $rootScope, notifierService, $modalInstance, clinics, clinicService) {

        $scope.clinics = clinics;
        $scope.case = {};
        $scope.selectedPatients = [];

        $scope.onClinicChange = function() {
            clinicService.getClinicPatients($scope.case.clinic.id).$promise.then(function(patients) {
                $scope.patients = patients;
            });
        }

        $scope.patientGridOptions = {
            data: 'patients',
            showGroupPanel: true,
            selectedItems: $scope.selectedPatients,
            multiSelect: false,
            columnDefs: [
                { field: 'user.emailAddress', displayName: 'Email' },
                { field: 'clinicPatientIdentifier', displayName: 'Identifier' }]
        };

        $scope.submit = function () {
            if ($scope.case) {
                $scope.case.clinicId = $scope.case.clinic.id;
                $scope.case.clinicPatientId = $scope.selectedPatients[0].id;

                caseService.addCase($scope.case).$promise.then(function () {
                    notifierService.notify('Case Create Success!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

therapistModule.controller('TherapistExamineSessionController', ['$scope','$state','examineSymptomsService','sessionService',
    function ($scope, $state, examineSymptomsService, sessionService) {
        $scope.selectedAreas = [];
        $scope.report = {};
        $scope.report.potentialInjuries = [];

        $scope.currentStep = {};
        $scope.currentStep.order = 0;

        $scope.skeletonNextState = 'therapist.session.symptoms';
        $scope.symptomBackState = 'therapist.session.skeleton';

        $scope.symptomsEnabled = true;


        $scope.$watch('session', function (session) {
            if (session) {
                if (session.differentialDiagnosisId == 0) {
                    $state.go('therapist.session.skeleton');
                } else {
                    $state.go('therapist.session.report');
                }
            }
        });

        $scope.submitReport = function () {

            var allBodyParts = [];
            angular.forEach($scope.selectedAreas, function (selectedArea) {
                angular.forEach(selectedArea.bodyParts, function (bodyPart) {
                    allBodyParts.push(bodyPart);
                });
            });

            examineSymptomsService.submitReport(allBodyParts, $scope.session.id).then(function (response) {
                sessionService.get($scope.session.id).$promise.then(function(session) {
                    $scope.session = session;
                    $state.go('therapist.session.report');
                });
            });
        };

    }
]);

therapistModule.controller('TherapistExamineReportController', [
    '$scope','examineSymptomsService',
    function ($scope, examineSymptomsService) {

        $scope.symptomsEnabled = true;

        if (!$scope.session.diagnosis) {
            examineSymptomsService.getReport($scope.session.differentialDiagnosisId).$promise.then(function(report) {
                $scope.session.diagnosis = report;
            });
        }
    }
]);


therapistModule.controller('TherapistSessionController', [
    '$scope', 'sessionService', '$stateParams','$modal','$state',
    function ($scope, sessionService, $stateParams, $modal, $state) {

        $scope.sessionId = $stateParams.sessionId;
        getSessionDetail();



        $scope.addSessionPlan = function () {
            var modalInstance = $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.session.plan.modal.htm',
                controller: 'TherapistSessionPlanModalController',
                resolve: {
                    sessionId: function () {
                        return $scope.sessionId;
                    }
                }
            });

            modalInstance.result.then(function () {
                getSessionDetail();
            });

        }


        function getSessionDetail() {
            sessionService.get($stateParams.sessionId).$promise.then(function(session) {
                $scope.session = session;
            });
        }
    }
]);

therapistModule.controller('TherapistCaseListController', [
    '$scope', 'therapistService', '$modal','$state','$rootScope',
    function ($scope, therapistService, $modal, $state, $rootScope) {

        $rootScope.$watch('currentUser', function (currentUser) {
            if(currentUser)
                getActiveCaseList();
        });

        $scope.caseGridOptions = {
            data: 'cases',
            showGroupPanel: true,
            columnDefs: [
                { field: 'clinicPatientIdentifier', displayName: 'Patient' },
                { field: 'patientEmail', displayName: 'Email' },
                { field: 'name', displayName: 'Name' },
                { field: 'clinic.name', displayName: 'Clinic' },
            { field: 'createdOn', displayName: 'Created' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="showCase(row.entity)" > View </button>' }]
        };

        $scope.addCase = function() {
            var modalInstance = $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.case.modal.htm',
                controller: 'TherapistCaseModalController',
                resolve: {
                    clinics: function () {
                        return $scope.clinics;
                    }
                }
            });

            modalInstance.result.then(function () {
                getActiveCaseList();
            });

        }

        $scope.showCase = function (caseInstance) {
            $state.go('therapist.case', { caseId: caseInstance.id });
        }

        function getActiveCaseList() {
            therapistService.getCasesForTherapist('active').$promise.then(function (cases) {
                $scope.cases = cases;
            });
        }
    }
]);


