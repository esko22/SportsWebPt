'use strict';

jQueryPluginModule.directive('clingify', function () {
        return {
            restrict: 'A',
            link: function($scope, element) {
                element.clingify({
                    breakpoint: 1010,
                    locked: function () {
                        $('#menu-logo').css('visibility', 'visible');
                    },
                    detached: function () {
                        $('#menu-logo').css('visibility', 'hidden');
                    }
                });
            }
        };
    });