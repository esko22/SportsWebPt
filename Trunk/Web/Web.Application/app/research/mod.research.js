'use strict';

angular.module('research', ['research.exercises', 'research.plans', 'research.injuries'])
    .controller('ResarchNavBarController', [
        '$scope', '$rootScope', '$http', 'notifierService', 'userManagementService', 'navBarService', function ($scope, $rootScope, $http, notifierService, userManagementService) {

        $scope.addAsFavorite = function() {
            if (!userManagementService.isAuthenticated()) {
                $rootScope.$emit("signup-dialog-prompt");
                return;
            }

            var messageKeys = {
                'entity': $scope.navBarService.entityType,
                'entityId': $scope.navBarService.entityId
            };

            $http.post("/data/users/favorites", messageKeys).
                success(function() {
                    notifierService.notify('Favorite Added Successfully!');
                    $scope.isFavorite = true;
                    userManagementService.refreshUser();
                }).
                error(function() {
                    notifierService.warn('Favorite Not Added. Please Try Again.');
                });
        };

    }])
    .controller('ResearchController', [
        '$scope', function ($scope) {
            
        }])
    .controller('ResearchLocateController', [
        '$scope', '$http', 'configService', function ($scope, $http, configService) {
            $scope.zipcode = null;

            $scope.onSearchSubmit = function () {
                $http.get(configService.apiUris.clinics.replace('{zipcode}', $scope.zipcode)).then(function (result) {
                    $scope.clinics = result.data;
                });
            };

    }])
    .factory('navBarService', ['userManagementService', function (userManagementService) {
        
        var entityType = '';
        var entityId = 0;
        var returnUri = '';

        function isFavorite() {
            var user = userManagementService.getUser();

            if (!user)
                return false;

            switch (this.entityType.toLowerCase()) {
                case 'plan':
                    if (_.findWhere(user.plans, { entityId: this.entityId })) {
                        return true;
                    }
                    return false;
                case 'exercise':
                    if (_.findWhere(user.exercises, { entityId: this.entityId })) {
                        return true;
                    }
                    return false;
                case 'injury':
                    if (_.findWhere(user.injuries, { entityId: this.entityId })) {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        };


        return {
            entityType: entityType,
            entityId: entityId,
            returnUri: returnUri,
            isFavorite: isFavorite
        };
    }])
    .directive('researchNavBar', [function () {
        return {
            restrict: 'E',
            replace: 'true',
            scope: true,
            controller: 'ResarchNavBarController',
            templateUrl: '/app/research/tmpl.research.nav.bar.htm'
        };
}]);


