define('vm.research.exercise.detail',
    ['jquery', 'knockback', 'model.exercise', 'underscore', 'services', 'config', 'vm.share.bar'],
    function($, kb, Exercise, _, services, config, ShareBar) {

        var exercise = kb.viewModel(new Exercise()),
            isInitialized = ko.observable(false),
            shareBar = new ShareBar();
        


        function init(searchKey) {
            if (searchKey !== '') {
                services.getEntityDetail(searchKey, config.apiUris.exerciseDetail, onFetchSuccess, null, null);
            }
            else {
                exercise.model(Exercise.findOrCreate(JSON.parse($('#selected-exercise').val())));
            }

            isInitialized(true);
        }

        function onFetchSuccess(data) {
            var foundExercise = Exercise.findOrCreate(data);
            exercise.model(foundExercise);
            postDataPrep();
        }
        
        function postDataPrep() {
            shareBar.init($.format("{0}/{1}/{2}", config.favoriteUri, config.favoriteHashTags.exerciseHash, exercise.pageName()), 'exercise', exercise.id());
        }

        return {
            exercise: exercise,
            init: init,
            isInitialized: isInitialized,
            shareBar : shareBar
        };
    });