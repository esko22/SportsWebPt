define('vm.research.injury',
    ['jquery', 'knockback', 'model.injury', 'underscore','favorites.helper'],
    function($, kb, Injury, _, favHelper) {

        var injuryModel = new Injury(JSON.parse($('#selected-injury').val())),
            injury = kb.viewModel(injuryModel),
            injuryTemplate = 'examine.report.injury',
            recoveryPlanTemplate = 'research.recovery.plans',
            researchWorkoutPlanTemplate = 'research.injury.plan',
            researchExerciseTemplate = 'research.injury.exercise',
            researchVideoTemplate = 'research.injury.video',
            addAsFavorite = function(data, event) {
                favHelper.addEntityToFavorites('injury', injuryModel.get('id'));
            },
            postExerciseRender = function() {
                sublime.load();
            };

        return {
            recoveryPlanTemplate: recoveryPlanTemplate,
            injuryTemplate: injuryTemplate,
            researchWorkoutPlanTemplate: researchWorkoutPlanTemplate,
            researchExerciseTemplate: researchExerciseTemplate,
            researchVideoTemplate: researchVideoTemplate,
            injury: injury,
            postExerciseRender: postExerciseRender,
            addAsFavorite: addAsFavorite
        };
    });