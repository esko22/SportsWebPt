'use strict';

var planAdminModule = angular.module('plan.admin.module', []);

planAdminModule.directive('adminPlanList', [function () {
    return {
        restrict: 'EA',
        scope: true,
        replace: 'true',
        templateUrl: '/app/admin/content/tmpl.plan.list.htm'
    };
}]);


planAdminModule.controller('PlanModalController', [
    '$scope', 'planAdminService', '$modalInstance', 'selectedPlan', 'notifierService',
    'configService', 'exerciseAdminService', 'bodyRegionAdminService',
    function ($scope, planAdminService, $modalInstance, selectedPlan, notifierService, configService, exerciseAdminService, bodyRegionAdminService) {

        $scope.plan = {};
        if (selectedPlan) {
            $scope.plan = selectedPlan;
        }

        //lookups
        $scope.categories = configService.lookups.functionPlanCategories;
        $scope.holdTypes = configService.lookups.holdTypes;
        $scope.repetitionRangeValues = configService.lookups.repetitionRangeValues;
        $scope.editorOptions = configService.kendoEditorOptions;

        bodyRegionAdminService.getAll().$promise.then(function (results) {
            $scope.availableBodyRegions = results;
            if (selectedPlan) {
                var currentBodyRegions = [];
                _.each(selectedPlan.bodyRegions, function (bodyRegion) {
                    currentBodyRegions.push(_.findWhere($scope.availableBodyRegions, { id: bodyRegion.id }));
                });
                selectedPlan.bodyRegions = currentBodyRegions;
            }
        });

        exerciseAdminService.getAll().$promise.then(function (results) {
            $scope.availableExercises = results;
            if (selectedPlan) {
                _.each(selectedPlan.exercises, function(planExercise) {
                    planExercise.exercise = _.findWhere($scope.availableExercises, { id: planExercise.id });
                });
            }
        });


        $scope.addExercise = function () {
            if (!$scope.plan.exercises) {
                $scope.plan.exercises = [];
            }

            $scope.plan.exercises.push({});
        }

        $scope.removeExercise = function (index) {
            $scope.plan.exercises.splice(index,1);
        }


        $scope.submit = function () {
            if ($scope.plan && $scope.plan.id > 0) {
                planAdminService.update($scope.plan).$promise.then(function () {
                    notifierService.notify('Update Success!');
                    $modalInstance.close($scope.plan);
                });
            } else {
                planAdminService.save($scope.plan).$promise.then(function () {
                    notifierService.notify('Created Successfully!');
                    $modalInstance.close($scope.plan);
                });
            }
        };

        $scope.reset = function () {
            $scope.plan = null;
            $modalInstance.dismiss('cancel');
        };
    }
]);

planAdminModule.controller('PlanAdminController', ['$scope', 'planAdminService', 'configService', '$modal', function ($scope, planAdminService, configService, $modal) {

    function getPlanList() {
        planAdminService.getAll().$promise.then(function (results) {
            $scope.plans = results;
        });
    }

    $scope.gridOptions = {
        data: 'plans',
        showGroupPanel: true,
        columnDefs: [{ field: 'id', displayName: 'Id' },
            { field: 'routineName', displayName: 'Name' },
            { field: 'bodyRegions', displayName: 'Body Regions' },
        { field: 'categories', displayName: 'Category' },
        { field: 'visible', displayName: 'Visible' },
        { displayName: 'Action', cellTemplate: '<button id="editBtn" type="button" class="btn-small" ng-click="bindPublishPlan(row.entity)" >Edit</button> ' }]
    };

    $scope.categories = configService.lookups.functionPlanCategories;
    getPlanList();

    $scope.bindPublishPlan = function (plan) {
        $scope.selectedPlan = plan;

        $modal.open({
            templateUrl: '/app/admin/content/tmpl.plan.publish.modal.htm',
            controller: 'PublishPlanAdminController',
            windowClass: 'x-dialog',
            resolve: {
                selectedPlan: function () {
                    return $scope.selectedPlan;
                }
            }
        });
    }




    $scope.bindSelectedPlan = function (plan) {
        $scope.selectedPlan = plan;

        var modalInstance = $modal.open({
            templateUrl: '/app/admin/content/tmpl.plan.modal.htm',
            controller: 'PlanModalController',
            windowClass: 'xx-dialog',
            resolve: {
                selectedPlan: function () {
                    return $scope.selectedPlan;
                }
            }
        });

        modalInstance.result.then(function (planReturned) {
            getPlanList();
            $scope.selectedPlan = planReturned;
        });
    }




}]);

planAdminModule.controller('PublishPlanAdminController', [
    '$scope', 'planAdminService', 'configService', '$modal','selectedPlan', 'notifierService', '$modalInstance', function($scope, planAdminService, configService, $modal, selectedPlan, notifierService, $modalInstance) {

        $scope.plan = {};
        if (selectedPlan) {
            $scope.plan = selectedPlan;
        }

        $scope.submit = function() {
            if ($scope.plan && $scope.plan.id > 0) {
                planAdminService.publish($scope.plan).$promise.then(function() {
                    notifierService.notify('Update Success!');
                    $modalInstance.close($scope.plan);
                });
            }
        }

}]);

planAdminModule.factory('planAdminService', ['$resource', function($resource) {

    var adminPlanPath = '/data/admin/plans';


    return {
        get: function(id) {
            var resource = $resource(adminPlanPath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function() {
            return $resource(adminPlanPath).query();
        },
        save: function(plan) {
            return $resource(adminPlanPath).save(plan);
        },
        update: function(plan) {
            var resource = $resource(adminPlanPath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: plan.id }, plan);
        },
        publish: function(plan) {
            var resource = $resource(adminPlanPath + '/:id/publish', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: plan.id }, plan);
        }


    }
}]);