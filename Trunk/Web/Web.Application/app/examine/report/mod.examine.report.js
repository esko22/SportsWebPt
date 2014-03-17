angular.module('examine.report', [])
    .controller('ExamineReportController', ['$scope', '$state', 'configService', function ($scope, $state, configService) {

        if ($scope.report.potentialInjuries.length === 0) {
            $state.go('public.examine.skeleton');
            return;
        }

        $scope.probabableInjuries = [];
        $scope.moderateInjuries = [];
        $scope.remoteInjuries = [];

        angular.forEach($scope.report.potentialInjuries, function (injury) {
            if (injury.likelyHood >= configService.likelyHoodThresholds.high)
                $scope.probabableInjuries.push(injury);
            else if (injury.likelyHood >= configService.likelyHoodThresholds.medium)
                $scope.moderateInjuries.push(injury);
            else
                $scope.remoteInjuries.push(injury);
        });

        $scope.injury = $scope.report.potentialInjuries[0];
        $scope.animationTag = null;
        $scope.oneAtATime = true;

        $scope.selectInjury = function (selectedInjury) {
            $("#injury-report-nav li.active").removeClass("active");
            $scope.injury = selectedInjury;
            $scope.plans = selectedInjury.plans;
            $scope.animationTag = selectedInjury.animationTag;
        };

        //TODO: HACKish
        setTimeout(function() {
            $('#injury-report-nav ul li:first').addClass("active");
        }, 500);

    }])
    .directive("examineReportInjuryListing", [function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/examine/report/tmpl.examine.report.injury.listing.htm'
        };
    }])
    .directive("examineReportInjuryNav", [function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/examine/report/tmpl.examine.report.injury.nav.htm'
        };
    }]);
