define('vm.research.injury.detail',
    ['jquery', 'knockback', 'model.injury', 'underscore', 'ko', 'services', 'config', 'vm.viewmedica.display', 'vm.share.bar'],
    function($, kb, Injury, _, ko, services, config, ViewMedicaDisplay, ShareBar) {

        var injury = kb.viewModel(new Injury()),
            isInitialized = ko.observable(false),
            viewMedicaDisplay = new ViewMedicaDisplay(),
            shareBar = new ShareBar();
        
        function init(searchKey) {
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
            var foundInjury = Injury.findOrCreate(data);
            injury.model(foundInjury);
            postLoadPrep();
        }
        
        function postLoadPrep() {
            viewMedicaDisplay.init(injury.animationTag());
            shareBar.init($.format("{0}/{1}/{2}", config.favoriteUri, config.favoriteHashTags.injuryHash, injury.pageName()),'injury',injury.id());
        }

        return {
            injury: injury,
            isInitialized: isInitialized,
            init: init,
            shareBar: shareBar,
            viewMedicaDisplay : viewMedicaDisplay
        };
    });