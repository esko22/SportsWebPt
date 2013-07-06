define('vm.admin.equipment',
    ['ko', 'underscore', 'knockback', 'model.equipment.collection', 'model.equipment', 'error.helper', 'bootstrap.helper'],
    function(ko, _, kb, EquipmentCollection, EquipmentModel, err, bh) {

        var equipmentListTemplate = 'admin.equipment.list',
            equipmentCollection = new EquipmentCollection(),
            equipment = kb.collectionObservable(equipmentCollection),
            selectedEquipment = kb.viewModel(new EquipmentModel()),
            bindSelectedEquipment = function (data, event) {
                selectedEquipment.model(data.model());
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
            rules: {
                commonName:
                {
                    required: true,
                    minlength: 4,
                    maxlength: 30
                },
                technicalName:
                {
                    required: true,
                    minlength: 4,
                    maxlength: 30
                },
                recommendedVendor:
                {
                    required: false,
                    minlength: 4,
                    maxlength: 30
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
                    minlength: "must be between 4 and 30 characters",
                    maxlength: "must be between 4 and 30 characters"
                },
                technicalName: {
                    required: "technical name required",
                    minlength: "must be between 4 and 30 characters",
                    maxlength: "must be between 4 and 30 characters"
                },
                recommendedVendor:
                {
                    minlength: "must be between 4 and 30 characters",
                    maxlength: "must be between 4 and 30 characters"
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


        equipmentCollection.fetch();

        return {
            equipment: equipment,
            equipmentListTemplate: equipmentListTemplate,
            selectedEquipment: selectedEquipment,
            saveChanges: saveChanges,
            bindSelectedEquipment: bindSelectedEquipment,
            equipmentValidationOptions: equipmentValidationOptions
        };
    });