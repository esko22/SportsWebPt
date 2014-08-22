'use strict';

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
        controller: 'ClniicPatientListController'
    };
}]);

clinicModule.controller('ClniicPatientListController', [
    '$scope', 'clinicService',
    function ($scope, clinicService) {

        clinicService.getManagedClinics($scope.currentUser.id).$promise.then(function (clinics) {
            $scope.clinics = clinics;
        });
    }
]);


clinicModule.directive('clinicPatientList', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/clinic/tmpl.clinic.therapist.list.htm',
        controller: 'ClniicTherapistListController'
    };
}]);

clinicModule.controller('ClniicTherapistListController', [
    '$scope', 'clinicService',
    function ($scope, clinicService) {

        clinicService.getManagedClinics($scope.currentUser.id).$promise.then(function (clinics) {
            $scope.clinics = clinics;
        });
    }
]);
