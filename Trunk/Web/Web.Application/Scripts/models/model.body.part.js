define('model.body.part', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            area = backbone.Model.extend({
                urlRoot: config.apiUris.areaComponents,
                defaults: {
                }
            });

        return area;

    });

define('model.body.part.collection', ['backbone', 'model.body.part'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });