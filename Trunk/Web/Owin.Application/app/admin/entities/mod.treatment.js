'use strict';

var treatmentAdminModule = angular.module('treatment.admin.module', []);

treatmentAdminModule.directive('treatmentForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.treatment.form.htm'
    };
}]);

treatmentAdminModule.directive('treatmentList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.treatment.list.htm'
    };
}]);


treatmentAdminModule.controller('TreatmentController', ['$scope', 'treatmentAdminService', 'configService', 'notifierService', function ($scope, treatmentAdminService, configService, notifierService) {

    function getTreatmentList() {
        treatmentAdminService.getAll().$promise.then(function (results) {
            $scope.treatments = results;
        });
    }

    $scope.categories = configService.lookups.treatmentCategories;
    $scope.providers = configService.lookups.treatmentProviders;
    getTreatmentList();

    $scope.editorOptions = configService.kendoEditorOptions;

    $scope.bindSelectedTreatment = function (treatment) {
        $scope.selectedTreatment = treatment;
    }

    $scope.reset = function () {
        $scope.selectedTreatment = null;
    }

    $scope.submit = function () {
        if ($scope.selectedTreatment.id > 0) {
            treatmentAdminService.update($scope.selectedTreatment).$promise.then(function () {
                notifierService.notify('Update Success!');
                getTreatmentList();
            });
        } else {
            treatmentAdminService.save($scope.selectedTreatment).$promise.then(function () {
                notifierService.notify('Created Successfully!');
                getTreatmentList();
            });
        }
    }
}]);


treatmentAdminModule.factory('treatmentAdminService', ['$resource', function ($resource) {

    var adminTreatmentPath = '/data/admin/treatments/';


    return {
        get: function (id) {
            var resource = $resource(adminTreatmentPath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function () {
            return $resource(adminTreatmentPath).query();
        },
        save: function (treatment) {
            return $resource(adminTreatmentPath).save(treatment);
        },
        update: function (treatment) {
            var resource = $resource(adminTreatmentPath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: treatment.id }, treatment);
        }
    }



}]);