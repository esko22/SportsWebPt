/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */

/// <reference path="../libs/angular/angular.1.2.13.js" />

(function () {
    "use strict";

    var app = angular.module("app", ["util.module"]);

    app.controller("LayoutCtrl", function ($scope, Model) {
        $scope.model = Model;

        $scope.signUp = function() {
            return true;
        }

        $scope.login = function () {
            return true;
        }

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
