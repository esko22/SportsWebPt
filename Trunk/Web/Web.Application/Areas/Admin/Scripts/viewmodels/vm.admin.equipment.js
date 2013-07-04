define('vm.admin.equipment',
    ['ko', 'underscore', 'knockback', 'model.equipment.collection'],
    function(ko, _, kb, Equipment) {

        var equipmentCollection = new Equipment();
        equipmentCollection.fetch();
        
        var equip = kb.collectionObservable(equipmentCollection);

        return {
            equip : equip
        };
    });