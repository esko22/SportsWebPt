﻿define('vm.admin.exercises',
    ['ko', 'underscore', 'knockback', 'model.admin.exercise.collection'],
    function (ko, _, kb, ExerciseCollection) {

        var exerciseListTemplate = 'admin.exercise.list',
            exerciseCollection = new ExerciseCollection(),
            exercises = kb.collectionObservable(exerciseCollection),
            editExercise = function (data, event) {
            },
            addExercise = function (data, event) {
                alert(data);
            },
            onSuccessfulChangeCallback = function () {
                exerciseCollection.fetch();
            };

        exerciseCollection.fetch();
        
        return {
            exercises: exercises,
            exerciseListTemplate: exerciseListTemplate,
            editExercise: editExercise,
            addExercise: addExercise,
            onSuccessfulChangeCallback: onSuccessfulChangeCallback
        };
    });