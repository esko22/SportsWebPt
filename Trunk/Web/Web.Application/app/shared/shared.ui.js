'use strict';

var sharedUi = angular.module('shared.ui', ['symptom.controls']);

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
        templateUrl: '/app/shared/tmpl.master.header.htm'
    };
}]);


