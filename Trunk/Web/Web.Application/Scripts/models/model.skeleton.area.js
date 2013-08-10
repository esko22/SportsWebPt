define('model.skeleton.area', ['backbone', 'config','jquery'],
    function (backbone, config, $) {
        var
            area = backbone.RelationalModel.extend({
                urlRoot: config.skeletonAreas,
                defaults: {
                    }
            });

        return area;

    });

define('model.skeleton.area.collection', ['backbone', 'model.skeleton.area'],
    function (backbone, area) {
        var
            areaCollection = backbone.Collection.extend({
                model : area
            });

        return areaCollection;

    });