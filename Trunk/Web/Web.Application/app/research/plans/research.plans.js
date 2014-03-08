'use strict';

angular.module('research.plans', [])
    .controller('PlanController', ['$scope', 'configService', 'plans', function ($scope, configService, plans) {

        $scope.categories = configService.planCategories;
        $scope.bodyRegions = configService.bodyRegions;
        $scope.plans = plans;
        $scope.hasPlans = function () { return plans.length > 0; };

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