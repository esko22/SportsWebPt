'use strict';

angular.module('examine', ['examine.skeleton', 'examine.symptoms', 'examine.report'])
    .controller('ExamineController', function ($scope) {
        $scope.selectedAreas = [];
        $scope.report = {};
    $scope.report.potentialInjuries = [];
});
