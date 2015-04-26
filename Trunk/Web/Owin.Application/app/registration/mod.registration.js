'use strict';

var registrationModule = angular.module('registration.module', []);


registrationModule.controller('RegistrationPatientController', [
    '$scope', 'registrationService', '$stateParams', 'authenticationService', '$state', 'notifierService','clinicService',
    function ($scope, registrationService, $stateParams, authenticationService, $state, notifierService, clinicService) {

        $scope.isAuthenticated = authenticationService.isAuthenticated();
        $scope.registrationDetails = {};
        $scope.registrationDetails.pin = $stateParams.pin;
        $scope.registrationDetails.emailAddress = $stateParams.email;

        clinicService.get($stateParams.clinicId).$promise.then(function(clinic) {
            $scope.clinic = clinic;
        });

        $scope.startRegistration = function() {
            authenticationService.signIn('/register/' + $stateParams.clinicId + '/patient?pin=' + encodeURIComponent($stateParams.pin) + '&email=' + encodeURIComponent($stateParams.email));
        };

        $scope.validate = function() {
            $scope.isValidated = false;
            registrationService.validatePatient($scope.registrationDetails.emailAddress, $scope.registrationDetails.pin).$promise.then(function (clinicPatient) {
                if (clinicPatient.id > 0) {
                    notifierService.notify('You have been registered successfully!');
                    $state.go('patient.dashboard');
                } else {
                    $scope.error = "Email Given Not Used With Initial Registration, Please try again";
                }
            },function(error) {
                $scope.error = "Email Given Not Used With Initial Registration, Please try again";
            });
        }
    }
]);



registrationModule.controller('RegistrationTherapistController', [
    '$scope', 'registrationService', '$stateParams', 'authenticationService', '$state', 'notifierService','clinicService',
    function ($scope, registrationService, $stateParams, authenticationService, $state, notifierService, clinicService) {

        $scope.isAuthenticated = authenticationService.isAuthenticated();
        $scope.registrationDetails = {};
        $scope.registrationDetails.pin = $stateParams.pin;
        $scope.registrationDetails.emailAddress = $stateParams.email;
        clinicService.get($stateParams.clinicId).$promise.then(function (clinic) {
            $scope.clinic = clinic;
        });

        $scope.startRegistration = function () {
            authenticationService.signIn('/register/' + $stateParams.clinicId + '/therapist?pin=' + encodeURIComponent($stateParams.pin) + '&email=' + encodeURIComponent($stateParams.email));
        };

        $scope.validate = function () {
            $scope.isValidated = false;
            registrationService.validateTherapist($scope.registrationDetails.emailAddress, $scope.registrationDetails.pin).$promise.then(function (clinicTherapist) {
                if (clinicTherapist.id > 0) {
                    notifierService.notify('You have been registered successfully!');
                    authenticationService.signIn('/therapist');
                } else {
                    $scope.error = "Email Given Not Used With Initial Registration, Please try again";
                }
            }, function (error) {
                $scope.error = "Email Given Not Used With Initial Registration, Please try again";
            });
        }
    }
]);

