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


sharedUi.controller('HeaderController', [
    '$scope', '$modal', 'userManagementService', function ($scope, $modal, userManagementService) {

        var currentModal = null;
        $scope.currentUser = null;
        $scope.isAuthenticated = userManagementService.isAuthenticated();

        $scope.showSignUp = function () {
            currentModal = $modal.open({
                templateUrl: '/app/shared/auth/tmpl.signup.modal.htm',
                scope: $scope
            });
        };

        $scope.showLogin = function () {
            currentModal =$modal.open({
                templateUrl: '/app/shared/auth/tmpl.login.modal.htm',
                scope: $scope
            });
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

        if (userManagementService.isAuthenticated()) {
            userManagementService.getUser().$promise.then(function(user) {
                $scope.currentUser = user;
            });
        }

        $scope.$onRootScope('signup-dialog-prompt', function() {
            $scope.showSignUp();
        });

    }
]);



