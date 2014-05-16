'use strict';

var equipmentAdminModule = angular.module('equipment.admin.module', []);

equipmentAdminModule.directive('equipmentForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.equipment.form.htm'
    };
}]);

equipmentAdminModule.directive('equipmentList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.equipment.list.htm'
    };
}]);


equipmentAdminModule.controller('EquipmentController', ['$scope', 'equipmentAdminService', 'configService', 'notifierService', function ($scope, equipmentAdminService, configService, notifierService) {

    function getEquipmentList() {
        equipmentAdminService.getAll().$promise.then(function (results) {
            $scope.equipment = results;
        });
    }

    $scope.categories = configService.lookups.functionExerciseCategories;
    getEquipmentList();

    $scope.editorOptions = configService.kendoEditorOptions;

    $scope.bindSelectedEquipment = function (equipment) {
        $scope.selectedEquipment = equipment;
    }

    $scope.reset = function () {
        $scope.selectedEquipment = null;
    }

    $scope.submit = function () {
        if ($scope.selectedEquipment.id > 0) {
            equipmentAdminService.update($scope.selectedEquipment).$promise.then(function () {
                notifierService.notify('Update Success!');
                getEquipmentList();
            });
        } else {
            equipmentAdminService.save($scope.selectedEquipment).$promise.then(function () {
                notifierService.notify('Created Successfully!');
                getEquipmentList();
            });
        }
    }
}]);


equipmentAdminModule.factory('equipmentAdminService', ['$resource', function ($resource) {

    var adminEquipmentPath = '/admin/equipment/';


    return {
        get: function (id) {
            var resource = $resource(adminEquipmentPath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function () {
            return $resource(adminEquipmentPath).query();
        },
        save: function (equipment) {
            return $resource(adminEquipmentPath).save(equipment);
        },
        update: function (equipment) {
            var resource = $resource(adminEquipmentPath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: equipment.id }, equipment);
        }
    }



}]);