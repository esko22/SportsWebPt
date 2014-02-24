define('vm.equipment.list.dialog',
    ['knockback', 'model.equipment.collection', 'underscore'],
    function(kb, EquipmentCollection, _) {

        var equipmentCollection = new EquipmentCollection(),
        equipment = kb.collectionObservable(equipmentCollection);

        function init(equipmentViewModels) {
            equipmentCollection.reset();

            _.each(equipmentViewModels, function(equipmentModel) {
                equipmentCollection.add(equipmentModel.model());
            });

        }

        return {
            equipment: equipment,
            init : init
        };

    });
