angular.module('examine.report', [])
    .controller('ExamineReportController', function ($scope) {


        $scope.potentialInjuries = $scope.report.potentialInjuries;
        $scope.injury = $scope.report.potentialInjuries[0];
        $scope.oneAtATime = true;

    $scope.selectInjury = function(selectedInjury) {
        $scope.injury = selectedInjury;
        $scope.plans = selectedInjury.plans;
    };


});
