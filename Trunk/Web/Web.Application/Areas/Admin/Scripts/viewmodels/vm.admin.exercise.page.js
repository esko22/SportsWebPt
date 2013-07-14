define('vm.admin.exercise.page', 
    ['ko', 'underscore', 'knockback', 'jquery', 'vm.admin.exercise.form', 'vm.admin.exercises'],
    function (ko, _, kb, $, form, exercises) {


        var bindViewModels = function () {
            exercises.bindSelectedExercise = form.bindSelectedExercise;
            form.suscribe(exercises.onSuccessfulChangeCallback);
            kb.applyBindings(form, $('#admin-exercise-form').get(0));
            kb.applyBindings(exercises, $('#admin-exercise-list').get(0));
        };

        return {
            bindViewModels: bindViewModels,
        };

    })