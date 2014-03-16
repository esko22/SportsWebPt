'use strict';

jQueryPluginModule.directive('viewMedicaDisplay', [function () {
    return {
        restrict: 'A',
        replace: false,
        link: function(scope, element) {

            scope.$watch('animationTag', function(animationTag) {
                if (animationTag) {
                    $(element).attr('id', animationTag);
                    openthis = animationTag;
                    vm_open();
                }
            });
        }
    };
}]);