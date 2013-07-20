define('vm.research.exercise',
    ['jquery', 'knockback', 'model.exercise', 'underscore'],
    function($, kb, Exercise, _) {

        var exerciseModel = new Exercise(JSON.parse($('#selected-exercise').val())),
        exercise = kb.viewModel(exerciseModel);
        
        return {
            exercise : exercise
        };
    });