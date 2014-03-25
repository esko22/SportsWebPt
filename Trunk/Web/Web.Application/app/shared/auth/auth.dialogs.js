'use strict';

var authDialogModule = angular.module('auth.dialogs', []);

authDialogModule.directive('loginDialog', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/shared/auth/tmpl.login.dialog.htm',
        controller: 'LoginController'
    };
}]);

authDialogModule.directive('signUpDialog', [function () {
    return {
        restrict: 'E',
        replace: 'true',
        templateUrl: '/app/shared/auth/tmpl.signup.dialog.htm',
        controller: 'SignupController'
    };
}]);

authDialogModule.controller('LoginController', [
    '$scope', 'returnUrlService', '$http', function ($scope, returnUrlService, $http) {
        $scope.showOAuthProvider = returnUrlService.getOAuthUrl;

        $scope.login = function () {
            return true;
        };
    }
]);

authDialogModule.controller('SignupController', [
    '$scope', 'returnUrlService', function ($scope, returnUrlService) {
        $scope.showOAuthProvider = returnUrlService.getOAuthUrl;

        $scope.signUp = function () {
            return true;
        };

    }
]);


authDialogModule.controller('AuthFormController', [
    '$scope', function ($scope) {

        $scope.showSignUp = false;
        $scope.showLogin = true;

        $scope.alreadyHaveAccount = function () {
            $scope.showSignUp = false;
            $scope.showLogin = true;
        };

        $scope.createAccount = function () {
            $scope.showLogin = false;
            $scope.showSignUp = true;
        };


    }
]);


authDialogModule.factory("returnUrlService", [
    function() {
        var getReturnUrl = function() {
            var returnUri = location.pathname + location.search;
            var returnUriParams = new Uri(location.search).getQueryParamValues('ReturnUrl');
            if (returnUriParams.length > 0) {
                returnUri = returnUriParams[0];
            }

            if (returnUri === '/logon'
                || returnUri === '/LOGON')
                returnUri = '';

            return returnUri;
        },
        getOAuthUrl = function (provider) {
            window.location.href = '/oauth?provider=' + provider + '&ReturnUrl=' + getReturnUrl();
        };

        return {
            getOAuthUrl: getOAuthUrl
        };
    }
]);

