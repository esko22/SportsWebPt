'use strict';

swptApp.directive('clingify', function () {
        return {
            restrict: 'A',
            link: function($scope, element) {
                element.clingify();
            }
        };
    });