angular.module('examine.symptoms', [])
    .controller('ExamineSymptomsController', ['$scope', 'examineSymptomsService', 'configService', '$state', '$modal', '$q', '$timeout', function ($scope, examineSymptomsService, configService, $state, $modal, $q, $timeout) {

        if ($scope.selectedAreas.length === 0) {
            $state.go('public.examine.skeleton');
            return;
        }

        $scope.selectedArea = $scope.selectedAreas[0];
        $scope.potentialSymptoms = [];

        $scope.selectArea = function(selectArea) {
            $scope.selectedArea = selectArea;
        };

        var showModal = function() {
            return $modal.open({
                templateUrl: '/app/examine/symptoms/tmpl.report.progress.modal.htm',
            });
        };

        $scope.submitReport = function() {
        var modal = showModal();

            examineSymptomsService.submitReport($scope.selectedArea.bodyParts).then(function(response) {

                $q.all([
                    examineSymptomsService.getReport(response.data).$promise,
                    $timeout(function() {}, 5000)
                    ])
                    .then(function(results) {
                        $scope.report.potentialInjuries = results[0].potentialInjuries;
                        $state.go('public.examine.report');
                        modal.dismiss('complete');
                    });
            });
        };
    }])
    .controller('ExamineSymptomListingController', ['$scope', 'examineSymptomsService', function ($scope, examineSymptomsService) {

        $scope.$watch('selectedArea', function(selectedArea) {
            if (selectedArea) {
                angular.forEach(selectedArea.bodyParts, function(bodyPart) {
                    if (bodyPart.potentialSymptoms.length === 0) {
                        //TODO: ANGC HACK if really has no values... fetch keeps happing
                        bodyPart.potentialSymptoms = examineSymptomsService.getSymptoms(bodyPart.bodyPartMatrixId);
                    }
                });
                $scope.bodyParts = selectedArea.bodyParts;
            }
        });
    }])
    .directive("examineSymptomListing", [function () {
        return {
            restrict: 'E',
            replace: true,
            controller: 'ExamineSymptomListingController',
            templateUrl: '/app/examine/symptoms/tmpl.symptom.listing.htm'
        };
    }])
    .factory('examineSymptomsService',['$resource', 'configService', '$http', function ($resource, configService, $http) {

    var submitReport = function(bodyParts) {
        var symptomsForReport = [];

        angular.forEach(bodyParts, function(bodyPart) {
            if (bodyPart.potentialSymptoms.length > 0) {
                symptomsForReport.push.apply(symptomsForReport, bodyPart.potentialSymptoms);
            }
        });

        var diffDiagSubmission = {
            "submittedFor": 0,
            "symptomDetails": symptomsForReport
        };

        return $http.post("/examine/diffdiag", diffDiagSubmission);
    };

    var getReport = function(reportId) {
        var resource = $resource(configService.apiUris.diagnosisReport + '/:id', { id: '@id' });
        return resource.get({ id: reportId });
    };

    var getSymptoms = function(bodyPartId) {
        var resource = $resource(configService.apiUris.potentialSymptoms + '/' + bodyPartId);
        return resource.query();
    };


    return {
        submitReport: submitReport,
        getReport: getReport,
        getSymptoms: getSymptoms
    };

}]);
