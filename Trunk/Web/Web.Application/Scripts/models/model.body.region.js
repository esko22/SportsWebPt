define('model.body.region', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            bodyRegion = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.bodyRegion,
                defaults: {
                }
            });

        return bodyRegion;

    });

define('model.body.region.collection', ['backbone', 'model.body.region', 'config'],
    function (backbone, bodyRegion, config) {
        var
            bodyRegionCollection = backbone.Collection.extend({
                model: bodyRegion,
                url: config.apiUris.bodyRegion
            });

        return bodyRegionCollection;
    });

define('model.admin.body.region', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            area = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminBodyRegion,
                defaults: {
                    'name': ''
                }
            });

        return area;

    });

define('model.admin.body.region.collection', ['backbone', 'model.admin.body.region', 'config'],
    function (backbone, component,config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminBodyRegion
            });

        return componentCollection;

    });