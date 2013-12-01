define('vm.research.plan.detail',
    ['jquery', 'knockback', 'model.plan', 'underscore', 'services', 'config', 'vm.viewmedica.display', 'vm.share.bar'],
    function($, kb, Plan, _, services, config, ViewMedicaDisplay, ShareBar) {

        var plan = kb.viewModel(new Plan()),
            isInitialized = ko.observable(false),
            viewMedicaDisplay = new ViewMedicaDisplay(),
            shareBar = new ShareBar();


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
            viewMedicaDisplay.init(plan.animationTag());
            shareBar.init($.format("{0}/{1}/{2}", config.favoriteUri, config.favoriteHashTags.planHash, plan.pageName()), 'plan', plan.id());
        }

        return {
            plan: plan,
            init: init,
            isInitialized: isInitialized,
            viewMedicaDisplay: viewMedicaDisplay,
            shareBar : shareBar
        };
    });