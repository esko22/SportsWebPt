define('model.skeleton.hotspot', ['backbone', 'config','jquery'],
    function (backbone, config, $) {
        var
            hotspot = backbone.Model.extend({
                urlRoot: config.skeletonHotspots,
                defaults: {
                    }
            });

        return hotspot;

    });

define('model.skeleton.hotspot.collection', ['backbone', 'model.skeleton.hotspot'],
    function (backbone, hotspot) {
        var
            hotspotCollection = backbone.Collection.extend({
                model : hotspot
            });

        return hotspotCollection;

    });