'use strict';

var prognosisAdminModule = angular.module('prognosis.admin.module', []);

prognosisAdminModule.directive('prognosisForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.prognosis.form.htm'
    };
}]);

prognosisAdminModule.directive('prognosisList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.prognosis.list.htm'
    };
}]);


prognosisAdminModule.controller('PrognosisController', ['$scope', 'prognosisAdminService', 'configService', 'notifierService', function ($scope, prognosisAdminService, configService, notifierService) {

    function getPrognosisList() {
        prognosisAdminService.getAll().$promise.then(function (results) {
            $scope.prognoses = results;
        });
    }

    $scope.categories = configService.lookups.prognosisCategories;
    getPrognosisList();

    $scope.editorOptions = configService.kendoEditorOptions;

    $scope.bindSelectedPrognosis = function (prognosis) {
        $scope.selectedPrognosis = prognosis;
    }

    $scope.reset = function () {
        $scope.selectedPrognosis = null;
    }

    $scope.submit = function () {
        if ($scope.selectedPrognosis.id > 0) {
            prognosisAdminService.update($scope.selectedPrognosis).$promise.then(function () {
                notifierService.notify('Update Success!');
                getPrognosisList();
            });
        } else {
            prognosisAdminService.save($scope.selectedPrognosis).$promise.then(function () {
                notifierService.notify('Created Successfully!');
                getPrognosisList();
            });
        }
    }
}]);


prognosisAdminModule.factory('prognosisAdminService', ['$resource', function ($resource) {

    var adminPronosisPath = '/data/admin/prognoses/';


    return {
        get: function (id) {
            var resource = $resource(adminPronosisPath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function () {
            return $resource(adminPronosisPath).query();
        },
        save: function (prognosis) {
            return $resource(adminPronosisPath).save(prognosis);
        },
        update: function (prognosis) {
            var resource = $resource(adminPronosisPath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: prognosis.id }, prognosis);
        }
    }



}]);