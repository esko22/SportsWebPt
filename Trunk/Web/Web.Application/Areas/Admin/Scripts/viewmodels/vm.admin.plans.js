define('vm.admin.plans',
    ['ko', 'underscore', 'knockback', 'model.admin.plan.collection'],
    function (ko, _, kb, PlanCollection) {

        var planListTemplate = 'admin.plan.list',
            planCollection = new PlanCollection(),
            plans = kb.collectionObservable(planCollection),
            bindSelectedPlan = function (data, event) {
            },
            editPlan = function (data, event) {
            },
            addPlan = function (data, event) {
            },
        onSuccessfulChangeCallback = function () {
            planCollection.fetch();
        };

        planCollection.fetch();

        return {
            plans: plans,
            planListTemplate: planListTemplate,
            bindSelectedPlan: bindSelectedPlan,
            onSuccessfulChangeCallback: onSuccessfulChangeCallback,
            addPlan: addPlan,
            editPlan : editPlan
        };
    });