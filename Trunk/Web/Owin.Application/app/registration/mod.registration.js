'use strict';

var registrationModule = angular.module('registration.module', []);


registrationModule.controller('RegistrationPatientController', [
    '$scope', 'registrationService', '$stateParams', 'authenticationService', '$state', 'notifierService','clinicService',
    function ($scope, registrationService, $stateParams, authenticationService, $state, notifierService, clinicService) {

        $scope.isAuthenticated = authenticationService.isAuthenticated();
        $scope.registrationDetails = {};

        clinicService.get($stateParams.clinicId).$promise.then(function(clinic) {
            $scope.clinic = clinic;
        });

        $scope.startRegistration = function () {
            authenticationService.signIn(encodeURIComponent('/register/' + $stateParams.clinicId + '/patient'));
        };

        $scope.validate = function() {
            $scope.isValidated = false;
            registrationService.validatePatient($scope.registrationDetails.emailAddress, $scope.registrationDetails.pin).$promise.then(function (clinicPatient) {
                if (clinicPatient) {
                    notifierService.notify('You have been registered successfully!');
                    $state.go('patient.dashboard');
                } else {
                    $scope.error = "Invalid Email / Pin Combo, Please try again";
                }
            },function(error) {
                $scope.error = "Invalid Email / Pin Combo, Please try again";
            });
        }
    }
]);



registrationModule.controller('RegistrationTherapistController', [
    '$scope', 'registrationService', '$stateParams', 'authenticationService', '$state', 'notifierService','clinicService',
    function ($scope, registrationService, $stateParams, authenticationService, $state, notifierService, clinicService) {

        $scope.isAuthenticated = authenticationService.isAuthenticated();
        $scope.registrationDetails = {};
        clinicService.get($stateParams.clinicId).$promise.then(function (clinic) {
            $scope.clinic = clinic;
        });

        $scope.startRegistration = function () {
            authenticationService.signIn(encodeURIComponent('/register/' + $stateParams.clinicId + '/therapist'));
        };

        $scope.validate = function () {
            $scope.isValidated = false;
            registrationService.validateTherapist($scope.registrationDetails.emailAddress, $scope.registrationDetails.pin).$promise.then(function (clinicPatient) {
                if (clinicPatient) {
                    notifierService.notify('You have been registered successfully!');
                    authenticationService.signIn('/therapist');
                } else {
                    $scope.error = "Invalid Email / Pin Combo, Please try again";
                }
            }, function (error) {
                $scope.error = "Invalid Email / Pin Combo, Please try again";
            });
        }
    }
]);

