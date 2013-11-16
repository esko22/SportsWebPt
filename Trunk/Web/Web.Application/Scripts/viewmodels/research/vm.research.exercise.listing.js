define('vm.research.exercise.listing',
    ['jquery', 'ko', 'knockback', 'model.exercise.collection', 'underscore', 'model.body.region.collection'],
    function ($, ko, kb, ExerciseCollection, _, BodyRegionCollection) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            exerciseCollection = new ExerciseCollection(),
            filteredExerciseCollection = new ExerciseCollection(),
            filteredExercises = kb.collectionObservable(filteredExerciseCollection),
            briefExerciseTemplate = 'research.brief.exercise',
            isInitialized = ko.observable(false);

        var onFilterSelect = function (data, event) {
            filteredExerciseCollection.reset(_.filter(exerciseCollection.models, function (exercise) {
                return _.some(exercise.get('bodyRegions').models, function (bodyRegion) {
                    return bodyRegion.get('id') === data.id();
                });
            }));
        };

        var onReset = function () {
            filteredExerciseCollection.reset(exerciseCollection.models);
        };

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
            isInitialized(true);
        }

        return {
            bodyRegions: bodyRegions,
            filteredExercises: filteredExercises,
            onFilterSelect: onFilterSelect,
            briefExerciseTemplate: briefExerciseTemplate,
            onReset: onReset,
            init: init,
            isInitialized: isInitialized
        };
    });