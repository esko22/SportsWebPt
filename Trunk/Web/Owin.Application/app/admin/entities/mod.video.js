'use strict';

var videoAdminModule = angular.module('video.admin.module', []);

videoAdminModule.directive('videoForm', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.video.form.htm'
    };
}]);

videoAdminModule.directive('videoList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/entities/tmpl.video.list.htm'
    };
}]);


videoAdminModule.controller('VideoController', ['$scope', 'videoAdminService', 'configService', 'notifierService', function ($scope, videoAdminService, configService, notifierService) {

    function getVideoList() {
        videoAdminService.getAll().$promise.then(function (results) {
            $scope.videos = results;
        });
    }

    $scope.categories = configService.lookups.functionExerciseCategories;
    getVideoList();

    $scope.editorOptions = configService.kendoEditorOptions;

    $scope.bindSelectedVideo = function (video) {
        $scope.selectedVideo = video;
    }

    $scope.reset = function () {
        $scope.selectedVideo = null;
    }

    $scope.submit = function () {
        if ($scope.selectedVideo.id > 0) {
            videoAdminService.update($scope.selectedVideo).$promise.then(function () {
                notifierService.notify('Update Success!');
                getVideoList();
            });
        } else {
            videoAdminService.save($scope.selectedVideo).$promise.then(function () {
                notifierService.notify('Created Successfully!');
                getVideoList();
            });
        }
    }
}]);


videoAdminModule.factory('videoAdminService', ['$resource', function ($resource) {

    var adminVideoPath = '/data/admin/videos/';


    return {
        get: function (id) {
            var resource = $resource(adminVideoPath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function () {
            return $resource(adminVideoPath).query();
        },
        save: function (video) {
            return $resource(adminVideoPath).save(video);
        },
        update: function (video) {
            var resource = $resource(adminVideoPath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: video.id }, video);
        }
    }



}]);