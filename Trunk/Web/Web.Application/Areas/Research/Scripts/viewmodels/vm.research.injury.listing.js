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
            signFilter;


        var onBodyRegionFilter = function(data, event) {
            bodyReginFilter = data.id();
            performFilter();
        };

        var onSignFilter = function (data, event) {
            signFilter = data.id();
            performFilter();
        };

        var performFilter = function() {
            filteredInjuryCollection.reset();

            if (typeof(bodyReginFilter) !== "undefined" && bodyReginFilter !== null) {
                _.each(injuryCollection.models, function(injury) {
                    _.each(injury.get('bodyRegions').models, function(bodyRegion) {
                        if (bodyRegion.get('id') === bodyReginFilter)
                            filteredInjuryCollection.add(injury);
                    });
                });
            }
            else
                filteredInjuryCollection = new Backbone.Collection(injuryCollection.toJSON());
            

            if (typeof (signFilter) !== "undefined" && signFilter !== null) {
                var injuriesToRemove = new InjuryCollection();

                _.each(filteredInjuryCollection.models, function(injury) {
                    _.each(injury.get('signs'), function (sign) {
                        if (typeof(sign) !== "undefined") {
                            if (sign.id !== signFilter)
                                injuriesToRemove.add(injury);
                        }
                    });
                });

                _.each(injuriesToRemove.models, function(injury) {
                    filteredInjuryCollection.remove(injury);
                });
            }
            
            filteredInjuries.collection(filteredInjuryCollection);
        };

        var onReset = function (resetFilter, data) {
            
            if(resetFilter === 'sign')
                signFilter = null;
            if (resetFilter === 'bodyregion')
                bodyReginFilter = null;
            
            performFilter();
        };
            

        bodyRegionCollection.fetch();
        injuryCollection.fetch();
        signCollection.fetch();
        filteredInjuries.collection(injuryCollection);

        return {
            bodyRegions: bodyRegions,
            filteredInjuries: filteredInjuries,
            onSignFilter: onSignFilter,
            onBodyRegionFilter: onBodyRegionFilter,
            briefInjuryTemplate: briefInjuryTemplate,
            onReset: onReset,
            signs : signs
        };
    });