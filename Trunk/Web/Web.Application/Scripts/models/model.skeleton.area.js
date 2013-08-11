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

define('model.admin.skeleton.area', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            area = backbone.RelationalModel.extend({
                urlRoot: config.adminSkeletonAreas,
                defaults: {
                }
            });

        return area;

    });

define('model.admin.skeleton.area.collection', ['backbone', 'model.skeleton.area','config'],
    function (backbone, area, config) {
        var
            areaCollection = backbone.Collection.extend({
                model: area,
                url: config.apiUris.adminSkeletonAreas,
            });

        return areaCollection;

    });