(function ($) {
    
    ko.bindingHandlers.validateForm = {
        init: function(element, valueAccessor) {
            $(document).ready(function() {
                var value = valueAccessor();
                var options = ko.utils.unwrapObservable(value);
                $(element).validate(options); // Use "unwrapObservable" so we can handle values that may or may not be observable
            });
        },
        update: function(element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(), allBindings = allBindingsAccessor();
            var options = ko.utils.unwrapObservable(value);
            var frm = $(element).validate(options);
            $.extend(true, frm.settings, options);
            frm.resetForm();
        }
    };
    
    ko.bindingHandlers.bootstrapPopover = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var options = valueAccessor();
            var defaultOptions = {};
            options = $.extend(true, {}, defaultOptions, options);
            $(element).popover(options)
                .click(function (e) {
                    e.preventDefault();
                });
        }
    };

})(jQuery);