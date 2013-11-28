define('vm.research.exercise.detail',
    ['jquery', 'knockback', 'model.exercise', 'underscore', 'services', 'config'],
    function($, kb, Exercise, _, services, config) {

        var exercise = kb.viewModel(new Exercise()),
            isInitialized = ko.observable(false);


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

            sublime.load();
        }

        return {
            exercise: exercise,
            init: init,
            isInitialized: isInitialized
        };
    });