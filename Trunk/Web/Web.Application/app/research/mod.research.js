'use strict';

angular.module('research', ['research.exercises', 'research.plans', 'research.injuries'])
    .controller('ResarchNavBarController', [
        '$scope', '$http', 'notifierService', function ($scope, $http, notifierService) {

            $scope.entityType = $scope.navBarService.entityType;
            $scope.entityId = $scope.navBarService.entityId;
            $scope.returnUri = $scope.navBarService.returnUri;

            $scope.addAsFavorite = function () {
                var messageKeys = {
                    'entity': $scope.navBarService.entityType,
                    'entityId': $scope.navBarService.entityId
                };

                $http.post("/users/favorites", messageKeys).
                    success(function() {
                        notifierService.notify('Favorite Added Successfully!');
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
    .factory('navBarService', function () {
        
        var entityType = '';
        var entityId = 0;
        var returnUri = '';

        return {
            entityType: entityType,
            entityId: entityId,
            returnUri: returnUri
        };
    })
    .directive('researchNavBar', function () {
        return {
            restrict: 'E',
            replace: 'true',
            controller: 'ResarchNavBarController',
            templateUrl: '/app/research/tmpl.research.nav.bar.htm'
        };
});


