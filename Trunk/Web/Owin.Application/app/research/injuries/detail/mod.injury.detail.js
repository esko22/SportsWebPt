'use strict';

angular.module('research.injury.detail', [])
    .controller('InjuryViewController', [
        '$scope', 'navBarService', 'configService', 'injuryDetailService', '$stateParams', function($scope, navBarService, configService, injuryDetailService, $stateParams) {

            $scope.isLoading = true;
            $scope.animationTag = null;

            //TODO: move this out of controller
            var injuryDump = $('#selected-injury').val();

            if (injuryDump) {
                onInjuryLoadComplete(JSON.parse(injuryDump));
            } else {
                injuryDetailService.getInjury($stateParams.injuryId).$promise.then(function(injury) {
                    onInjuryLoadComplete(injury);
                });
            }

            function onInjuryLoadComplete(injury) {
                $scope.injury = injury;
                navBarService.entityId = injury.id;
                $scope.isFavorite = navBarService.isFavorite();
                $scope.isLoading = false;
            };

            navBarService.entityType = 'injury';
            navBarService.returnUri = configService.returnUris.researchInjuries;

            $scope.navBarService = navBarService;
        }
    ])
    .controller('InjuryDescriptionController', [
        '$scope', function($scope) {
            $scope.hasAnimationTag = function() {
                return $scope.animationTag !== null;
            };

            $scope.treatmentSortFunc = function(treatment) {
                switch (treatment.provider.toLowerCase()) {
                case "self":
                    return 1;
                case "physicaltherapist":
                    return 2;
                case "messagetherapist":
                    return 3;
                case "physican":
                    return 4;
                case "surgeon":
                    return 5;
                case "chiropracter":
                    return 6;
                default:
                    return 99;
                }
            };

            $scope.$watch('injury', function(injury) {
                if (injury) {
                    if ($scope.injury.animationTag) {
                        $scope.animationTag = $scope.injury.animationTag;
                    }
                }
            });
        }
    ])
    .controller('InjuryPlanDetailController', [
        '$scope', function($scope) {
        }
    ])
    .controller('InjuryPlanListingController', [
        '$scope', function($scope) {
            $scope.$watch('injury', function(injury) {
                if (injury) {
                    $scope.oneAtATime = true;
                    $scope.plans = $scope.injury.plans;
                }
            });

        $scope.status = {
            isFirstOpen: false
        };

        $scope.planSortFunc = function (plan) {
            if (!plan.categories)
                return 99;

            switch (plan.categories[0].toLowerCase()) {
                case "rehabilitation":
                    return 1;
                case "streching":
                    return 2;
                case "preventative":
                    return 3;
                case "strengthening":
                    return 4;
                case "spinal stabilization":
                    return 5;
                case "self massage":
                    return 6;
                case "range of motion":
                    return 7;
                case "balance":
                    return 6;
                default:
                    return 99;
            }
        };

    }])
    .directive("injuryDescription", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryDescriptionController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.description.htm'
        };
    }])
    .directive("injuryPlanListing", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryPlanListingController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.plan.listing.htm'
        };
    }])
    .directive("injuryPlanDetail", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'InjuryPlanDetailController',
            templateUrl: '/app/research/injuries/detail/tmpl.injury.plan.detail.htm'
        };
    }])
    .factory('injuryDetailService',['$resource', 'configService', function ($resource, configService) {

        var resource = $resource(configService.apiUris.injuryDetail + ':id', { id: '@id' });

        var getInjury = function (injuryId) {
            return resource.get({ id: injuryId });
        };

        return {
            getInjury: getInjury
        };
    }]);

