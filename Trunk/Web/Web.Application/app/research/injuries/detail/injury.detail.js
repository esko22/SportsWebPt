'use strict';

angular.module('research.injury.detail', [])
    .controller('InjuryViewController', [
        '$scope', 'injury','navBarService', 'configService', function($scope, injury, navBarService, configService) {
            $scope.injury = injury;

            navBarService.entityType = 'injuries';
            navBarService.entityId = injury.id;
            navBarService.returnUri = configService.returnUris.researchInjuries;

            $scope.navBarService = navBarService;
        }
    ])
    .controller('InjuryDescriptionController', function ($scope) {
        $scope.animationTag = $scope.injury.animationTag;
    })
    .controller('InjuryDetailController', function ($scope) {


    })
    .directive("injuryDescription", function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryDescriptionController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.description.htm',
            link: function(scope) {
                openthis = "C_d4202781";
                vm_open();
            }
        };
    })
    //.directive("exerciseDetail", function () {
    //    return {
    //        restrict: 'E',
    //        replace: true,
    //        controller: 'ExerciseDetailController',
    //        templateUrl: '/app/research/exercises/detail/tmpl.exercise.detail.htm'
    //    };
    //})
    .factory('injuryDetailService', function ($resource, $q, configService) {
        var resource = $resource(configService.apiUris.injuryDetail + ':id', { id: '@id' });
        return {
            getInjury: function(injuryId) {
                var deferred = $q.defer();
                resource.get({ id: injuryId },
                    function(injury) {
                        deferred.resolve(injury);
                    },
                    function (response) {
                        deferred.reject(response);
                    });

                return deferred.promise;
            }
        };
    });

