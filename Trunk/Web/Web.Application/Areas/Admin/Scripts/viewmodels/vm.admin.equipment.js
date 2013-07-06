define('vm.admin.equipment',
    ['ko', 'underscore', 'knockback', 'model.equipment.collection', 'model.equipment'],
    function(ko, _, kb, EquipmentCollection, EquipmentModel) {

        var equipmentListTemplate = 'admin.equipment.list';
        var equipmentModel = new EquipmentModel();
        var equipmentCollection = new EquipmentCollection();
        equipmentCollection.fetch();
        
        var equipment = kb.collectionObservable(equipmentCollection);
        var selectedEquipment = kb.viewModel(equipmentModel);

        var bindSelectedEquipment = function(model) {

        };

        var saveChanges = function () {
            selectedEquipment.model().save();
            equipmentCollection.push(selectedEquipment);
        };

        return {
            equipment: equipment,
            equipmentListTemplate: equipmentListTemplate,
            selectedEquipment: selectedEquipment,
            saveChanges : saveChanges
        };
    });