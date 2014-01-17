define('config.lookups', ['ko', 'jquery', 'knockback','model.filter.collection', 'model.equipment.collection', 'config'],
    function (ko, $, kb, FilterCollection, EquipmentCollection, config) {

        var signFilterCollection = new FilterCollection(),
            causeFilterCollection = new FilterCollection(),
            equipmentCollection = new EquipmentCollection(),
            availableSignFilters = kb.collectionObservable(signFilterCollection),
            availableEquipment = kb.collectionObservable(equipmentCollection),
            availableCauseFilters = kb.collectionObservable(causeFilterCollection),
            functionPlanCategories = ko.observableArray(['Rehabilitation', 'Stretching', 'Preventative', 'Spinal Stabilization', 'Strengthing', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization']),
            functionExerciseCategories = ko.observableArray(['Stretching', 'Spinal Stabilization', 'Strengthing', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization']),
            regionCategories = ko.observableArray(['UpperExtremity', 'LowerExtremity', 'Spine']),
            holdTypes = ko.observableArray(['Seconds', 'Minutes', 'Breaths']),
            causeCategories = ko.observableArray(['Lifestyle', 'Physiological']),
            signCategories = ko.observableArray(['Functional', 'Subjective', 'Visual']);

        
        function init() {
            signFilterCollection.url = config.apiUris.signFilters;
            signFilterCollection.fetch();

            causeFilterCollection.url = config.apiUris.causeFilters;
            causeFilterCollection.fetch();
            
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
            holdTypes: holdTypes,
            causeCategories: causeCategories,
            signCategories: signCategories,
            availableCauseFilters: availableCauseFilters
        };
    });
