'use strict';

angular.module('research.plans', [])
    .controller('PlanController', ['$scope', 'configService', 'plans', function ($scope, configService, plans) {

        $scope.categories = configService.planCategories;
        $scope.bodyRegions = configService.bodyRegions;
        $scope.plans = plans;

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
    }
    ])
    .controller('BriefPlanController', [
        '$scope', function ($scope) {
            $scope.oneAtATime = true;
        }
    ])
    .directive("briefPlanAccordian", function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/research/plans/prtl.brief.plan.accord.htm',
            controller: "BriefPlanController"
        };
    });