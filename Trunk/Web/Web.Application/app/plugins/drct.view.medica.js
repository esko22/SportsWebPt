'use strict';

jQueryPluginModule.directive('viewMedicaDisplay', function () {
    return {
        restrict: 'A',
        replace: false,
        link: function (scope, element) {
            $(element).attr('id', scope.animationTag);
            openthis = scope.animationTag;
            vm_open();
        }
    };
});