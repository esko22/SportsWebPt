'use strict';

angular.module('research.plans', ['research.plan.detail'])
    .controller('PlanController', ['$scope', 'configService', 'planListService', function ($scope, configService, planListService) {

        $scope.categories = configService.planCategories;
        $scope.bodyRegions = configService.bodyRegions;
        $scope.isLoading = true;

        $scope.selectedCategory = "";
        $scope.selectedBodyRegion = "";

        $scope.isCategorySelected = function (category) {
            return $scope.selectedCategory === category;
        };

        $scope.isBodyRegionSelected = function (bodyRegion) {
            return $scope.selectedBodyRegion === bodyRegion;
        };

        $scope.setCategory = function (category) {
            $scope.selectedCategory = category;
        };

        $scope.setBodyRegion = function (bodyRegion) {
            $scope.selectedBodyRegion = bodyRegion;
        };

        planListService.planList.$promise.then(function (plans) {
            $scope.isLoading = false;
            $scope.plans = plans;
        });


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