'use strict';

var registrationModule = angular.module('registration.module', []);


registrationModule.controller('RegistrationPatientController', [
    '$scope', 'registrationService','$stateParams',
    function ($scope, registrationService, $stateParams) {

        $scope.registrationDetails = {};
        $scope.isValidated = false;
        $scope.actionPath = '/register/';
        $scope.error = $stateParams.errId;
        $scope.registrationId = 0;

        $scope.validate = function() {
            $scope.isValidated = false;
            registrationService.validatePatient($scope.registrationDetails.emailAddress, $scope.registrationDetails.pin).$promise.then(function (clinicPatient) {
                $scope.clinic = clinicPatient.clinic;
                $scope.actionPath = '/register/' + clinicPatient.id + '/patient';
                $scope.registrationId = clinicPatient.id;
                $scope.isConfirmed = clinicPatient.userConfirmed;
                $scope.isValidated = true;
            });
        }
    }
]);

registrationModule.directive('registerPatientDialog', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/shared/auth/tmpl.signup.dialog.htm',
        controller: 'RegisterPatientDialogController'
    };
}]);

registrationModule.controller('RegisterPatientDialogController', [
    '$scope', 'returnUrlService', function ($scope, returnUrlService) {
        $scope.showOAuthProvider = function(provider) {
            return returnUrlService.getOAuthRegUrl(provider, $scope.registrationId, 'patient');
        }

        $scope.signUp = function () {
            return true;
        };

    }
]);


registrationModule.controller('RegistrationTherapistController', [
    '$scope', 'registrationService', '$stateParams',
    function ($scope, registrationService, $stateParams) {

        $scope.registrationDetails = {};
        $scope.isValidated = false;
        $scope.actionPath = '/register/';
        $scope.error = $stateParams.errId;
        $scope.registrationId = 0;

        $scope.validate = function () {
            $scope.isValidated = false;
            registrationService.validateTherapist($scope.registrationDetails.emailAddress, $scope.registrationDetails.pin).$promise.then(function (clinicTherapist) {
                $scope.clinic = clinicTherapist.clinic;
                $scope.actionPath = '/register/' + clinicTherapist.id + '/therapist';
                $scope.registrationId = clinicTherapist.id;
                $scope.isConfirmed = clinicTherapist.userConfirmed;
                $scope.isValidated = true;
            });
        }
    }
]);

registrationModule.directive('registerTherapistDialog', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/shared/auth/tmpl.signup.dialog.htm',
        controller: 'RegisterTherapistDialogController'
    };
}]);

registrationModule.controller('RegisterTherapistDialogController', [
    '$scope', 'returnUrlService', function ($scope, returnUrlService) {
        $scope.showOAuthProvider = function (provider) {
            return returnUrlService.getOAuthRegUrl(provider, $scope.registrationId, 'therapist');
        }

        $scope.signUp = function () {
            return true;
        };

    }
]);
