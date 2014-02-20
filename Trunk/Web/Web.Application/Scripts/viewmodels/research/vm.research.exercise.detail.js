define('vm.research.exercise.detail',
    ['jquery', 'knockback', 'model.exercise', 'underscore', 'services', 'config', 'vm.share.bar', 'youtube.video.manager', 'model.video'],
    function($, kb, Exercise, _, services, config, ShareBar, youtubeManager, Video) {

        var exerciseModel = new Exercise(),
            exercise = kb.viewModel(exerciseModel),
            isInitialized = ko.observable(false),
            shareBar = new ShareBar();

        function init(searchKey) {
            exercise.model(exerciseModel);
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
            youtubeManager.addVideoInstance('ytplayer-' + exercise.id());
        }

        return {
            exercise: exercise,
            init: init,
            isInitialized: isInitialized,
            shareBar: shareBar
        };
    });