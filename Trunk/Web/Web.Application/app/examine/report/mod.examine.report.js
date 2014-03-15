angular.module('examine.report', [])
    .controller('ExamineReportController', function ($scope, $state) {


        $scope.potentialInjuries = $scope.report.potentialInjuries;

        if ($scope.potentialInjuries.length === 0) {
            $state.go('public.examine.skeleton');
            return;
        }

        $scope.injury = $scope.report.potentialInjuries[0];
        $scope.animationTag = null;
        $scope.oneAtATime = true;

        $scope.selectInjury = function(selectedInjury) {
            $scope.injury = selectedInjury;
            $scope.plans = selectedInjury.plans;
            $scope.animationTag = selectedInjury.animationTag;
        };
});
