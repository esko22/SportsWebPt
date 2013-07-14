define('vm.admin.exercises',
    ['ko', 'underscore', 'knockback', 'model.admin.exercise.collection'],
    function (ko, _, kb, ExerciseCollection) {

        var exerciseListTemplate = 'admin.exercise.list',
            exerciseCollection = new ExerciseCollection(),
            exercises = kb.collectionObservable(exerciseCollection),
            bindSelectedExercise = function (data, event) {
        },
        onSuccessfulChangeCallback = function () {
            exerciseCollection.fetch();
        };

        exerciseCollection.fetch();
        
        return {
            exercises: exercises,
            exerciseListTemplate: exerciseListTemplate,
            bindSelectedExercise: bindSelectedExercise,
            onSuccessfulChangeCallback: onSuccessfulChangeCallback
        };
    });