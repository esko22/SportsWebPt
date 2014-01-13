define('vm.research.plan.listing',
    ['jquery', 'config', 'knockback', 'ko', 'model.body.region.collection', 'model.plan.collection', 'underscore', 'config.lookups'],
    function ($, config, kb, ko, BodyRegionCollection, PlanCollection, _, lookups) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            planCollection = new PlanCollection(),
            filteredPlanCollection = new PlanCollection(),
            filteredPlans = kb.collectionObservable(filteredPlanCollection),
            briefPlanTemplate = 'research.brief.plan',
            categories = lookups.functionPlanCategories,
            bodyReginFilter,
            categoryFilter,
            isInitialized = ko.observable(false);


        var onBodyRegionFilter = function (data, event) {
            clearBodyRegions();
            $(event.target).addClass("selected-filter-item");
            bodyReginFilter = data;
            performFilter();
        };

        var onCategoryFilter = function (data, event) {
            clearCategory();
            $(event.target).addClass("selected-filter-item");
            categoryFilter = data;
            performFilter();
        };

        var performFilter = function() {
            filteredPlanCollection.reset();

            if (typeof (bodyReginFilter) !== "undefined" && bodyReginFilter !== null) {
                filteredPlanCollection = new PlanCollection(_.filter(planCollection.models, function (plan) {
                    var bodyRegionMatch = _.find(plan.get('bodyRegions').models, function (bodyRegion) {
                        return (bodyRegion.id === bodyReginFilter.id());
                    });

                    return typeof (bodyRegionMatch) !== "undefined";
                }));

            }
            else
                filteredPlanCollection = new PlanCollection(planCollection.toJSON());

            if (typeof(categoryFilter) !== "undefined" && categoryFilter !== null) {
                filteredPlanCollection = new PlanCollection(_.filter(filteredPlanCollection.models, function(plan) {
                    var categoryMatch = _.find(plan.get('categories'), function(category) {
                        return (category.toLowerCase() === categoryFilter.toLowerCase());
                    });

                    return typeof(categoryMatch) !== "undefined";
                }));
            }
            
            filteredPlans.collection(filteredPlanCollection);
        };

        var onReset = function (resetFilter, data) {

            if (resetFilter === 'category')
                clearCategory();
            if (resetFilter === 'bodyregion')
                clearBodyRegions();
            
            performFilter();
        };

        var init = function () {
            if (!isInitialized()) {
                $.when(
                    bodyRegionCollection.fetch(),
                    planCollection.fetch()).done(onInitComplete);
            }
        };
        
        function clearCategory() {
            $("#plan-categories-filter-list .selected-filter-item").removeClass("selected-filter-item");
            categoryFilter = null;
        }

        function clearBodyRegions() {
            $("#plan-bodyregions-filter-list .selected-filter-item").removeClass("selected-filter-item");
            bodyReginFilter = null;
        }

        function onInitComplete() {
            filteredPlans.collection(planCollection);
            isInitialized(true);
        }

        return {
            bodyRegions: bodyRegions,
            filteredPlans: filteredPlans,
            onCategoryFilter: onCategoryFilter,
            onBodyRegionFilter: onBodyRegionFilter,
            briefPlanTemplate: briefPlanTemplate,
            onReset: onReset,
            categories: categories,
            init: init,
            isInitialized: isInitialized
        };
    });