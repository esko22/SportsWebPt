'use strict';

var causeModule = angular.module('cause.admin.module', []);

causeModule.directive('causeForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.cause.form.htm'
    };
}]);

causeModule.directive('causeList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.cause.list.htm'
    };
}]);


causeModule.controller('CauseController', ['$scope', 'causeAdminService', 'configService', 'notifierService', function ($scope, causeAdminService, configService, notifierService) {

    function getCauseList() {
        causeAdminService.getAll().$promise.then(function (results) {
            $scope.causes = results;
        });
    }

    $scope.filters = configService.causeFilters;
    $scope.categories = configService.lookups.causeCategories;

    getCauseList();

    $scope.bindSelectedCause = function(cause) {
        $scope.selectedCause = cause;
    }

    $scope.reset = function() {
        $scope.selectedCause = null;
    }

    $scope.submit = function() {
        if ($scope.selectedCause.id > 0) {
            causeAdminService.update($scope.selectedCause).$promise.then(function () {
                notifierService.notify('Update Success!');
                getCauseList();
            });
        } else {
            causeAdminService.save($scope.selectedCause).$promise.then(function () {
                notifierService.notify('Created Successfully!');
                getCauseList();
            });
        }
    }
}]);


causeModule.factory('causeAdminService', ['$resource', function ($resource) {
    
    var adminCausePath = '/data/admin/causes';


return {
    get: function (id) {
        var resource = $resource(adminCausePath + '/:id');
        return resource.get({ id: id });
    },
    getAll: function () {
        return $resource(adminCausePath).query();
    },
    save: function (cause) {
        return $resource(adminCausePath).save(cause);
    },
    update: function (cause) {
        var resource = $resource(adminCausePath + '/:id', null, {
            'update': { method: 'PUT' }
        });
        return resource.update({ id: cause.id }, cause);
    }
}



}]);