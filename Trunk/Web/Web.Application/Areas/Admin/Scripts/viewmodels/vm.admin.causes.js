define('vm.admin.causes',
    ['ko', 'underscore', 'knockback', 'model.admin.cause.collection', 'model.admin.cause', 'error.helper', 'bootstrap.helper', 'config.lookups'],
    function (ko, _, kb, CauseCollection, CauseModel, err, bh, lookups) {

        var causeCollection = new CauseCollection(),
            causes = kb.collectionObservable(causeCollection),
            selectedCause = kb.viewModel(new CauseModel()),
            bindSelectedCause = function (data, event) {
                var category = data.model().get('category');
                var filterId = data.model().get('filterId');
                selectedCause.model(data.model());
                selectedCause.category(category);
                selectedCause.filterId(filterId);
            },
            onSuccessfulChange = function () {
                bindSelectedCause(kb.viewModel(new CauseModel()), null);
                causeCollection.fetch();
            },
            saveChanges = function () {
                selectedCause.model().save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var causeValidationOptions = ko.observable({
            debug: true,
            rules: {
                description:
                {
                    required: false,
                    minlength: 1,
                    maxlength: 100
                }
            },
            messages: {
                description:
                {
                    minlength: "must be between 1 and 100 characters",
                    maxlength: "must be between 1 and 100 characters"
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });

        var init = function() {
            causeCollection.fetch();
        };
        
        return {
            causes: causes,
            selectedCause: selectedCause,
            saveChanges: saveChanges,
            bindSelectedCause: bindSelectedCause,
            causeValidationOptions: causeValidationOptions,
            availableCauseFilters: lookups.availableCauseFilters,
            availableCategories: lookups.causeCategories,
            init: init
        };
    });