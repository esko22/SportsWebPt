angular.module('examine.symptoms', [])
    .controller('ExamineSymptomsController', ['$scope', 'examineSymptomsService', 'configService', '$state', '$modal', '$q', '$timeout', 'notifierService',
        function ($scope, examineSymptomsService, configService, $state, $modal, $q, $timeout, notifierService) {

        if ($scope.selectedAreas.length === 0) {
            $state.go('public.examine.skeleton');
            return;
        }
        $scope.currentStep.order = 2;
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
                $scope.report.success = false;
                $q.all([
                        examineSymptomsService.getReport(response.data).$promise,
                        $timeout(function() {}, 5000)
                    ])
                    .then(function(results) {
                        $scope.report.potentialInjuries = results[0].potentialInjuries;
                        $scope.report.success = true;
                        $state.go('public.examine.report');
                        modal.dismiss('complete');
                    }).catch(function() {
                        modal.dismiss('complete');
                    notifierService.error('Issue getting results, please try again');
                });
            });
        };
    }])
    .controller('ExamineSymptomListingController', ['$scope', 'examineSymptomsService', function ($scope, examineSymptomsService) {

        $scope.bodyParts = [];

        $scope.$watch('selectedArea', function(selectedArea) {
            if (selectedArea) {
                $scope.bodyParts.length = 0;
                $scope.selectedBodyPart = null;

                angular.forEach(selectedArea.bodyParts, function (bodyPart) {
                    if (bodyPart.potentialSymptoms.length === 0) {
                        //TODO: ANGC HACK if really has no values... fetch keeps happing
                        examineSymptomsService.getSymptoms(bodyPart.bodyPartMatrixId).$promise.then(function(results) {
                            bodyPart.potentialSymptoms = results;
                            if (bodyPart.potentialSymptoms.length > 0) {
                                $scope.bodyParts.push(bodyPart);
                                $scope.selectedBodyPart = $scope.bodyParts[0];
                            }
                        });
                    } else {
                        $scope.bodyParts.push(bodyPart);
                        $scope.selectedBodyPart = $scope.bodyParts[0];
                    }
                });
            }
        });

        $scope.onBodyPartSelected = function(bodyPart) {
            if(bodyPart)
                $scope.selectedBodyPart = bodyPart;
        }

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

        return $http.post("/data/examine/diffdiag", diffDiagSubmission);
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
