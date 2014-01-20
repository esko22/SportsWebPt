define('vm.admin.treatments',
    ['ko', 'underscore', 'knockback', 'model.admin.treatment.collection', 'model.admin.treatment', 'error.helper', 'bootstrap.helper', 'config.lookups'],
    function (ko, _, kb, TreatmentCollection, TreatmentModel, err, bh, lookups) {

        var treatmentCollection = new TreatmentCollection(),
            treatments = kb.collectionObservable(treatmentCollection),
            selectedTreatment = kb.viewModel(new TreatmentModel()),
            bindSelectedTreatment = function (data, event) {
                var category = data.model().get('category');
                var provider = data.model().get('provider');
                selectedTreatment.model(data.model());
                selectedTreatment.category(category);
                selectedTreatment.provider(provider);
            },
            onSuccessfulChange = function () {
                bindSelectedTreatment(kb.viewModel(new TreatmentModel()), null);
                treatmentCollection.fetch();
            },
            saveChanges = function () {
                selectedTreatment.model().save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var treatmentValidationOptions = ko.observable({
            debug: true,
            rules: {
                name:
                {
                    required: false,
                    minlength: 1,
                    maxlength: 50
                },
                description:
                {
                    required: false,
                    minlength: 1,
                    maxlength: 10000
                }
            },
            messages: {
                description:
                {
                    minlength: "must be between 1 and 10000 characters",
                    maxlength: "must be between 1 and 10000 characters"
                },
                name:
                {
                    minlength: "must be between 1 and 50 characters",
                    maxlength: "must be between 1 and 50 characters"
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });

        var init = function() {
            treatmentCollection.fetch();
        };
        
        return {
            treatments: treatments,
            selectedTreatment: selectedTreatment,
            saveChanges: saveChanges,
            bindSelectedTreatment: bindSelectedTreatment,
            treatmentValidationOptions: treatmentValidationOptions,
            availableProviders: lookups.treatmentProviders,
            availableCategories: lookups.treatmentCategories,
            init: init
        };
    });