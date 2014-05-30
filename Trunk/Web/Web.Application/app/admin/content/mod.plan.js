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

    $scope.categories = configService.lookups.functionPlanCategories;
    getPlanList();


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


planAdminModule.factory('planAdminService', ['$resource', function ($resource) {

    var adminPlanPath = '/data/admin/plans';


    return {
        get: function (id) {
            var resource = $resource(adminPlanPath + '/:id');
            return resource.get({ id: id });
        },
        getAll: function () {
            return $resource(adminPlanPath).query();
        },
        save: function (plan) {
            return $resource(adminPlanPath).save(plan);
        },
        update: function (plan) {
            var resource = $resource(adminPlanPath + '/:id', null, {
                'update': { method: 'PUT' }
            });
            return resource.update({ id: plan.id }, plan);
        }
    }



}]);