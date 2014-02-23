define('vm.research.exercise.listing',
    ['jquery', 'ko', 'knockback', 'model.exercise.collection', 'underscore', 'model.body.region.collection', 'config', 'config.lookups'],
    function ($, ko, kb, ExerciseCollection, _, BodyRegionCollection, config, lookups) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            exerciseCollection = new ExerciseCollection(),
            filteredExerciseCollection = new ExerciseCollection(),
            filteredExercises = kb.collectionObservable(filteredExerciseCollection),
            briefExerciseTemplate = 'research.brief.exercise',
            isInitialized = ko.observable(false),
            bodyReginFilter,
            categoryFilter,
            equipmentFilter,
            categories = lookups.functionExerciseCategories;

        var performFilter = function (data, event) {
            filteredExerciseCollection.reset();

            if (typeof (bodyReginFilter) !== "undefined" && bodyReginFilter !== null) {
                filteredExerciseCollection = new ExerciseCollection(_.filter(exerciseCollection.models, function (exercise) {
                    var bodyRegionMatch = _.find(exercise.get('bodyRegions').models, function (bodyRegion) {
                        return (bodyRegion.id === bodyReginFilter.id());
                    });

                    return typeof (bodyRegionMatch) !== "undefined";
                }));

            }
            else
                filteredExerciseCollection = new ExerciseCollection(exerciseCollection.toJSON());

            if (typeof (categoryFilter) !== "undefined" && categoryFilter !== null) {
                filteredExerciseCollection = new ExerciseCollection(_.filter(filteredExerciseCollection.models, function (exercise) {
                    var categoryMatch = _.find(exercise.get('categories'), function (category) {
                        return (category.toLowerCase() === categoryFilter.toLowerCase());
                    });

                    return typeof (categoryMatch) !== "undefined";
                }));
            }
            
            if (typeof (equipmentFilter) !== "undefined" && equipmentFilter !== null) {
                filteredExerciseCollection = new ExerciseCollection(_.filter(filteredExerciseCollection.models, function (exercise) {
                    var equipmentMatch = _.find(exercise.get('equipment').models, function (equipment) {
                        return (equipment.id === equipmentFilter.id());
                    });

                    return typeof (equipmentMatch) !== "undefined";
                }));
            }

            filteredExercises.collection(filteredExerciseCollection);
            $('#visible-exercises').quicksand($('#filtered-exercises'));
        };

        var onBodyRegionFilter = function (data, event) {
            clearBodyRegions();
            $(event.target).addClass("selected-filter-item");
            bodyReginFilter = data;
            performFilter();
        };

        var onCategoryFilter = function (data, event) {
            clearCategory();
            $(event.target).addClass("selected-filter-item");
            categoryFilter = data;
            performFilter();
        };

        var onEquipmentFilter = function(data, event) {
            clearEquipment();
            $(event.target).addClass("selected-filter-item");
            equipmentFilter = data;
            performFilter();
        };

        var onReset = function (resetFilter, data) {

            if (resetFilter === 'category')
                clearCategory();
            if (resetFilter === 'bodyregion')
                clearBodyRegions();
            if (resetFilter === 'equipment')
                clearEquipment();

            performFilter();
        };
        
        function clearCategory() {
            $("#exercise-categories-filter-list .selected-filter-item").removeClass("selected-filter-item");
            categoryFilter = null;
        }

        function clearBodyRegions() {
            $("#exercise-bodyregions-filter-list .selected-filter-item").removeClass("selected-filter-item");
            bodyReginFilter = null;
        }
        
        function clearEquipment() {
            $("#exercise-equipment-filter-list .selected-filter-item").removeClass("selected-filter-item");
            equipmentFilter = null;
        }

        var init = function() {
            if (!isInitialized()) {
                $.when(
                    bodyRegionCollection.fetch(),
                    exerciseCollection.fetch({
                        success: onReset
                    })).done(onInitComplete);
            }
        };
        
        function onInitComplete() {
            filteredExercises.collection(exerciseCollection);
            isInitialized(true);
        }

        return {
            bodyRegions: bodyRegions,
            filteredExercises: filteredExercises,
            briefExerciseTemplate: briefExerciseTemplate,
            onReset: onReset,
            init: init,
            isInitialized: isInitialized,
            onBodyRegionFilter: onBodyRegionFilter,
            onCategoryFilter: onCategoryFilter,
            onEquipmentFilter: onEquipmentFilter, 
            categories: categories,
            equipment: lookups.availableEquipment
        };
    });