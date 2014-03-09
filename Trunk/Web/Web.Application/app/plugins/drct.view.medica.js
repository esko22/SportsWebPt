'use strict';

jQueryPluginModule.directive('viewMedicaDisplay', function () {
    return {
        restrict: 'EA',
        scope: { code: '=' },
        replace: true,
        template: '<div class="viewmedica-container" id="{{animationTag}}"></div>',
        link: function (scope, element) {
            scope.$watch('code', function(newVal) {
                if (newVal) {
                    scope.animationTag = newVal;
                }
            });

            setTimeout(function() {
                openthis = scope.animationTag;
                vm_open();
            },2000);

        }
    };
});