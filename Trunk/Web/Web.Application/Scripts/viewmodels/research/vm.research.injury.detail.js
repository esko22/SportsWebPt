define('vm.research.injury.detail',
    ['jquery', 'knockback', 'model.injury', 'underscore','favorites.helper', 'ko', 'services', 'config'],
    function($, kb, Injury, _, favHelper, ko, services, config) {

        var injury = kb.viewModel(new Injury()),
            injuryTemplate = 'examine.report.injury',
            researchWorkoutPlanTemplate = 'research.injury.plan',
            researchExerciseTemplate = 'research.injury.exercise',
            researchVideoTemplate = 'research.injury.video',
            addAsFavorite = function(data, event) {
                favHelper.addEntityToFavorites('injury', injury.id());
            },
            isInitialized = ko.observable(false);
        
        function init(searchKey) {
            if (searchKey !== '') {
                services.getEntityDetail(searchKey, config.apiUris.injuryDetail, onFetchSuccess, null, null);
            }
            else {
                injury.model(Injury.findOrCreate(JSON.parse($('#selected-injury').val())));
                postLoadPrep();
            }
            
            isInitialized(true);
        }
        
        function onFetchSuccess(data) {
            var foundInjury = Injury.findOrCreate(data);
            injury.model(foundInjury);
            postLoadPrep();
        }
        
        function postLoadPrep() {
            sublime.load();
            //hate this shit... reloads there player every time
            if (injury.animationTag() !== null) {
                openthis = injury.animationTag();
                vm_open();
            } else {
                $('.viewmedica-container').empty();
            }
        }

        return {
            injuryTemplate: injuryTemplate,
            researchWorkoutPlanTemplate: researchWorkoutPlanTemplate,
            researchExerciseTemplate: researchExerciseTemplate,
            researchVideoTemplate: researchVideoTemplate,
            injury: injury,
            addAsFavorite: addAsFavorite,
            isInitialized: isInitialized,
            init: init
        };
    });