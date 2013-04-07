define('bootstrap.helper',['jquery'] , function($) {

    var popoverErrorPlacement = function (popoverClass) {

        return function(error, element) {
            if (typeof($(element).data('popover')) === 'undefined') {
                $(element).popover({
                    trigger: 'manual',
                    content: error.text(),
                    template: '<div class="popover"><div class="arrow"></div><div class="popover-inner ' + popoverClass + '"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>'
                });
                $(element).popover('show');
            }
        };
    },

    popoverUnhighlight = function(element) {
        $(element).popover('destroy');
    };

    return {
        popoverErrorPlacement: popoverErrorPlacement,
        popoverUnhighlight: popoverUnhighlight
    };

});