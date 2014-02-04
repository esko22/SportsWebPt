define('vm.admin.prognoses',
    ['ko', 'underscore', 'knockback', 'model.admin.prognosis.collection', 'model.admin.prognosis', 'error.helper', 'bootstrap.helper', 'config.lookups'],
    function (ko, _, kb, PrognosisCollection, PrognosisModel, err, bh, lookups) {

        var prognosisCollection = new PrognosisCollection(),
            prognoses = kb.collectionObservable(prognosisCollection),
            selectedPrognosis = kb.viewModel(new PrognosisModel()),
            bindSelectedPrognosis = function (data, event) {
                var category = data.model().get('category');
                selectedPrognosis.model(data.model());
                selectedPrognosis.category(category);
            },
            onSuccessfulChange = function () {
                bindSelectedPrognosis(kb.viewModel(new PrognosisModel()), null);
                prognosisCollection.fetch();
            },
            saveChanges = function () {
                selectedPrognosis.model().save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var prognosisValidationOptions = ko.observable({
            debug: true,
            rules: {
                name:
                {
                    required: true,
                    minlength: 1,
                    maxlength: 50
                },
                description:
                {
                    required: true,
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
            prognosisCollection.fetch();
        };
        
        return {
            prognoses: prognoses,
            selectedPrognosis: selectedPrognosis,
            saveChanges: saveChanges,
            bindSelectedPrognosis: bindSelectedPrognosis,
            prognosisValidationOptions: prognosisValidationOptions,
            availableCategories: lookups.prognosisCategories,
            init: init
        };
    });