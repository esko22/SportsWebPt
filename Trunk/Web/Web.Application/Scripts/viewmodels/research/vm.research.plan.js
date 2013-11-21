define('vm.research.plan',
    ['jquery', 'knockback', 'model.plan', 'underscore'],
    function($, kb, Plan, _) {

        var planModel = new Plan(JSON.parse($('#selected-plan').val())),
        plan = kb.viewModel(planModel);
        
        return {
            plan : plan
        };
    });