define('vm.admin.body.regions',
    ['ko', 'underscore', 'knockback', 'model.admin.body.region.collection', 'model.admin.body.region', 'error.helper', 'bootstrap.helper', 'config'],
    function (ko, _, kb, BodyRegionCollection, BodyRegion, err, bh, config) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            availableCategories = config.regionCategories;
            selectedBodyRegion = kb.viewModel(new BodyRegion()),
            bindSelectedBodyRegion = function (data, event) {
                var category = data.model().get('regionCategory');
                selectedBodyRegion.model(data.model());
                selectedBodyRegion.regionCategory(category);
            },
            onSuccessfulChange = function () {
                bindSelectedBodyRegion(kb.viewModel(new BodyRegion()), null);
                bodyRegionCollection.fetch();
            },
            saveChanges = function () {
                selectedBodyRegion.model().save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var bodyRegionValidationOptions = ko.observable({
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
            bodyRegionCollection.fetch();
        };
        
        return {
            bodyRegions: bodyRegions,
            selectedBodyRegion: selectedBodyRegion,
            saveChanges: saveChanges,
            bindSelectedBodyRegion: bindSelectedBodyRegion,
            bodyRegionValidationOptions: bodyRegionValidationOptions,
            init: init,
            availableCategories: availableCategories
        };
    });