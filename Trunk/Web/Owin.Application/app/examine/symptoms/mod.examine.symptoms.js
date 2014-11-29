angular.module('examine.symptoms', [])
    .controller('ExamineSymptomsController', [
        '$scope', 'configService', '$state',
        function($scope, configService, $state) {

            if ($scope.selectedAreas.length === 0) {
                $state.go($scope.symptomBackState);
                return;
            }
            $scope.currentStep.order = 2;
            $scope.selectedArea = $scope.selectedAreas[0];
            $scope.potentialSymptoms = [];

            $scope.selectArea = function(selectArea) {
                $scope.selectedArea = selectArea;
            };

        }
    ])
    .controller('ExamineSymptomListingController', [
        '$scope', 'examineSymptomsService', function($scope, examineSymptomsService) {

            $scope.bodyParts = [];

            $scope.$watch('selectedArea', function(selectedArea) {
                if (selectedArea) {
                    $scope.bodyParts.length = 0;
                    $scope.selectedBodyPart = null;

                    angular.forEach(selectedArea.bodyParts, function(bodyPart) {
                        if (bodyPart.potentialSymptoms.length === 0) {
                            //TODO: ANGC HACK if really has no values... fetch keeps happing
                            examineSymptomsService.getSymptoms(bodyPart.bodyPartMatrixId).$promise.then(function(results) {
                                bodyPart.potentialSymptoms = results;
                                if (bodyPart.potentialSymptoms.length > 0) {
                                    angular.forEach(bodyPart.potentialSymptoms, function(symptom) {
                                        symptom.givenResponse = [];
                                    });
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
                if (bodyPart)
                    $scope.selectedBodyPart = bodyPart;
            }

        }
    ])
    .directive("examineSymptomsControl", [
        function() {
            return {
                restrict: 'E',
                replace: true,
                controller: 'ExamineSymptomsController',
                templateUrl: '/app/examine/symptoms/tmpl.examine.symptoms.htm'
            };
        }
    ])
    .directive("examineSymptomListing", [
        function() {
            return {
                restrict: 'E',
                replace: true,
                controller: 'ExamineSymptomListingController',
                templateUrl: '/app/examine/symptoms/tmpl.symptom.listing.htm'
            };
        }
    ]);
