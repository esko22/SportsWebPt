'use strict';

var sharedUi = angular.module('shared.ui', ['symptom.controls', 'auth.dialogs']);

sharedUi.directive('masterFooter', [function() {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/shared/tmpl.master.footer.htm'
    };
}]);

sharedUi.directive('masterHeader', [function() {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/shared/tmpl.master.header.htm',
        controller: 'HeaderController'
    };
}]);

sharedUi.directive('modalHeader', [ function () {
    return {
        restrict: 'A',
        replace: 'true',
        template: '<div class="modal-header"><button type="button" class="close" ng-click="$dismiss()" aria-hidden="true">&times;</button><img class="modal-logo" src="../../Content/images/logoFooter.png" /></div>'
    };
}]);


sharedUi.controller('HeaderController', [
    '$scope', '$modal', 'userManagementService', '$rootScope', function ($scope, $modal, userManagementService, $rootScope) {

        var currentModal = null;

        userManagementService.getUser().$promise.then(function(user) {
            $scope.currentUser = user;
        });

        $scope.isAuthenticated = userManagementService.isAuthenticated();

        $scope.showSignUp = function () {
            window.location.assign('/auth?returnUrl=');
        };

        $scope.logOut = function () {
            $rootScope.currentUser = null;
            $scope.currentUser = null;
            userManagementService.logOut();
            $scope.isAuthenticated = false;
        };

        $scope.showLogin = function () {
            window.location.assign('/auth?returnUrl=');
        };

        $scope.alreadyHaveAccount = function() {
            if (currentModal)
                currentModal.close();

            $scope.showLogin();
        };

        $scope.createAccount = function () {
            if (currentModal)
                currentModal.close();

            $scope.showSignUp();
        };
       
        $scope.$onRootScope('signup-dialog-prompt', function() {
            $scope.showSignUp();
        });

    }
]);



