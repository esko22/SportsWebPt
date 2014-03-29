'use strict';

angular.module('research.plans', ['research.plan.detail'])
    .controller('PlanController', ['$scope', 'configService', 'planListService', '$filter', '$location', '$anchorScroll', function ($scope, configService, planListService, $filter, $location, $anchorScroll) {

        $scope.categories = configService.planCategories;
        $scope.bodyRegions = configService.bodyRegions;
        $scope.isLoading = true;

        $scope.selectedCategory = "";
        $scope.selectedBodyRegion = "";

        // Paging controls
        $scope.pageSize = 20;
        $scope.currentPage = 1;
        $scope.firstItemPosition = 1;
        $scope.lastItemPosition = $scope.pageSize;
        $scope.onPageChanged = function (page) {
            updatePaging(page);
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
            updatePaging(1);
            $location.hash('research-plan-panel');
            $anchorScroll();
        }

        //todo: convert to a service
        function updatePaging(page) {
            $scope.firstItemPosition = ((page - 1) * $scope.pageSize) + 1;
            $scope.lastItemPosition = Math.min(page * $scope.pageSize, $scope.filteredPlans.length);
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