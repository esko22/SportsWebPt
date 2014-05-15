'use strict';

var bodyRegionModule = angular.module('body.region.module', []);

bodyRegionModule.directive('bodyRegionForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/content/tmpl.body.region.form.htm'
    };
}]);

bodyRegionModule.directive('bodyRegionList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/content/tmpl.body.region.list.htm'
    };
}]);


bodyRegionModule.controller('BodyRegionController', ['$scope', 'bodyRegionService', 'configService', 'notifierService', function ($scope, bodyRegionService, configService, notifierService) {

    function getBodyRegionList() {
        bodyRegionService.getAll().$promise.then(function (results) {
            $scope.bodyRegions = results;
        });
    }

    $scope.categories = configService.lookups.regionCategories;
    getBodyRegionList();

    $scope.bindSelectedBodyRegion = function(bodyRegion) {
        $scope.selectedBodyRegion = bodyRegion;
    }

    $scope.resetBodyRegion = function() {
        $scope.selectedBodyRegion = null;
    }

    $scope.submit = function() {
        if ($scope.selectedBodyRegion.id > 0) {
            bodyRegionService.update($scope.selectedBodyRegion).$promise.then(function () {
                notifierService.notify('Update Successfully!');
                getBodyRegionList();
            });
        } else {
            bodyRegionService.save($scope.selectedBodyRegion).$promise.then(function() {
                notifierService.notify('Creeate Successfully!');
                getBodyRegionList();
            });
        }
    }
}]);


bodyRegionModule.factory('bodyRegionService', ['$resource', function($resource) {
    
    var adminBodyRegion = '/admin/bodyregions';


return {
    get: function (id) {
        var resource = $resource(adminBodyRegion + '/:id');
        return resource.get({ id: id });
    },
    getAll: function () {
        return $resource(adminBodyRegion).query();
    },
    save: function (bodyRegion) {
        return $resource(adminBodyRegion).save(bodyRegion);
    },
    update: function (bodyRegion) {
        var resource = $resource(adminBodyRegion + '/:id', null, {
            'update': { method: 'PUT' }
        });
        return resource.update({ id: bodyRegion.id }, bodyRegion);
    }
}



}]);