'use strict';

angular.module('examine', ['examine.skeleton', 'examine.symptoms', 'examine.report'])
    .controller('ExamineController', ['$scope', 'examineSymptomsService','$modal','$state','notifierService','$q','$timeout', function ($scope, examineSymptomsService, $modal, $state,notifierService, $q, $timeout) {
        $scope.selectedAreas = [];
        $scope.report = {};
        $scope.report.potentialInjuries = [];

        $scope.currentStep = {};
        $scope.currentStep.order = 0;

        $scope.skeletonNextState = 'public.examine.symptoms';
        $scope.symptomBackState = 'public.examine.skeleton';




        var showModal = function () {
            return $modal.open({
                templateUrl: '/app/examine/symptoms/tmpl.report.progress.modal.htm',
            });
        };

        $scope.submitReport = function () {
            var modal = showModal();

            var allBodyParts = [];
            angular.forEach($scope.selectedAreas, function (selectedArea) {
                angular.forEach(selectedArea.bodyParts, function(bodyPart) {
                    allBodyParts.push(bodyPart);
                });
            });

            examineSymptomsService.submitReport(allBodyParts).then(function (response) {
                $scope.report.success = false;
                $q.all([
                        examineSymptomsService.getReport(response.data).$promise,
                        $timeout(function () { }, 5000)
                ])
                    .then(function (results) {
                        $scope.report.potentialInjuries = results[0].potentialInjuries;
                        $scope.report.success = true;
                        $state.go('public.examine.report');
                        modal.dismiss('complete');
                    }).catch(function () {
                        modal.dismiss('complete');
                        notifierService.error('Issue getting results, please try again');
                    });
            });
        };
    }]).factory('examineSymptomsService', ['$resource', 'configService', '$http', function ($resource, configService, $http) {

        var submitReport = function (bodyParts, sessionId) {
            var symptomsForReport = [];

            angular.forEach(bodyParts, function (bodyPart) {
                if (bodyPart.potentialSymptoms && bodyPart.potentialSymptoms.length > 0) {
                    symptomsForReport.push.apply(symptomsForReport, bodyPart.potentialSymptoms);
                }
            });

            var diffDiagSubmission = {
                "symptomDetails": symptomsForReport,
                "sessionId" : sessionId
            };

            return $http.post("/data/examine/diffdiag", diffDiagSubmission);
        };

        var getReport = function (reportId) {
            var resource = $resource(configService.apiUris.diagnosisReport + '/:id', { id: '@id' });
            return resource.get({ id: reportId });
        };

        var getSymptoms = function (bodyPartId) {
            var resource = $resource(configService.apiUris.potentialSymptoms + '/' + bodyPartId);
            return resource.query();
        };


        return {
            submitReport: submitReport,
            getReport: getReport,
            getSymptoms: getSymptoms
        };

    }]);
