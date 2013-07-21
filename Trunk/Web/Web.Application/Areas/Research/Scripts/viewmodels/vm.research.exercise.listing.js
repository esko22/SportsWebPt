define('vm.research.exercise.listing',
    ['jquery', 'knockback', 'model.body.region.collection', 'model.exercise.collection', 'underscore'],
    function ($, kb, BodyRegionCollection, ExerciseCollection, _) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            exerciseCollection = new ExerciseCollection(),
            filteredExerciseCollection = new ExerciseCollection(),
            filteredExercises = kb.collectionObservable(filteredExerciseCollection),
            briefExerciseTemplate = 'research.brief.exercise';

        var onFilterSelect = function(data, event) {
            filteredExerciseCollection.reset();

            _.each(exerciseCollection.models, function(exercise) {
                _.each(exercise.get('bodyRegions').models, function(bodyRegion) {
                    if (bodyRegion.get('id') === data.id())
                        filteredExerciseCollection.add(exercise);
                });
            });

            filteredExercises.collection(filteredExerciseCollection);
        };

        var onReset = function() {
            filteredExercises.collection(exerciseCollection);
        };
            

        bodyRegionCollection.fetch();
        exerciseCollection.fetch();
        filteredExercises.collection(exerciseCollection);


        return {
            bodyRegions: bodyRegions,
            filteredExercises: filteredExercises,
            onFilterSelect: onFilterSelect,
            briefExerciseTemplate: briefExerciseTemplate,
            onReset : onReset
        };
    });