define('vm.examine.skeleton',
    ['backbone', 'jquery', 'model.skeleton.hotspot.collection', 'knockback', 'underscore', 'vm.examine.container'],
    function (bs,$, HotspotCollection, kb, _, container) {

        var hotspots = new HotspotCollection();
        
        hotspots.reset(JSON.parse($('#skeleton-hotspots').val()));

        var selectHotspot = function (item) {
            if (container.selectedHotspots.indexOf(item) == -1) {
                $('#' + item.name()).addClass('skeleton-selected');
                container.selectedHotspots.push(item);
            } else {
                $('#' + item.name()).removeClass('skeleton-selected');
                container.selectedHotspots.remove(item);
            }
        };
        
        var hotspotMouseOver = function (item) {
            if (container.selectedHotspots.indexOf(item) > -1) {
                $('#' + item.name()).removeClass('skeleton-selected');
            }
            
            $('#' + item.name()).addClass('skeleton-hover');
        };

        var hotspotMouseOut = function (item) {
            $('#' + item.name()).removeClass('skeleton-hover');

            if (container.selectedHotspots.indexOf(item) > -1) {
                $('#' + item.name()).addClass('skeleton-selected');
            }
        };

        return {
            hotspots: new kb.CollectionObservable(hotspots),
            selectHotspot: selectHotspot,
            hotspotMouseOver: hotspotMouseOver,
            hotspotMouseOut: hotspotMouseOut,
            selectedHotspots: container.selectedHotspots
        };
    });