define('vm.examine.container', ['knockback', 'model.symptomatic.region.collection'],
    function (kb, SymptomaticRegionCollection) {
        var selectedSypmtomaticRegions = new SymptomaticRegionCollection();
        var selectedAreas = kb.collectionObservable(selectedSypmtomaticRegions);

        return {
            selectedAreas: selectedAreas
        };
    });