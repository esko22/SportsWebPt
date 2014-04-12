'use strict';

angular.module('research.injuries', ['research.injury.detail'])
    .controller('InjuryController', [
        '$scope', 'configService', 'injuryListService', '$filter', '$location', '$anchorScroll', function ($scope, configService, injuryListService, $filter, $location, $anchorScroll) {

            $scope.signFilters = configService.signFilters;
            $scope.bodyRegions = configService.bodyRegions;
            $scope.isLoading = true;

            $scope.selectedSign = "";
            $scope.selectedBodyRegion = "";

            // Paging controls
            $scope.pageSize = 20;
            $scope.currentPage = 1;
            $scope.firstItemPosition = 1;
            $scope.lastItemPosition = $scope.pageSize;
            $scope.onPageChanged = function (page) {
                updatePaging(page);
            };

            $scope.isSignSelected = function (sign) {
                return $scope.selectedSign === sign;
            };

            $scope.isBodyRegionSelected = function (bodyRegion) {
                return $scope.selectedBodyRegion === bodyRegion;
            };

            $scope.setSign = function (sign) {
                $scope.selectedSign = sign;
                applyFilter();
            };

            $scope.setBodyRegion = function (bodyRegion) {
                $scope.selectedBodyRegion = bodyRegion;
                applyFilter();
            };

            injuryListService.injuryList.$promise.then(function (injuries) {
                $scope.isLoading = false;
                $scope.injuries = injuries;
                $scope.filteredInjuries = injuries;
            });

            function applyFilter() {
                $scope.filteredInjuries = $filter('filter')($scope.injuries, { signs: $scope.selectedSign, bodyRegions: $scope.selectedBodyRegion });
                updatePaging(1);
                $location.hash('research-injury-panel');
                //$anchorScroll();
            }

            //todo: convert to a service
            function updatePaging(page) {
                $scope.firstItemPosition = ((page - 1) * $scope.pageSize) + 1;
                $scope.lastItemPosition = Math.min(page * $scope.pageSize, $scope.filteredInjuries.length);
            }


        }
    ])
    .controller('BriefInjuryController', [
        '$scope', function($scope) {
            $scope.oneAtATime = true;
        }
    ])
    .factory('injuryListService', ['$resource', 'configService', function ($resource, configService) {

        return {
            injuryList: $resource(configService.apiUris.briefInjuries).query()
        };

    }])
    .directive("briefInjuryAccordian", [function() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/research/injuries/prtl.brief.injury.accord.htm',
            controller: "BriefInjuryController"
        };
    }]);