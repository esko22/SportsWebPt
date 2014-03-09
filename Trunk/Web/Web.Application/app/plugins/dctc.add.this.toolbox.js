'use strict';

/**
 * AddThis widget directive
 *
 * Usage:
 *   1. include `addthis_widget.js` in header with async=1 parameter
 *   <script src="//s7.addthis.com/js/300/addthis_widget.js#pubid=<pubid>&async=1"></script>
 *   http://support.addthis.com/customer/portal/articles/381263-addthis-client-api#configuration-url
 *   2. add "addthis-toolbox" directive to a widget's toolbox div
 *   <div addthis-toolbox class="addthis_toolbox addthis_default_style addthis_32x32_style">
 *     ...       ^
 *   </div>
 */

jQueryPluginModule.directive('addthisToolbox', function () {
    return {
        restrict: 'A',
        transclude: true,
        replace: true,
        template: '<div ng-transclude></div>',
        link: function ($scope, element, attrs) {
            // Dynamically init for performance reason
            // Safe for multiple calls, only first call will be processed (loaded css/images, popup injected)
            // http://support.addthis.com/customer/portal/articles/381263-addthis-client-api#configuration-url
            // http://support.addthis.com/customer/portal/articles/381221-optimizing-addthis-performance
            addthis.init();
            // Ajax load (bind events)
            // http://support.addthis.com/customer/portal/articles/381263-addthis-client-api#rendering-js-toolbox
            // http://support.addthis.com/customer/portal/questions/548551-help-on-call-back-using-ajax-i-lose-share-buttons
            addthis.toolbox($(element).get());
        }
    }
});