define('vm.admin.exercises',
    ['ko', 'underscore', 'knockback', 'model.admin.exercise.collection'],
    function (ko, _, kb, ExerciseCollection) {

        var exerciseListTemplate = 'admin.exercise.list',
            exerciseCollection = new ExerciseCollection(),
            exercises = kb.collectionObservable(exerciseCollection),
            editExercise = function(data, event) {
            },
            addExercise = function(data, event) {
            },
            onSuccessfulChangeCallback = function() {
                exerciseCollection.fetch();
            },
            init = function() {
                exerciseCollection.fetch();
            };

        
        return {
            exercises: exercises,
            exerciseListTemplate: exerciseListTemplate,
            editExercise: editExercise,
            addExercise: addExercise,
            onSuccessfulChangeCallback: onSuccessfulChangeCallback,
            init : init
        };
    });