﻿define('vm.research.injury.detail',
    ['jquery', 'knockback', 'model.injury', 'underscore', 'ko', 'services', 'config', 'vm.viewmedica.display', 'vm.share.bar'],
    function($, kb, Injury, _, ko, services, config, ViewMedicaDisplay, ShareBar) {

        var injury = kb.viewModel(new Injury()),
            isInitialized = ko.observable(false),
            viewMedicaDisplay = new ViewMedicaDisplay(),
            shareBar = new ShareBar();
        
        function init(searchKey) {
            injury.model(new Injury());
            if (searchKey !== '') {
                services.getEntityDetail(searchKey, config.apiUris.injuryDetail, onFetchSuccess, null, null);
            }
            else {
                injury.model(Injury.findOrCreate(JSON.parse($('#selected-injury').val())));
                postLoadPrep();
            }
            
            isInitialized(true);
        }
        
        function onFetchSuccess(data) {
            injury.model(Injury.findOrCreate(data));
            _.each(injury.model().get('plans').models, function (plan) {
                if (plan.get('exercises').length === 0) {
                    plan.fetch();
                }
            });
            postLoadPrep();
        }
        
        function postLoadPrep() {
            viewMedicaDisplay.init(injury.animationTag(), '#research-injury-detail');
            shareBar.init($.format("{0}/{1}/{2}", config.favoriteUri, config.favoriteHashTags.injuryHash, injury.pageName()), 'injury', injury.id());
            $('#injury-plan-accordion .panel-collapse:first').collapse('show');
        }
        
        function setDefaultPanel() {
            $('#injury-plan-accordion .panel-collapse:first').collapse('show');
        }

        return {
            injury: injury,
            isInitialized: isInitialized,
            init: init,
            shareBar: shareBar,
            viewMedicaDisplay: viewMedicaDisplay,
            setDefaultPanel : setDefaultPanel
        };
    });