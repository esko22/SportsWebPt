define('bootstrap.helper',['jquery'] , function($) {

    var popoverErrorPlacement = function (popoverClass) {

        return function(error, element) {
            if (typeof ($(element).data('popover')) === 'undefined') {
                buildPopover(element, error.text(), popoverClass);
            }
            else if ($(element).data('popover').tip().text() != error.text()) {
                removePopover(element);
                buildPopover(element, error.text(), popoverClass);
            }
        };
    },

    popoverUnhighlight = function (element) {
        removePopover(element);
    };

    function buildPopover(element, errorText, popoverClass) {
        if (typeof(popoverClass) === 'undefined')
            popoverClass = '';

        $(element).popover({
            trigger: 'manual',
            content: errorText,
            template: $.validator.format('<div class="popover"><div class="arrow"></div><div class="popover-inner {0}"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>',popoverClass)
        });
        $(element).popover('show');
    };
    
    function removePopover(element) {
        $(element).popover('destroy');
    }

    return {
        popoverErrorPlacement: popoverErrorPlacement,
        popoverUnhighlight: popoverUnhighlight
    };

});