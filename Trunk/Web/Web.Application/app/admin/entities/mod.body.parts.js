'use strict';

var bodyPartModule = angular.module('body.part.admin.module', []);

bodyPartModule.directive('bodyPartForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.body.part.form.htm'
    };
}]);

bodyPartModule.directive('bodyPartList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.body.part.list.htm'
    };
}]);


bodyPartModule.controller('BodyPartController', ['$scope', 'bodyPartAdminService', 'configService', 'notifierService', function ($scope, bodyPartAdminService, configService, notifierService) {

    function getBodyPartList() {
        bodyPartAdminService.getAll().$promise.then(function (results) {
            $scope.bodyParts = results;
        });
    }

    bodyPartAdminService.getSkeletonAreas().$promise.then(function(areas) {
        $scope.skeletonAreas = areas;
    });

    getBodyPartList();

    $scope.bindSelectedBodyPart = function (bodyPart) {
        $scope.selectedBodyPart = bodyPart;

        var primaryAreas = [];
        var secondaryAreas = [];
        _.each(bodyPart.primaryAreas, function(area) {
            primaryAreas.push(_.findWhere($scope.skeletonAreas, { id: area.id }));
        });
        _.each(bodyPart.secondaryAreas, function (area) {
            secondaryAreas.push(_.findWhere($scope.skeletonAreas, { id: area.id }));
        });

        $scope.selectedBodyPart.primaryAreas = primaryAreas;
        $scope.selectedBodyPart.secondaryAreas = secondaryAreas;

    }

    $scope.reset = function () {
        $scope.selectedBodyPart = null;
    }

    $scope.submit = function () {
        if ($scope.selectedBodyPart.id > 0) {
            bodyPartAdminService.update($scope.selectedBodyPart).$promise.then(function () {
                notifierService.notify('Update Success!');
                getBodyPartList();
            });
        } else {
            bodyPartAdminService.save($scope.selectedBodyPart).$promise.then(function () {
                notifierService.notify('Created Successfully!');
                getBodyPartList();
            });
        }
    }
}]);


bodyPartModule.factory('bodyPartAdminService', ['$resource', function ($resource) {

    var adminBodyPartPath = '/data/admin/bodyparts';
    var skeletonAreas = '/data/admin/skeletonareas';

    return {
        get: function (id) {
            var resource = $resource(adminBodyPartPath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function () {
            return $resource(adminBodyPartPath).query();
        },
        getSkeletonAreas: function () {
            return $resource(skeletonAreas).query();
        },
        save: function (bodyPart) {
            return $resource(adminBodyPartPath).save(bodyPart);
        },
        update: function (bodyPart) {
            var resource = $resource(adminBodyPartPath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: bodyPart.id }, bodyPart);
        }
    }



}]);