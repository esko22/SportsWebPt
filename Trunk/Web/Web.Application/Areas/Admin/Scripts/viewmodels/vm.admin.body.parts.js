define('vm.admin.body.parts',
    ['ko', 'underscore', 'knockback', 'model.admin.body.part.collection', 'model.admin.body.part', 'error.helper', 'bootstrap.helper', 'model.admin.skeleton.area.collection'],
    function (ko, _, kb, BodyPartCollection, BodyPart, err, bh, SkeletonAreaCollection) {

        var bodyPartCollection = new BodyPartCollection(),
            skeletonAreaCollection = new SkeletonAreaCollection(),
            availableAreas = kb.collectionObservable(skeletonAreaCollection),
            bodyParts = kb.collectionObservable(bodyPartCollection),
            selectedBodyPart = new BodyPart(),
            commonName = kb.observable(selectedBodyPart, 'commonName'),
            scientificName = kb.observable(selectedBodyPart, 'scientificName'),
            primaryAreas = kb.collectionObservable(selectedBodyPart.get('primaryAreas'), availableAreas.shareOptions()),
            secondaryAreas = kb.collectionObservable(selectedBodyPart.get('secondaryAreas'), availableAreas.shareOptions()),
            bindSelectedBodyPart = function (data, event) {
                selectedBodyPart.get('primaryAreas').reset();
                selectedBodyPart.get('secondaryAreas').reset();

                _.each(data.primaryAreas(), function (viewModel) {
                    _.each(skeletonAreaCollection.models, function (skeletonAreaModel) {
                        if (viewModel.id() === skeletonAreaModel.get('id')) {
                            selectedBodyPart.get('primaryAreas').add(skeletonAreaModel);
                        }
                    });
                });
                
                _.each(data.secondaryAreas(), function (viewModel) {
                    _.each(skeletonAreaCollection.models, function (skeletonAreaModel) {
                        if (viewModel.id() === skeletonAreaModel.get('id')) {
                            selectedBodyPart.get('secondaryAreas').add(skeletonAreaModel);
                        }
                    });
                });

                selectedBodyPart.set('commonName', data.commonName());
                selectedBodyPart.set('scientificName', data.scientificName());
                selectedBodyPart.id = data.model().get('id');
            },
            onSuccessfulChange = function () {
                bindSelectedBodyPart(kb.viewModel(new BodyPart()), null);
                bodyPartCollection.fetch();
            },
            saveChanges = function () {
                var bodyPart = BodyPart.findOrCreate(selectedBodyPart);
                bodyPart.save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var bodyPartValidationOptions = ko.observable({
            debug: true,
            rules: {
                commonName:
                {
                    required: true,
                    minlength: 1,
                    maxlength: 100
                },
                scientificName:
                {
                    required: false,
                    minlength: 1,
                    maxlength: 100
                },
                primaryAreas:
                {
                    required: true,
                }
            },
            messages: {
                commonName:
                {
                    minlength: "must be between 1 and 100 characters",
                    maxlength: "must be between 1 and 100 characters"
                },
                scientificName:
                {
                    minlength: "must be between 1 and 100 characters",
                    maxlength: "must be between 1 and 100 characters"
                },
                primaryAreas: {
                    required: "must select a skeleton area",
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });

        var init = function() {
            bodyPartCollection.fetch();
            skeletonAreaCollection.fetch();
        };
        
        return {
            bodyParts: bodyParts,
            selectedBodyPart: selectedBodyPart,
            saveChanges: saveChanges,
            bindSelectedBodyPart: bindSelectedBodyPart,
            bodyPartValidationOptions: bodyPartValidationOptions,
            availableAreas: availableAreas,
            secondaryAreas: secondaryAreas,
            primaryAreas: primaryAreas,
            commonName: commonName,
            scientificName: scientificName,
            init : init
        };
    });