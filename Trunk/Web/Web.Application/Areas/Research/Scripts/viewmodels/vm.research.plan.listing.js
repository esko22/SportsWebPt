define('vm.research.plan.listing',
    ['jquery', 'config', 'knockback', 'ko', 'model.body.region.collection', 'model.plan.collection', 'underscore'],
    function ($, config, kb, ko, BodyRegionCollection, PlanCollection, _) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            planCollection = new PlanCollection(),
            filteredPlanCollection = new PlanCollection(),
            filteredPlans = kb.collectionObservable(filteredPlanCollection),
            briefPlanTemplate = 'research.brief.plan',
            categories = config.planCategories,
            bodyReginFilter,
            categoryFilter;


        var onBodyRegionFilter = function(data, event) {
            bodyReginFilter = data.id();
            performFilter();
        };

        var onCategoryFilter = function (data, event) {
            categoryFilter = data;
            performFilter();
        };

        var performFilter = function() {
            filteredPlanCollection.reset();

            if (typeof(bodyReginFilter) !== "undefined" && bodyReginFilter !== null) {
                _.each(planCollection.models, function(plan) {
                    _.each(plan.get('bodyRegions').models, function(bodyRegion) {
                        if (bodyRegion.get('id') === bodyReginFilter)
                            filteredPlanCollection.add(plan);
                    });
                });
            }
            else
                filteredPlanCollection = new Backbone.Collection(planCollection.toJSON());
            

            if (typeof (categoryFilter) !== "undefined" && categoryFilter !== null) {
                var plansToRemove = new PlanCollection();

                _.each(filteredPlanCollection.models, function(plan) {
                    if (plan.get('category').toLowerCase() !== categoryFilter.toLowerCase())
                        plansToRemove.add(plan);
                });

                _.each(plansToRemove.models, function(plan) {
                    filteredPlanCollection.remove(plan);
                });
            }
            
            filteredPlans.collection(filteredPlanCollection);
        };

        var onReset = function (resetFilter, data) {
            
            if(resetFilter === 'category')
                categoryFilter = null;
            if (resetFilter === 'bodyregion')
                bodyReginFilter = null;
            
            performFilter();
        };

        var init = function() {
            bodyRegionCollection.fetch();
            planCollection.fetch();
            filteredPlans.collection(planCollection);
        };

        return {
            bodyRegions: bodyRegions,
            filteredPlans: filteredPlans,
            onCategoryFilter: onCategoryFilter,
            onBodyRegionFilter: onBodyRegionFilter,
            briefPlanTemplate: briefPlanTemplate,
            onReset: onReset,
            categories: categories,
            init : init
        };
    });