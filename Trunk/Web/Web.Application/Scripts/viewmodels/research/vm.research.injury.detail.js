define('vm.research.injury.detail',
    ['jquery', 'knockback', 'model.injury', 'underscore','favorites.helper', 'ko', 'services'],
    function($, kb, Injury, _, favHelper, ko, services) {

        var injuryModel = new Injury(),
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
            },
            isInitialized = ko.observable(false);
        function init(searchKey) {
            if (searchKey !== '') {
                services.getInjuryDetail(searchKey, onFetchSuccess, null, null);
            }
            else {
                injury.model(Injury.findOrCreate(JSON.parse($('#selected-injury').val())));
            }
            
            isInitialized(true);
        }
        
        function onFetchSuccess(data) {
            var foundInjury = Injury.findOrCreate(data);
            injury.model(foundInjury);
        }

        return {
            recoveryPlanTemplate: recoveryPlanTemplate,
            injuryTemplate: injuryTemplate,
            researchWorkoutPlanTemplate: researchWorkoutPlanTemplate,
            researchExerciseTemplate: researchExerciseTemplate,
            researchVideoTemplate: researchVideoTemplate,
            injury: injury,
            postExerciseRender: postExerciseRender,
            addAsFavorite: addAsFavorite,
            isInitialized: isInitialized,
            init: init
        };
    });