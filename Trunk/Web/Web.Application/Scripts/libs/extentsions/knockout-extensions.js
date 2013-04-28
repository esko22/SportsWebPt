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

    ko.bindingHandlers.kendoUISlider = {
        init: function(element, valueAccessor) {
            $(document).ready(function () {
                $(element).kendoSlider({
                    min: 1,
                    max: 5,
                    orientation: "horizontal",
                    smallStep: 1,
                    largeStep: 2
                });

            });
        }
    };

})(jQuery);