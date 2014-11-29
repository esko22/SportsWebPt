'use strict';

var injuryAdminModule = angular.module('injury.admin.module', []);


injuryAdminModule.controller('InjuryModalController', [
    '$scope', 'injuryAdminService', '$modalInstance', 'selectedInjury', 'notifierService',
    'configService', 'causeAdminService', 'bodyRegionAdminService', 'signAdminService', 'planAdminService', 'treatmentAdminService', 'prognosisAdminService',
    function ($scope, injuryAdminService, $modalInstance, selectedInjury, notifierService, configService, causeAdminService, bodyRegionAdminService, signAdminService, planAdminService, treatmentAdminService, prognosisAdminService) {

        $scope.injury = {};
        if (selectedInjury) {
            injuryAdminService.get(selectedInjury.id).$promise.then(function(result) {
                $scope.injury = result;
                setSelectedItems();
            });
        } else {
            setSelectedItems();
        }
        $scope.symptomsEnabled = true;

        //lookups
        $scope.repetitionRangeValues = configService.lookups.repetitionRangeValues;
        $scope.editorOptions = configService.kendoEditorOptions;

        configService.availableSymptomsProxy.query().$promise.then(function (results) {
            $scope.availableSymptoms = results;
        });

        configService.bodyPartMatrixProxy.query().$promise.then(function (results) {
            $scope.availableBodyPartMatrix = results;
        });


        function setSelectedItems() {

            causeAdminService.getAll().$promise.then(function(results) {
                $scope.availableCauses = results;
                //have to get items from available collection
                if ($scope.injury) {
                    var currentCauses = [];
                    _.each($scope.injury.causes, function (cause) {
                        currentCauses.push(_.findWhere($scope.availableCauses, { id: cause.id }));
                    });
                    $scope.injury.causes = currentCauses;
                }
            });

            bodyRegionAdminService.getAll().$promise.then(function(results) {
                $scope.availableBodyRegions = results;
                if ($scope.injury) {
                    var currentBodyRegions = [];
                    _.each($scope.injury.bodyRegions, function (bodyRegion) {
                        currentBodyRegions.push(_.findWhere($scope.availableBodyRegions, { id: bodyRegion.id }));
                    });
                    $scope.injury.bodyRegions = currentBodyRegions;
                }
            });

            signAdminService.getAll().$promise.then(function(results) {
                $scope.availableSigns = results;
                if ($scope.injury) {
                    var currentSigns = [];
                    _.each($scope.injury.signs, function (sign) {
                        currentSigns.push(_.findWhere($scope.availableSigns, { id: sign.id }));
                    });
                    $scope.injury.signs = currentSigns;
                }
            });

            planAdminService.getAll().$promise.then(function(results) {
                $scope.availablePlans = results;
                if ($scope.injury) {
                    var currentPlans = [];
                    _.each($scope.injury.plans, function (plan) {
                        currentPlans.push(_.findWhere($scope.availablePlans, { id: plan.id }));
                    });
                    $scope.injury.plans = currentPlans;
                }
            });

            treatmentAdminService.getAll().$promise.then(function(results) {
                $scope.availableTreatments = results;
                if ($scope.injury) {
                    var currentTreatments = [];
                    _.each($scope.injury.treatments, function (treatment) {
                        currentTreatments.push(_.findWhere($scope.availableTreatments, { id: treatment.id }));
                    });
                    $scope.injury.treatments = currentTreatments;
                }
            });

            prognosisAdminService.getAll().$promise.then(function (results) {
                $scope.availablePrognoses = results;
            });

        }


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

injuryAdminModule.controller('InjuryPublishController', ['$scope', 'injuryAdminService', 'configService', '$modal', function ($scope, injuryAdminService, configService, $modal) {

    function getInjuryList() {
        injuryAdminService.getAll().$promise.then(function (results) {
            $scope.injuries = results;
        });
    }

    getInjuryList();

    $scope.gridOptions = {
        data: 'injuries',
        showGroupPanel: true,
        columnDefs: [{ field: 'id', displayName: 'Id' },
            { field: 'medicalName', displayName: 'Name' },
            { field: 'commonName', displayName: 'Common Name' },
        { field: 'visible', displayName: 'Visible' },
        { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindPublishInjury(row.entity)" >Edit</button> ' }]
    };

    $scope.bindPublishInjury = function (injury) {
        $scope.selectedInjury = injury;

        $modal.open({
            templateUrl: '/app/admin/injuries/tmpl.injury.publish.modal.htm',
            controller: 'PublishInjuryAdminController',
            windowClass: 'x-dialog',
            resolve: {
                selectedInjury: function () {
                    return $scope.selectedInjury;
                }
            }
        });
    }

}]);


injuryAdminModule.controller('InjuryAdminController', ['$scope', 'injuryAdminService', 'configService', '$modal', function ($scope, injuryAdminService, configService, $modal) {

    function getInjuryList() {
        injuryAdminService.getAll().$promise.then(function (results) {
            $scope.injuries = results;
        });
    }

    getInjuryList();

    $scope.gridOptions = {
        data: 'injuries',
        showGroupPanel: true,
        columnDefs: [{ field: 'id', displayName: 'Id' },
            { field: 'medicalName', displayName: 'Name' },
            { field: 'commonName', displayName: 'Common Name' },
        { field: 'visible', displayName: 'Visible' },
        { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindSelectedInjury(row.entity)" >Edit</button> ' }]
    };


    $scope.bindSelectedInjury = function (injury) {
        $scope.selectedInjury = injury;

        var modalInstance = $modal.open({
            templateUrl: '/app/admin/injuries/tmpl.injury.modal.htm',
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


injuryAdminModule.controller('PublishInjuryAdminController', [
    '$scope', 'injuryAdminService', 'configService', '$modal', 'selectedInjury', 'notifierService', '$modalInstance', function ($scope, injuryAdminService, configService, $modal, selectedInjury, notifierService, $modalInstance) {

        $scope.injury = {};
        if (selectedInjury) {
            $scope.injury = selectedInjury;
        }

        $scope.submit = function () {
            if ($scope.injury && $scope.injury.id > 0) {
                injuryAdminService.publish($scope.injury).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close($scope.injury);
                });
            }
        }

    }]);




injuryAdminModule.factory('injuryAdminService', ['$resource', function ($resource) {

    var adminInjuryPath = '/data/admin/injuries';


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
        },
        publish: function (injury) {
            var resource = $resource(adminInjuryPath + '/:id/publish', null, {
            'update': { method: 'PUT' }
        });
        return resource.update({ id: injury.id }, injury);
    }
    }



}]);