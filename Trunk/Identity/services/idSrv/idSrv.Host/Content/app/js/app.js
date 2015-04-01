/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */

/// <reference path="../libs/angular/angular.1.2.13.js" />

(function () {
    "use strict";

    var app = angular.module("app", ["util.module", "ngPasswordStrength"]);

    app.controller("LayoutCtrl", function ($scope, Model) {
        $scope.model = Model;

        $scope.signUp = function() {
            return true;
        }

        $scope.login = function () {
            return true;
        }

    });

    app.controller("RegistrationCtrl", function($scope, Model) {

        $scope.user = {};
        $scope.user.password = '';
        $scope.user.passStrength = 0;
        $scope.mode = 'bootstrap';

    });

    app.controller("PasswordResetCtrl", function ($scope, Model) {
        $scope.model = Model;
        $scope.mode = 'bootstrap';
        $scope.model.passStrength = 0;


        $scope.$watch('model.passStrength', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                if ($scope.model.passStrength >= 70) {
                    $scope.validPassword = 'success';
                }
                else
                    $scope.validPassword = 'error';
            }
        });

            $scope.$watch('model.Password', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.model.Password = newVal;

                if ($scope.model.Password) {
                    $scope.validPassword = 'success';
                }
                else
                    $scope.validPassword = 'error';


                if ($scope.model.Password !== $scope.model.ConfirmPassword) {
                    $scope.validConfirmPassword = 'error';
                } else {
                    $scope.validConfirmPassword = 'success';
                }

            }
        });

        $scope.$watch('model.ConfirmPassword', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.model.ConfirmPassword = newVal;
                if ($scope.model.Password !== $scope.model.ConfirmPassword) {
                    $scope.validConfirmPassword = 'error';
                } else {
                    $scope.validConfirmPassword = 'success';
                }
                console.log("validConfirmPassword=" + $scope.validConfirmPassword);
            }
        });
    });


    app.directive("antiForgeryToken", function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                token: "="
            },
            template: "<input type='hidden' name='{{token.name}}' value='{{token.value}}'>"
        };
    });


    app.directive("masterHeader", function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "/content/app/templates/tmpl.master.header.htm"
        };
    });

    app.directive("login", function () {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: "/content/app/templates/tmpl.login.htm"
        };
    });

})();

(function () {
    var json = document.getElementById("modelJson").textContent;
    var model = JSON.parse(json);
    angular.module("app").constant("Model", model);
})();
