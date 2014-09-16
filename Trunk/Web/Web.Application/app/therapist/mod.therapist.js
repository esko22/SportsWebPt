'use strict';

var therapistModule = angular.module('therapist.module', []);


therapistModule.controller('TherapistDashboardController', [
    '$scope', 'therapistService',
    function ($scope, therapistService) {

        therapistService.get($scope.currentUser.id).$promise.then(function(therapistDetails) {
            $scope.clinics = therapistDetails.clinics;
        });
    }
]);

therapistModule.controller('TherapistEpisodeController', [
    '$scope', 'episodeService', '$stateParams',
    function ($scope, episodeService, $stateParams) {

        $scope.episodeId = $stateParams.episodeId;

        episodeService.get($stateParams.episodeId).$promise.then(function (episode) {
            $scope.episode = episode;
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
    '$scope', 'planAdminService', 'userManagementService', '$modal',
    function ($scope, planAdminService, userManagementService, $modal) {

        getPlanList();

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
            planAdminService.getPlansForTherapist($scope.currentUser.id).$promise.then(function (plans) {
                $scope.plans = plans;
            });
        }
    }
]);

therapistModule.controller('SharedPlanModalController', [
    '$scope', 'therapistService', '$modal', 'selectedPlan', 'clinics','$rootScope','notifierService','$modalInstance',
    function ($scope, therapistService, $modal, selectedPlan, clinics, $rootScope, notifierService, $modalInstance) {

        $scope.sharedPlans = [];

        therapistService.getSharedPlansForTherapist($rootScope.currentUser.id, selectedPlan.id).$promise.then(function(results) {
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
                therapistService.updateSharedPlans($rootScope.currentUser.id, $scope.sharedPlans).$promise.then(function () {
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

        therapistService.getSharedExercisesForTherapist($rootScope.currentUser.id, selectedExercise.id).$promise.then(function (results) {
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
                therapistService.updateSharedExercises($rootScope.currentUser.id, $scope.sharedExercises).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

therapistModule.controller('TherapistExerciseController', [
    '$scope', 'exerciseAdminService', '$modal',
    function ($scope, exerciseAdminService, $modal) {

        getExerciseList();

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
            exerciseAdminService.getExercisesForTherapist($scope.currentUser.id).$promise.then(function (exercises) {
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


therapistModule.directive('therapistEpisodeList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.episode.list.htm',
        controller: 'TherapistEpisodeListController'
    };
}]);

therapistModule.directive('therapistEpisodeSessionList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/therapist/tmpl.therapist.episode.session.list.htm',
        controller: 'TherapistEpisodeSessionListController',
        scope: {
            episodeId : '='
        }
    };
}]);

therapistModule.controller('TherapistEpisodeSessionListController', [
    '$scope', 'episodeService', '$modal','$state',
    function ($scope, episodeService, $modal, $state) {

        getSessionList();

        $scope.sessionGridOptions = {
            data: 'sessions',
            showGroupPanel: true,
            columnDefs: [
                { field: 'sessionType', displayName: 'Type' },
                { field: 'scheduledAt', displayName: 'Scheduled' },
                { field: 'executed', displayName: 'Executed At' },
                { field: 'notes', displayName: 'notes' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="showSession(row.entity)" > View </button>' }]
        };

        $scope.addSession = function() {
            var modalInstance = $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.session.modal.htm',
                controller: 'TherapistSessionModalController',
                resolve: {
                    episodeId: function () {
                        return $scope.episodeId;
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
            episodeService.getSessions($scope.episodeId).$promise.then(function (sessions) {
                $scope.sessions = sessions;
            });
        };
    }
]);


therapistModule.controller('TherapistSessionModalController', [
    '$scope', 'sessionService', '$modal', 'episodeId', '$rootScope', 'notifierService', '$modalInstance', '$http','$timeout',
    function ($scope, sessionService, $modal, episodeId, $rootScope, notifierService, $modalInstance, $http, $timeout) {

        $scope.session = {};
        $scope.gtmData = {}; 

        $scope.launchGTM = function() {
            var authWindow = window.open('https://api.citrixonline.com/oauth/authorize?client_id=uDtwgMdiQaxYomZuIR5nncG1otbpnTVp', 'gtmAuthWindow', 'width=800, height=600');

            var pollTimer   =   window.setInterval(function () {
                try {
                    if (authWindow.document.URL.indexOf('code') != -1) {
                        window.clearInterval(pollTimer);
                        var url = authWindow.document.URL;
                        var responseKey = gup(url, 'code');
                        authWindow.close();

                        $scope.getAccessToken(responseKey);
                    }
                } catch (err) {
                    console.log(err);
                }
            }, 500);
        }




        $scope.getAccessToken = function(responseKey) {
            $http({ method: 'GET', url: 'https://api.citrixonline.com/oauth/access_token?grant_type=authorization_code&code=' + responseKey + '&client_id=uDtwgMdiQaxYomZuIR5nncG1otbpnTVp' }).
                success(function (data, status, headers, config) {
                    $scope.gtmData.access = data;


                    $http.defaults.headers.common['Authorization'] = 'OAuth oauth_token=' + data.access_token;
                    $http.defaults.headers.common['Accept'] = 'application/json';
                    $http.defaults.headers.common['Content-type'] = 'application/json';
                    var meeting = {
                        "subject": "test",
                        "starttime": "2014-12-01T09:00:00",
                        "endtime": "2014-12-01T10:00:00",
                        "passwordrequired": "false",
                        "conferencecallinfo": "Hybrid",
                        "timezonekey": "",
                        "meetingtype": "Scheduled"
                    }

                    $http.post('https://api.citrixonline.com/G2M/rest/meetings', meeting).success(function(data2) {
                        $scope.meetingtype = data2;
                    });


                }).
                error(function(data, status, headers, config) {
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

        $scope.submit = function () {
            if ($scope.session) {
                $scope.session.episodeId = episodeId;
                $scope.session.scheduledWithId = $rootScope.currentUser.id;
                sessionService.addSession($scope.session).$promise.then(function () {
                    notifierService.notify('Session Create Success!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

therapistModule.controller('TherapistSessionPlanModalController', [
    '$scope', 'sessionService', '$modal', 'sessionId', '$rootScope', 'notifierService', '$modalInstance', 'planAdminService',
function ($scope, sessionService, $modal, sessionId, $rootScope, notifierService, $modalInstance, planAdminService) {

    $scope.selectedPlans = [];
    $scope.sessionId = sessionId;
        getPlanList();

        $scope.planGridOptions = {
            data: 'plans',
            selectedItems: $scope.selectedPlans,
            showGroupPanel: true,
            columnDefs: [{ field: 'id', displayName: 'Id' },
                { field: 'routineName', displayName: 'Name' },
                { field: 'bodyRegions', displayName: 'Body Regions' },
            { field: 'categories', displayName: 'Category' }]
        };

        $scope.submit = function () {
            if ($scope.sessionId > 0) {
                sessionService.addSessionPlans($scope.sessionId, _.pluck($scope.selectedPlans, 'id')).$promise.then(function () {
                    notifierService.notify('Session Plans Added!');
                    $modalInstance.close();
                });
            }
        }

        function getPlanList() {
            planAdminService.getPlansForTherapist($rootScope.currentUser.id).$promise.then(function (plans) {
                $scope.plans = plans;
            });
        }
    }
]);

therapistModule.controller('TherapistEpisodeModalController', [
    '$scope', 'episodeService', '$modal', '$rootScope', 'notifierService', '$modalInstance', 'clinics', 'clinicService',
    function ($scope, episodeService, $modal, $rootScope, notifierService, $modalInstance, clinics, clinicService) {

        $scope.clinics = clinics;
        $scope.episode = {};
        $scope.selectedPatients = [];

        $scope.onClinicChange = function() {
            clinicService.getClinicPatients($scope.episode.clinic.id).$promise.then(function(patients) {
                $scope.patients = patients;
            });
        }

        $scope.patientGridOptions = {
            data: 'patients',
            showGroupPanel: true,
            selectedItems: $scope.selectedPatients,
            multiSelect: false,
            columnDefs: [
                { field: 'emailAddress', displayName: 'Email' }]
        };

        $scope.submit = function () {
            if ($scope.episode) {
                $scope.episode.clinicId = $scope.episode.clinic.id;
                $scope.episode.patientId = $scope.selectedPatients[0].id;
                $scope.episode.therapistId = $rootScope.currentUser.id;

                episodeService.addEpisode($scope.episode).$promise.then(function () {
                    notifierService.notify('Episode Create Success!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

therapistModule.controller('TherapistSessionController', [
    '$scope', 'sessionService', '$stateParams','$modal',
    function ($scope, sessionService, $stateParams,$modal) {

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

therapistModule.controller('TherapistEpisodeListController', [
    '$scope', 'therapistService', '$modal','$state',
    function ($scope, therapistService, $modal, $state) {

        getActiveEpisodeList();

        $scope.episodeGridOptions = {
            data: 'episodes',
            showGroupPanel: true,
            columnDefs: [
                { field: 'patientEmail', displayName: 'Patient' },
                { field: 'name', displayName: 'Name' },
                { field: 'clinic.name', displayName: 'Clinic' },
            { field: 'createdOn', displayName: 'Created' },
            { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="showEpisode(row.entity)" > View </button>' }]
        };

        $scope.addEpisode = function() {
            var modalInstance = $modal.open({
                templateUrl: '/app/therapist/tmpl.therapist.episode.modal.htm',
                controller: 'TherapistEpisodeModalController',
                resolve: {
                    clinics: function () {
                        return $scope.clinics;
                    }
                }
            });

            modalInstance.result.then(function () {
                getActiveEpisodeList();
            });

        }

        $scope.showEpisode = function (episode) {
            $state.go('therapist.episode', { episodeId: episode.id });
        }

        function getActiveEpisodeList() {
            therapistService.getEpisodesForTherapist($scope.currentUser.id, 'active').$promise.then(function (episodes) {
                $scope.episodes = episodes;
            });
        }
    }
]);


