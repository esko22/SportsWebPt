define('vm.research.injury.listing',
    ['jquery', 'config', 'knockback', 'ko', 'model.body.region.collection', 'model.injury.collection', 'underscore', 'model.sign.collection'],
    function ($, config, kb, ko, BodyRegionCollection, InjuryCollection, _, SignCollection) {

        var bodyRegionCollection = new BodyRegionCollection(),
            bodyRegions = kb.collectionObservable(bodyRegionCollection),
            injuryCollection = new InjuryCollection(),
            filteredInjuryCollection = new InjuryCollection(),
            filteredInjuries = kb.collectionObservable(filteredInjuryCollection),
            briefInjuryTemplate = 'research.brief.injury',
            signCollection = new SignCollection(),
            signs = kb.collectionObservable(signCollection),
            bodyReginFilter,
            signFilter,
            isInitialized = ko.observable(false);


        var onBodyRegionFilter = function (data, event) {
            clearBodyRegions();
            $(event.target).addClass("selected-filter-item");
            bodyReginFilter = data;
            performFilter();
        };

        var onSignFilter = function (data, event) {
            clearSigns();
            $(event.target).addClass("selected-filter-item");
            signFilter = data;
            performFilter();
        };

        var performFilter = function() {
            filteredInjuryCollection.reset();

            if (typeof(bodyReginFilter) !== "undefined" && bodyReginFilter !== null) {
                filteredInjuryCollection = new InjuryCollection(_.filter(injuryCollection.models, function (injury) {
                    var bodyRegionMatch = _.find(injury.get('bodyRegions').models, function (bodyRegion) {
                        return (bodyRegion.id === bodyReginFilter.id());
                    });

                    return typeof (bodyRegionMatch) !== "undefined";
                }));

            }
            else
                filteredInjuryCollection = new InjuryCollection(injuryCollection.toJSON());
            

            if (typeof (signFilter) !== "undefined" && signFilter !== null) {
                filteredInjuryCollection = new InjuryCollection(_.filter(filteredInjuryCollection.models, function (injury) {
                   var signMatch = _.find(injury.get('signs').models, function(sign) {
                        return (sign.get('id') === signFilter.id());
                    });

                    return typeof (signMatch) !== "undefined";
                }));
            }
            
            filteredInjuries.collection(filteredInjuryCollection);
        };

        var onReset = function (resetFilter, data) {
            
            if (resetFilter === 'sign') {
                clearSigns();
            }
            if (resetFilter === 'bodyregion') {
                clearBodyRegions();
            }
            
            performFilter();
        };


        var init = function () {
            if (!isInitialized()) {
                $.when(
                    bodyRegionCollection.fetch(),
                    injuryCollection.fetch(),
                    signCollection.fetch()).done(onInitComplete);
            }
        };
        
        function clearSigns() {
            $("#injury-signs-filter-list .selected-filter-item").removeClass("selected-filter-item");
            signFilter = null;
        }

        function clearBodyRegions() {
            $("#injury-bodyregions-filter-list .selected-filter-item").removeClass("selected-filter-item");
            bodyReginFilter = null;
        }


        function onInitComplete() {
            filteredInjuries.collection(injuryCollection);
            isInitialized(true);
        }

        return {
            bodyRegions: bodyRegions,
            filteredInjuries: filteredInjuries,
            onSignFilter: onSignFilter,
            onBodyRegionFilter: onBodyRegionFilter,
            briefInjuryTemplate: briefInjuryTemplate,
            onReset: onReset,
            signs: signs,
            init: init,
            isInitialized : isInitialized
        };
    });