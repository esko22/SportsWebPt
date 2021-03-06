﻿'use strict';

var clinicModule = angular.module('clinic.module', []);

clinicModule.controller('ClinicManagerController', [
    '$scope', 'clinicService',
    function ($scope, clinicService) {

        clinicService.getManagedClinics($scope.currentUser.id).$promise.then(function (clinics) {
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
        controller: 'ClniicPatientListController',
        scope: {
            clinicId : '='
        }
    };
}]);

clinicModule.controller('ClinicAddPatientModalController', [
    '$scope', 'clinicService', '$modal', 'clinicId', '$rootScope', 'notifierService', '$modalInstance',
    function ($scope, clinicService, $modal, clinicId, $rootScope, notifierService, $modalInstance) {

        $scope.formData = {};

        $scope.validateUser = function() {
            $scope.userToAdd = null;

            if ($scope.formData.lookupEmail) {
                clinicService.getUserByEmail($scope.formData.lookupEmail).$promise.then(function (user) {

                    if (!user.id) {
                        $scope.userToAdd = { emailAddress: $scope.formData.lookupEmail, id: 0, firstName: '', lastName: '' }
                    } else {
                        $scope.userToAdd = user;
                    }
                });
            }
        }

        $scope.submit = function () {
            if ($scope.userToAdd) {
                clinicService.addPatientToClinic(clinicId, $scope.userToAdd).$promise.then(function () {
                    notifierService.notify('Patient Added To Clinic!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

clinicModule.controller('ClinicAddTherapistModalController', [
    '$scope', 'clinicService', '$modal', 'clinicId', '$rootScope', 'notifierService', '$modalInstance',
    function ($scope, clinicService, $modal, clinicId, $rootScope, notifierService, $modalInstance) {

        $scope.formData = {};

        $scope.validateUser = function () {
            $scope.userToAdd = null;

            if ($scope.formData.lookupEmail) {
                clinicService.getUserByEmail($scope.formData.lookupEmail).$promise.then(function (user) {

                    if (!user.id) {
                        $scope.userToAdd = { emailAddress: $scope.formData.lookupEmail, id: 0, firstName: '', lastName: '' }
                    } else {
                        $scope.userToAdd = user;
                    }
                });
            }
        }

        $scope.submit = function () {
            if ($scope.userToAdd) {
                clinicService.addTherapistToClinic(clinicId, $scope.userToAdd).$promise.then(function () {
                    notifierService.notify('Therapist Added To Clinic!');
                    $modalInstance.close();
                });
            }
        }
    }
]);

clinicModule.controller('ClniicPatientListController', [
    '$scope', 'clinicService', '$modal',
    function ($scope, clinicService, $modal) {

        clinicService.getClinicPatients($scope.clinicId).$promise.then(function (patients) {
            $scope.patients = patients;
        });

        $scope.showAddPatientModal = function() {
            $modal.open({
                templateUrl: '/app/clinic/tmpl.clinic.add.patient.modal.htm',
                controller: 'ClinicAddPatientModalController',
                resolve: {
                    clinicId: function() {
                        return $scope.clinicId;
                    }
                }
            });
        };
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

        clinicService.getClinicTherapists($scope.clinicId).$promise.then(function (therapists) {
            $scope.therapists = therapists;
        });

        $scope.showAddTherapistModal = function() {
            $modal.open({
                templateUrl: '/app/clinic/tmpl.clinic.add.therapist.modal.htm',
                controller: 'ClinicAddTherapistModalController',
                size: 'sm',
                resolve: {
                    clinicId: function() {
                        return $scope.clinicId;
                    }
                }
            });
        };
    }
]);
