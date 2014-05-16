'use strict';

var bodyRegionModule = angular.module('body.region.admin.module', []);

bodyRegionModule.directive('bodyRegionForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.body.region.form.htm'
    };
}]);

bodyRegionModule.directive('bodyRegionList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.body.region.list.htm'
    };
}]);


bodyRegionModule.controller('BodyRegionController', ['$scope', 'bodyRegionAdminService', 'configService', 'notifierService', function ($scope, bodyRegionAdminService, configService, notifierService) {

    function getBodyRegionList() {
        bodyRegionAdminService.getAll().$promise.then(function (results) {
            $scope.bodyRegions = results;
        });
    }

    $scope.categories = configService.lookups.regionCategories;
    getBodyRegionList();

    $scope.bindSelectedBodyRegion = function(bodyRegion) {
        $scope.selectedBodyRegion = bodyRegion;
    }

    $scope.reset = function() {
        $scope.selectedBodyRegion = null;
    }

    $scope.submit = function() {
        if ($scope.selectedBodyRegion.id > 0) {
            bodyRegionAdminService.update($scope.selectedBodyRegion).$promise.then(function () {
                notifierService.notify('Update Success!');
                getBodyRegionList();
            });
        } else {
            bodyRegionAdminService.save($scope.selectedBodyRegion).$promise.then(function () {
                notifierService.notify('Created Successfully!');
                getBodyRegionList();
            });
        }
    }
}]);


bodyRegionModule.factory('bodyRegionAdminService', ['$resource', function($resource) {
    
    var adminBodyRegionPath = '/admin/bodyregions';


return {
    get: function (id) {
        var resource = $resource(adminBodyRegionPath + '/:id');
        return resource.get({ id: id });
    },
    getAll: function () {
        return $resource(adminBodyRegionPath).query();
    },
    save: function (bodyRegion) {
        return $resource(adminBodyRegionPath).save(bodyRegion);
    },
    update: function (bodyRegion) {
        var resource = $resource(adminBodyRegionPath + '/:id', null, {
            'update': { method: 'PUT' }
        });
        return resource.update({ id: bodyRegion.id }, bodyRegion);
    }
}



}]);