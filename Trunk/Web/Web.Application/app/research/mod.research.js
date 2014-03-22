﻿'use strict';

angular.module('research', ['research.exercises', 'research.plans', 'research.injuries'])
    .controller('ResarchNavBarController', [
        '$scope', function ($scope) {

            $scope.entityType = $scope.navBarService.entityType;
            $scope.entityId = $scope.navBarService.entityId;
            $scope.returnUri = $scope.navBarService.returnUri;

            $scope.addAsFavorite = function() {
                alert($scope.entityType + $scope.entityId);
                //favHelper.addEntityToFavorites(this.entityType(), this.entityId);
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
        
        var entityType = 'dsfdsf';
        var entityId = 0;
        var returnUri = 'testklk';

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


