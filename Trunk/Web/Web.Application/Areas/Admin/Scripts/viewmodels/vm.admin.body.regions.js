define('vm.admin.body.regions',
    ['ko', 'underscore', 'knockback', 'model.admin.body.region.collection', 'model.admin.body.region', 'error.helper', 'bootstrap.helper'],
    function (ko, _, kb, BodyRegionCollection, BodyRegion, err, bh) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            selectedBodyRegion = kb.viewModel(new BodyRegion()),
            bindSelectedBodyRegion = function (data, event) {
                selectedBodyRegion.model(data.model());
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
            init : init
        };
    });