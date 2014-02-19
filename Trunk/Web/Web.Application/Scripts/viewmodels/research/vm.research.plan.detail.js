define('vm.research.plan.detail',
    ['jquery', 'knockback', 'model.plan', 'underscore', 'services', 'config', 'vm.viewmedica.display', 'vm.share.bar', 'model.plan.exercise'],
    function($, kb, Plan, _, services, config, ViewMedicaDisplay, ShareBar, Exercise) {

        var plan = kb.viewModel(new Plan()),
            isInitialized = ko.observable(false),
            viewMedicaDisplay = new ViewMedicaDisplay(),
            shareBar = new ShareBar(),
            selectedExercise = kb.viewModel(new Exercise());


        function init(searchKey) {
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
            selectedExercise.model(plan.exercises()[0].model());
            setDefaultTab();
        }
        
        function setDefaultTab() {
            $('#plan-exercise-tab-container a:first').tab('show');
        }

        function updateSelectedExercise(data) {
            selectedExercise.model(data.model());
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