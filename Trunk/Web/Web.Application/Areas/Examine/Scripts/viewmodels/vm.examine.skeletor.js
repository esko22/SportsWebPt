define('vm.examine.skeletor',
    ['backbone', 'jquery', 'model.skeleton.hotspot.collection', 'knockback', 'underscore'],
    function (bs,$, HotspotCollection, kb, _) {

        var hotspots = new HotspotCollection();
        hotspots.reset(JSON.parse($('#skeleton-hotspots').val()));

        return {
            hotspots : new kb.CollectionObservable(hotspots)
        };
    });