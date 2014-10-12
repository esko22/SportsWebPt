'use strict';

var signModule = angular.module('sign.admin.module', []);

signModule.directive('signForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.sign.form.htm'
    };
}]);

signModule.directive('signList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.sign.list.htm'
    };
}]);


signModule.controller('SignController', ['$scope', 'signAdminService', 'configService', 'notifierService', function ($scope, signAdminService, configService, notifierService) {

    function getSignList() {
        signAdminService.getAll().$promise.then(function (results) {
            $scope.signs = results;
        });
    }

    $scope.filters = configService.signFilters;
    $scope.categories = configService.lookups.signCategories;

    getSignList();

    $scope.bindSelectedSign = function (sign) {
        $scope.selectedSign = sign;
    }

    $scope.reset = function() {
        $scope.selectedSign = null;
    }

    $scope.submit = function() {
        if ($scope.selectedSign.id > 0) {
            signAdminService.update($scope.selectedSign).$promise.then(function () {
                notifierService.notify('Update Success!');
                getSignList();
            });
        } else {
            signAdminService.save($scope.selectedSign).$promise.then(function () {
                notifierService.notify('Created Successfully!');
                getSignList();
            });
        }
    }
}]);


signModule.factory('signAdminService', ['$resource', function ($resource) {
    
    var adminSignPath = '/data/admin/signs';


return {
    get: function (id) {
        var resource = $resource(adminSignPath + '/:id');
        return resource.get({ id: id });
    },
    getAll: function () {
        return $resource(adminSignPath).query();
    },
    save: function (sign) {
        return $resource(adminSignPath).save(sign);
    },
    update: function (sign) {
        var resource = $resource(adminSignPath + '/:id', null, {
            'update': { method: 'PUT' }
        });
        return resource.update({ id: sign.id }, sign);
    }
}



}]);