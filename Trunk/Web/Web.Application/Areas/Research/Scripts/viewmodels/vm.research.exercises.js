define('vm.research.exercises',
    ['jquery', 'knockback', 'model.body.region.collection', 'model.exercise.collection'],
    function ($, kb, BodyRegionCollection, ExerciseCollection) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            exerciseCollection = new ExerciseCollection(),
            filteredExerciseCollection = new ExerciseCollection(),
            filteredExercises = kb.collectionObservable(filteredExerciseCollection),
            briefExerciseTemplate = 'research.brief.exercise';

        var onFilterSelect = function(data) {
            filteredExerciseCollection.reset();
            
        };
            

        bodyRegionCollection.fetch();
        exerciseCollection.fetch();

        filteredExercises.collection(exerciseCollection);


        return {
            bodyRegions: bodyRegions,
            filteredExercises: filteredExercises,
            onFilterSelect: onFilterSelect,
            briefExerciseTemplate: briefExerciseTemplate
        };
    });