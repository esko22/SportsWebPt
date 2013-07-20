define('vm.admin.plan.page',
    ['ko', 'underscore', 'knockback', 'jquery', 'vm.admin.plan.form', 'vm.admin.plans'],
    function (ko, _, kb, $, form, plans) {


        var bindViewModels = function () {
            plans.bindSelectedPlan = form.bindSelectedPlan;
            form.suscribe(plans.onSuccessfulChangeCallback);
            kb.applyBindings(form, $('#admin-plan-form').get(0));
            kb.applyBindings(plans, $('#admin-plan-list').get(0));
        };

        return {
            bindViewModels: bindViewModels,
        };

    })