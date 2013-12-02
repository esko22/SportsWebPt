define('vm.research.exercise.listing',
    ['jquery', 'ko', 'knockback', 'model.exercise.collection', 'underscore', 'model.body.region.collection', 'config'],
    function ($, ko, kb, ExerciseCollection, _, BodyRegionCollection, config) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            exerciseCollection = new ExerciseCollection(),
            filteredExerciseCollection = new ExerciseCollection(),
            filteredExercises = kb.collectionObservable(filteredExerciseCollection),
            briefExerciseTemplate = 'research.brief.exercise',
            isInitialized = ko.observable(false),
            bodyReginFilter,
            categoryFilter,
            categories = config.functionCategories;

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

            filteredExercises.collection(filteredExerciseCollection);
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

        var onReset = function (resetFilter, data) {

            if (resetFilter === 'category')
                clearCategory();
            if (resetFilter === 'bodyregion')
                clearBodyRegions();

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
            categories : categories
        };
    });