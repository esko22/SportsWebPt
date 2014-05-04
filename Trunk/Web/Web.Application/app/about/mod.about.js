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


