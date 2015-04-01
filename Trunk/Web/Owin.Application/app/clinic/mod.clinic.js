'use strict';

var clinicModule = angular.module('clinic.module', []);

clinicModule.controller('ClinicManagerController', [
    '$scope', 'clinicService',
    function ($scope, clinicService) {

        clinicService.getManagedClinics().$promise.then(function (clinics) {
            $scope.clinics = clinics;
        });
    }
]);

clinicModule.controller('ClinicDashboardController', [
    '$scope', 'clinicService', '$stateParams',
    function ($scope, clinicService, $stateParams) {
        $scope.clinicId = $stateParams.clinicId;
        clinicService.get($stateParams.clinicId).$promise.then(function (clinic) {
            $scope.clinic = clinic;
        });
    }
]);

clinicModule.directive('clinicPatientList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/clinic/tmpl.clinic.patient.list.htm',
        controller: 'ClinicPatientListController',
        scope: {
            clinicId : '='
        }
    };
}]);

clinicModule.controller('ClinicAddPatientModalController', [
    '$scope', 'clinicService', '$modal', 'clinicId', '$rootScope', 'notifierService', '$modalInstance',
    function ($scope, clinicService, $modal, clinicId, $rootScope, notifierService, $modalInstance) {

        $scope.formData = {};
        $scope.close = $modalInstance.close;

        $scope.submit = function () {
            if ($scope.formData.lookupEmail && $scope.formData.lookupEmail !== '') {
                clinicService.addPatientToClinic(clinicId, { emailAddress: $scope.formData.lookupEmail }).$promise.then(function (clinicPatient) {
                    $scope.formData.clinicPatient = clinicPatient;
                });
            }
        }
    }
]);

clinicModule.controller('ClinicAddTherapistModalController', [
    '$scope', 'clinicService', '$modal', 'clinicId', '$rootScope', 'notifierService', '$modalInstance',
    function ($scope, clinicService, $modal, clinicId, $rootScope, notifierService, $modalInstance) {
        $scope.formData = {};
        $scope.close = $modalInstance.close;

        $scope.submit = function () {
            if ($scope.formData.lookupEmail && $scope.formData.lookupEmail !== '') {
                clinicService.addTherapistToClinic(clinicId, { emailAddress: $scope.formData.lookupEmail }).$promise.then(function (clinicTherapist) {
                    $scope.formData.clinicTherapist = clinicTherapist;
                });
            }
        }
    }
]);

clinicModule.controller('ClinicPatientListController', [
    '$scope', 'clinicService', '$modal',
    function ($scope, clinicService, $modal) {

        activate();

        $scope.showAddPatientModal = function() {
            var patientModal = $modal.open({
                templateUrl: '/app/clinic/tmpl.clinic.add.patient.modal.htm',
                controller: 'ClinicAddPatientModalController',
                resolve: {
                    clinicId: function() {
                        return $scope.clinicId;
                    }
                }
            });

            patientModal.result.then(function () {
                activate();
            });
        };



        function activate() {
            clinicService.getClinicPatients($scope.clinicId).$promise.then(function (patients) {
                $scope.patients = patients;
            });
        }

    }
]);


clinicModule.directive('clinicTherapistList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/clinic/tmpl.clinic.therapist.list.htm',
        controller: 'ClniicTherapistListController',
        scope: {
            clinicId: '='
        }
    };
}]);

clinicModule.controller('ClniicTherapistListController', [
    '$scope', 'clinicService', '$modal',
    function ($scope, clinicService, $modal) {

        activate();

        $scope.showAddTherapistModal = function () {

            var therapistModal = $modal.open({
                templateUrl: '/app/clinic/tmpl.clinic.add.therapist.modal.htm',
                controller: 'ClinicAddTherapistModalController',
                size: 'sm',
                resolve: {
                    clinicId: function() {
                        return $scope.clinicId;
                    }
                }
            });

            therapistModal.result.then(function () {
                activate();
            });
        };

        function activate() {
            clinicService.getClinicTherapists($scope.clinicId).$promise.then(function (therapists) {
                $scope.therapists = therapists;
            });
        }



    }
]);
