angular.module('examine.symptoms', [])
    .controller('ExamineSymptomsController', function($scope, examineSymptomsService, configService, $state, $http) {

        if ($scope.selectedAreas.length === 0) {
            $state.go('public.examine.skeleton');
            return;
        }

        $scope.selectedArea = $scope.selectedAreas[0];
        $scope.potentialSymptoms = [];

        $scope.selectArea = function(selectArea) {
            $scope.selectedArea = selectArea;
        };

        $scope.submitReport = function() {
            var symptomsForReport = [];

            angular.forEach($scope.selectedArea.bodyParts, function(bodyPart) {
                if (bodyPart.potentialSymptoms.length > 0) {
                    symptomsForReport.push.apply(symptomsForReport, bodyPart.potentialSymptoms);
                }
            });


            var diffDiagSubmission = {
                "submittedFor": 0,
                "symptomDetails": symptomsForReport
            };


            $http.post("/examine/diffdiag", diffDiagSubmission).then(function(response) {
                examineSymptomsService.getReport(response.data).then(function(report) {
                    $scope.report.potentialInjuries = report.potentialInjuries;
                    $state.go('public.examine.report');

                });
            });

        };
    })
    .controller('ExamineSymptomListingController', function ($scope, examineSymptomsService) {

        $scope.$watch('selectedArea', function(selectedArea) {
            if (selectedArea) {
                angular.forEach(selectedArea.bodyParts, function(bodyPart) {
                    if (bodyPart.potentialSymptoms.length === 0) {
                        //TODO: ANGC HACK if really has no values... fetch keeps happing
                        bodyPart.potentialSymptoms = examineSymptomsService.getSymptoms(bodyPart.id);
                    }
                });
                $scope.bodyParts = selectedArea.bodyParts;
            }
        });

    })
    .controller('BodyPartSymptomListingController', function ($scope, examineSymptomsService) {

    })
    .factory('examineSymptomsService', function ($resource, $q, configService, notifierService, $http) {
        return {
            getReport: function(reportId) {
                var resource = $resource(configService.apiUris.diagnosisReport + '/:id', { id: '@id' });
                var deferred = $q.defer();
                    resource.get({ id: reportId },
                        function(report) {
                            deferred.resolve(report);
                        },
                        function(response) {
                            deferred.reject(response);
                        });

                    return deferred.promise;
            },            

            getSymptoms: function (bodyPartId) {
                var resource = $resource(configService.apiUris.potentialSymptoms + '/' + bodyPartId);
                return resource.query();
            }
        };
    
});
