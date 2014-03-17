'use strict';

angular.module('examine', ['examine.skeleton', 'examine.symptoms', 'examine.report'])
    .controller('ExamineController',[ '$scope', function ($scope) {
        $scope.selectedAreas = [];
        $scope.report = {};
        $scope.report.potentialInjuries = [];
}]);
