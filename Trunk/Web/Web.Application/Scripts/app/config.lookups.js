define('config.lookups', ['ko', 'jquery', 'knockback','model.sign.filter.collection', 'model.equipment.collection'],
    function (ko, $, kb, SignFilters, EquipmentCollection) {

        var signFilterCollection = new SignFilters(),
            equipmentCollection = new EquipmentCollection(),
            availableSignFilters = kb.collectionObservable(signFilterCollection),
            availableEquipment = kb.collectionObservable(equipmentCollection),
            functionPlanCategories = ko.observableArray(['Rehabilitation', 'Stretching', 'Preventative', 'Spinal Stabilization', 'Strengthing', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization']),
            functionExerciseCategories = ko.observableArray(['Stretching', 'Spinal Stabilization', 'Strengthing', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization']),
            regionCategories = ko.observableArray(['UpperExtremity', 'LowerExtremity', 'Spine']),
            holdTypes = ko.observableArray(['Seconds', 'Minutes', 'Breaths']);

        
        function init() {
            signFilterCollection.fetch();
            equipmentCollection.fetch();
        }

        //need to build in some promises
        init();

        return {
            availableSignFilters: availableSignFilters,
            availableEquipment: availableEquipment,
            functionPlanCategories: functionPlanCategories,
            functionExerciseCategories: functionExerciseCategories,
            regionCategories: regionCategories,
            holdTypes: holdTypes
        };
    });
