define('vm.research.exercise.listing',
    ['jquery', 'knockback', 'model.exercise.collection', 'underscore'],
    function ($, kb, ExerciseCollection, _) {

        //var bodyRegionCollection = new BodyRegionCollection(),
        //    bodyRegions = kb.collectionObservable(bodyRegionCollection),
            var exerciseCollection = new ExerciseCollection(),
            //filteredExerciseCollection = new ExerciseCollection(),
            //filteredExercises = kb.collectionObservable(filteredExerciseCollection),
            briefExerciseTemplate = 'research.brief.exercise';

        var onFilterSelect = function (data, event) {

            _.each(exerciseCollection.models, function(exercise) {
                alert(exercise.get('bodyRegions').length);
            });


            //filteredExerciseCollection.reset(_.filter(exerciseCollection.models, function (exercise) {
            //    return _.some(exercise.get('bodyRegions').models, function (bodyRegion) {
            //        return bodyRegion.get('id') === data.id();
            //    });
            //}));
        };

        var onReset = function () {
            //filteredExerciseCollection.reset(exerciseCollection.models);
        };

        //bodyRegionCollection.fetch();
        exerciseCollection.fetch({
            success: onReset
        });

        return {
            //bodyRegions: bodyRegions,
            //filteredExercises: filteredExercises,
            onFilterSelect: onFilterSelect,
            briefExerciseTemplate: briefExerciseTemplate,
            onReset : onReset
        };
    });