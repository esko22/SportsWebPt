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


    ko.bindingHandlers.showInitPanel = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            $(valueAccessor() + ' .panel-collapse:first').collapse('show');
        }   
    };
    
    ko.bindingHandlers.showInitTab = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            $(valueAccessor() + ' a:first').tab('show');
        }
    };



    ko.bindingHandlers.sublimeVideo = {
        init: function (element, valueAccessor, allBindingsAccessor) {

            if (sublime.prepare === undefined) {
                sublime.ready(function() {
                    sublime.prepare(valueAccessor());
                });
                sublime.load();
            } else {
                sublime.prepare(valueAccessor());
            }
        }
    };

    // ************ bootstrap handlers ***************
    
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
    
    // ************ end bootstrap handlers ***************

    // ************ kendo handlers ***************

    //Applies mask to an element
    ko.bindingHandlers.mask = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor();
            var shouldMask = ko.utils.unwrapObservable(value);

            kendo.ui.progress($(element), shouldMask);
        }
    };

    // ************ end kendo handlers ***************


})(jQuery);