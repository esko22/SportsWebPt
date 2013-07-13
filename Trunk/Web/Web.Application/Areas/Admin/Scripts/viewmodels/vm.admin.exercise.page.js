define('vm.admin.exercise.page', 
    ['ko', 'underscore', 'knockback', 'jquery', 'vm.admin.exercise.form', 'vm.admin.exercises'],
    function (ko, _, kb, $, form, Exercises) {

        var bindViewModels = function () {

            Exercises.bindSelectedExercise = form.bindSelectedExercise;

            kb.applyBindings(form, $('#admin-exercise-form').get(0));
            kb.applyBindings(Exercises, $('#admin-exercise-list').get(0));
        };

        return {
            bindViewModels: bindViewModels
        };

    })