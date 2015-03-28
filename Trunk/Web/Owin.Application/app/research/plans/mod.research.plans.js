'use strict';

angular.module('research.plans', ['research.plan.detail'])
    .controller('PlanController', ['$scope', 'configService', 'planListService', '$filter', '$location', '$rootScope', function ($scope, configService, planListService, $filter, $location, $rootScope) {

        $rootScope.pageTitle = 'Research - Plan Listing';
        $scope.categories = configService.lookups.planCategories;
        $scope.bodyRegions = configService.bodyRegions;
        $scope.isLoading = true;

        $scope.selectedCategory = "";
        $scope.selectedBodyRegion = "";

        // Paging controls
        $scope.pageSize = 20;
        $scope.currentPage = 1;
        $scope.firstItemPosition = 1;
        $scope.lastItemPosition = $scope.pageSize;
        $scope.onPageChanged = function () {
            $scope.firstItemPosition = (($scope.currentPage - 1) * $scope.pageSize) + 1;
            $scope.lastItemPosition = Math.min($scope.currentPage * $scope.pageSize, $scope.filteredPlans.length);
        };

        $scope.isCategorySelected = function (category) {
            return $scope.selectedCategory === category;
        };

        $scope.isBodyRegionSelected = function (bodyRegion) {
            return $scope.selectedBodyRegion === bodyRegion;
        };

        $scope.setCategory = function (category) {
            $scope.selectedCategory = category;
            applyFilter();
        };

        $scope.setBodyRegion = function (bodyRegion) {
            $scope.selectedBodyRegion = bodyRegion;
            applyFilter();
        };

        planListService.planList.$promise.then(function (plans) {
            $scope.isLoading = false;
            $scope.plans = plans;
            $scope.filteredPlans = plans;
        });

        function applyFilter() {
            $scope.filteredPlans = $filter('filter')($scope.plans, { categories: $scope.selectedCategory, bodyRegions: $scope.selectedBodyRegion});
            $scope.currentPage = 1;
            $scope.onPageChanged();
            $location.hash('research-plan-panel');
            //$anchorScroll();
        }

    }])
    .controller('BriefPlanController', [
        '$scope', function ($scope) {
            $scope.oneAtATime = true;
        }
    ])
    .factory('planListService', ['$resource', 'configService', function ($resource, configService) {

        return {
            planList: $resource(configService.apiUris.briefPlans).query()
        };

    }])
    .directive("briefPlanAccordian",[ function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/research/plans/prtl.brief.plan.accord.htm',
            controller: "BriefPlanController"
        };
    }]);