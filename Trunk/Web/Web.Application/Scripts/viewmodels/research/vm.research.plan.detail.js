define('vm.research.plan.detail',
    ['jquery', 'knockback', 'model.plan', 'underscore', 'services', 'config'],
    function($, kb, Plan, _, services, config) {

        var plan = kb.viewModel(new Plan()),
            isInitialized = ko.observable(false);
        
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
        }

        return {
            plan: plan,
            init: init,
            isInitialized : isInitialized
        };
    });