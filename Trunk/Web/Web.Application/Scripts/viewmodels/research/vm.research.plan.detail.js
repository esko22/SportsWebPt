define('vm.research.plan.detail',
    ['jquery', 'knockback', 'model.plan', 'underscore', 'services', 'config', 'vm.viewmedica.display', 'vm.share.bar', 'model.plan.exercise', 'youtube.video.manager'],
    function($, kb, Plan, _, services, config, ViewMedicaDisplay, ShareBar, Exercise, youtubeManager) {

        var plan = kb.viewModel(new Plan()),
            isInitialized = ko.observable(false),
            viewMedicaDisplay = new ViewMedicaDisplay(),
            shareBar = new ShareBar(),
            exerciseModel = new Exercise(),
            selectedExercise = kb.viewModel(exerciseModel);


        function init(searchKey) {
            selectedExercise.model(exerciseModel);

            if (searchKey !== '') {
                services.getEntityDetail(searchKey, config.apiUris.planDetail, onFetchSuccess, null, null);
            }
            else {
                plan.model(Plan.findOrCreate(JSON.parse($('#selected-plan').val())));
                postDataPrep();
            }
            
            isInitialized(true);
        }

        function onFetchSuccess(data) {
            var foundPlan = Plan.findOrCreate(data);
            plan.model(foundPlan);
            postDataPrep();
        }
        
        function postDataPrep() {
            viewMedicaDisplay.init(plan.animationTag(), '#research-plan-detail');
            shareBar.init($.format("{0}/{1}/{2}", config.favoriteUri, config.favoriteHashTags.planHash, plan.pageName()), 'plan', plan.id());
            updateSelectedExercise(plan.exercises()[0]);
            setDefaultTab();
        }
        
        function setDefaultTab() {
            $('#plan-exercise-tab-container a:first').tab('show');
        }

        function updateSelectedExercise(data) {
            selectedExercise.model(data.model());
            var video = data.model().get('videos').models[0];
            youtubeManager.addVideoInstance('ytplayer-' + plan.id() + '-' + video.get('id'));
        }

        return {
            plan: plan,
            init: init,
            isInitialized: isInitialized,
            viewMedicaDisplay: viewMedicaDisplay,
            shareBar: shareBar,
            setDefaultTab: setDefaultTab,
            selectedExercise: selectedExercise,
            updateSelectedExercise: updateSelectedExercise
        };
    });