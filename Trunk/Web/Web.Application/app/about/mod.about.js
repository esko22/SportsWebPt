'use strict';

var aboutModule = angular.module('about.module', []);

aboutModule.controller('AboutController', ['$scope', '$stateParams', function ($scope, $stateParams) {

    $scope.selectedTab = 'us';

    if ($stateParams.aboutType && $stateParams.aboutType !== '') {
        $scope.selectedTab = $stateParams.aboutType;
    }

    $scope.onTabSelect = function(tab) {
        $scope.selectedTab = tab;
    }


}]);

aboutModule.directive('aboutUs', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/about/tmpl.us.htm'
    };
}]);

aboutModule.directive('aboutContact', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/about/tmpl.contact.htm'
    };
}]);

aboutModule.directive('aboutPrivacy', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/about/tmpl.privacy.htm'
    };
}]);

aboutModule.directive('aboutTerms', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/about/tmpl.terms.htm'
    };
}]);

aboutModule.directive('aboutTeam', [function () {
    return {
        restrict: 'EA',
        replace: 'true',
        templateUrl: '/app/about/tmpl.team.htm'
    };
}]);

aboutModule.controller('SiteMapController', ['$scope', 'planListService', 'injuryListService', 'exerciseListService',
    function ($scope, planListService, injuryListService, exerciseListService) {

        planListService.planList.$promise.then(function (plans) {
            $scope.plans = plans;
        });

        exerciseListService.exerciseList.$promise.then(function (exercises) {
            $scope.exercises = exercises;
        });

        injuryListService.injuryList.$promise.then(function (injuries) {
            $scope.injuries = injuries;
        });

    }]);



