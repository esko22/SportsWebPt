'use strict';

jQueryPluginModule.directive('kendoCloak', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {

            attrs.$observe('kendoCloak', function (applyMask) {
                if(applyMask === 'true')
                    kendo.ui.progress(element, true);
                else 
                    kendo.ui.progress(element, false);
                
            });
        }
    };
});