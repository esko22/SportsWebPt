define('model.admin.body.part', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            area = backbone.Model.extend({
                urlRoot: config.apiUris.areaComponents,
                defaults: {
                }
            });

        return area;

    });

define('model.admin.body.region.collection', ['backbone', 'model.admin.body.part', 'config'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminEquipment
            });

        return componentCollection;

    });