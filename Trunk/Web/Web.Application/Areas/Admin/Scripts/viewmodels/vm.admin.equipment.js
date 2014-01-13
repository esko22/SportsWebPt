define('vm.admin.equipment',
    ['ko', 'underscore', 'knockback', 'model.admin.equipment.collection', 'model.admin.equipment', 'error.helper', 'bootstrap.helper', 'config', 'config.lookups'],
    function(ko, _, kb, EquipmentCollection, EquipmentModel, err, bh, config, lookups) {

        var equipmentListTemplate = 'admin.equipment.list',
            equipmentCollection = new EquipmentCollection(),
            equipment = kb.collectionObservable(equipmentCollection),
            selectedEquipment = kb.viewModel(new EquipmentModel()),
            availableCategories = lookups.functionExerciseCategories,
            bindSelectedEquipment = function (data, event) {
                var category = data.model().get('category');
                selectedEquipment.model(data.model());
                selectedEquipment.category(category);
            },
            onSuccessfulChange = function () {
                selectedEquipment.model(new EquipmentModel());
                equipmentCollection.fetch();
            },
            saveChanges = function () {
                selectedEquipment.model().save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var equipmentValidationOptions = ko.observable({
            debug: true,
            ignore: config.kendoEditorIgnores,
            rules: {
                commonName:
                {
                    required: true,
                    minlength: 2,
                    maxlength: 30
                },
                technicalName:
                {
                    required: true,
                    minlength: 2,
                    maxlength: 30
                },
                productInformation:
                {
                    required: false,
                    maxlength: 10000
                },
                priceRange:
                {
                    required: false,
                    minlength: 1,
                    maxlength: 10
                }
            },
            messages: {
                commonName:
                {
                    required: "common name required",
                    minlength: "must be between 2 and 30 characters",
                    maxlength: "must be between 2 and 30 characters"
                },
                technicalName: {
                    required: "technical name required",
                    minlength: "must be between 2 and 30 characters",
                    maxlength: "must be between 2 and 30 characters"
                },
                productInformation:
                {
                    maxlength: "must be less than 10000 characters"
                },
                priceRange: {
                    minlength: "must be between 1 and 10 characters",
                    maxlength: "must be between 1 and 10 characters"
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });

        var init = function() {
            equipmentCollection.fetch();
        };
        
        return {
            equipment: equipment,
            equipmentListTemplate: equipmentListTemplate,
            selectedEquipment: selectedEquipment,
            saveChanges: saveChanges,
            bindSelectedEquipment: bindSelectedEquipment,
            equipmentValidationOptions: equipmentValidationOptions,
            init: init,
            availableCategories: availableCategories,
            kendoEditorOptions : config.kendoEditorOptions
        };
    });