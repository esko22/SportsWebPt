'use strict';

var injuryAdminModule = angular.module('injury.admin.module', []);

injuryAdminModule.directive('adminInjuryList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/content/tmpl.injury.list.htm'
    };
}]);


injuryAdminModule.controller('InjuryModalController', [
    '$scope', 'injuryAdminService', '$modalInstance', 'selectedInjury', 'notifierService',
    'configService', 'causeAdminService', 'bodyRegionAdminService', 'signAdminService', 'planAdminService', 'treatmentAdminService', 'prognosisAdminService',
    function ($scope, injuryAdminService, $modalInstance, selectedInjury, notifierService, configService, causeAdminService, bodyRegionAdminService, signAdminService, planAdminService, treatmentAdminService, prognosisAdminService) {

        $scope.injury = {};
        if (selectedInjury) {
            $scope.injury = selectedInjury;
        }

        //lookups
        $scope.repetitionRangeValues = configService.lookups.repetitionRangeValues;
        $scope.editorOptions = configService.kendoEditorOptions;

        configService.availableSymptomsProxy.query().$promise.then(function (results) {
            $scope.availableSymptoms = results;
        });

        configService.bodyPartMatrixProxy.query().$promise.then(function (results) {
            $scope.availableBodyPartMatrix = results;
        });



        causeAdminService.getAll().$promise.then(function (results) {
            $scope.availableCauses = results;
            //have to get items from available collection
            if (selectedInjury) {
                var currentCauses = [];
                _.each(selectedInjury.causes, function (cause) {
                    currentCauses.push(_.findWhere($scope.availableCauses, { id: cause.id }));
                });
                selectedInjury.causes = currentCauses;
            }
        });

        bodyRegionAdminService.getAll().$promise.then(function (results) {
            $scope.availableBodyRegions = results;
            if (selectedInjury) {
                var currentBodyRegions = [];
                _.each(selectedInjury.bodyRegions, function (bodyRegion) {
                    currentBodyRegions.push(_.findWhere($scope.availableBodyRegions, { id: bodyRegion.id }));
                });
                selectedInjury.bodyRegions = currentBodyRegions;
            }
        });

        signAdminService.getAll().$promise.then(function (results) {
            $scope.availableSigns = results;
            if (selectedInjury) {
                var currentSigns = [];
                _.each(selectedInjury.signs, function (sign) {
                    currentSigns.push(_.findWhere($scope.availableSigns, { id: sign.id }));
                });
                selectedInjury.signs = currentSigns;
            }
        });

        planAdminService.getAll().$promise.then(function (results) {
            $scope.availablePlans = results;
            if (selectedInjury) {
                var currentPlans = [];
                _.each(selectedInjury.plans, function (plan) {
                    currentPlans.push(_.findWhere($scope.availablePlans, { id: plan.id }));
                });
                selectedInjury.plans = currentPlans;
            }
        });

        treatmentAdminService.getAll().$promise.then(function (results) {
            $scope.availableTreatments = results;
            if (selectedInjury) {
                var currentTreatments = [];
                _.each(selectedInjury.treatments, function (treatment) {
                    currentTreatments.push(_.findWhere($scope.availableTreatments, { id: treatment.id }));
                });
                selectedInjury.treatments = currentTreatments;
            }
        });

        prognosisAdminService.getAll().$promise.then(function(results) {
            $scope.availablePrognoses = results;
        });

        $scope.submit = function () {
            if ($scope.injury && $scope.injury.id > 0) {
                injuryAdminService.update($scope.injury).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close($scope.injury);
                });
            } else {
                injuryAdminService.save($scope.injury).$promise.then(function () {
                    notifierService.notify('Created Successfully!');
                    $modalInstance.close($scope.injury);
                });
            }
        };

        $scope.symptomChanged = function(injurySymptom) {
            if (injurySymptom) {
                injurySymptom.renderTemplate = '';
                injurySymptom.givenResponse = [];
                var symptom = _.findWhere($scope.availableSymptoms, { id: injurySymptom.symptomId });
                if (symptom) {
                    injurySymptom.renderTemplate = symptom.renderTemplate;
                    //TODO: hack... kendo not adding value correctly for dropdowns, need to set initially
                    if (injurySymptom.renderTemplate === 'examine.duration.dropdown') {
                        injurySymptom.givenResponse.push(0);
                    }
                }
            }
        }

        $scope.addSymptom = function () {
            if (!$scope.injury.injurySymptoms) {
                $scope.injury.injurySymptoms = [];
            }

            $scope.injury.injurySymptoms.push({ givenResponse: [] });
        }

        $scope.removeSymptom = function (index) {
            $scope.injury.injurySymptoms.splice(index, 1);
        }

        $scope.addPrognosis = function () {
            if (!$scope.injury.injuryPrognoses) {
                $scope.injury.injuryPrognoses = [];
            }

            $scope.injury.injuryPrognoses.push({});
        }

        $scope.removePrognosis = function (index) {
            $scope.injury.injuryPrognoses.splice(index, 1);
        }



        $scope.reset = function () {
            $scope.injury = null;
            $modalInstance.dismiss('cancel');
        };
    }
]);

injuryAdminModule.controller('InjuryAdminController', ['$scope', 'injuryAdminService', 'configService', '$modal', function ($scope, injuryAdminService, configService, $modal) {

    function getInjuryList() {
        injuryAdminService.getAll().$promise.then(function (results) {
            $scope.injuries = results;
        });
    }

    getInjuryList();


    $scope.bindSelectedInjury = function (injury) {
        $scope.selectedInjury = injury;

        var modalInstance = $modal.open({
            templateUrl: '/app/admin/content/tmpl.injury.modal.htm',
            controller: 'InjuryModalController',
            windowClass: 'xx-dialog',
            resolve: {
                selectedInjury: function () {
                    return $scope.selectedInjury;
                }
            }
        });

        modalInstance.result.then(function (exerciseReturned) {
            getInjuryList();
            $scope.selectedExercise = exerciseReturned;
        });
    }




}]);


injuryAdminModule.factory('injuryAdminService', ['$resource', function ($resource) {

    var adminInjuryPath = '/admin/injuries';


    return {
        get: function (id) {
            var resource = $resource(adminInjuryPath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function () {
            return $resource(adminInjuryPath).query();
        },
        save: function (injury) {
            return $resource(adminInjuryPath).save(injury);
        },
        update: function (injury) {
            var resource = $resource(adminInjuryPath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: injury.id }, injury);
        }
    }



}]);