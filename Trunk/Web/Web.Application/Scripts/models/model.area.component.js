define('model.area.component', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            area = backbone.Model.extend({
                urlRoot: config.apiUris.areaComponents,
                defaults: {
                }
            });

        return area;

    });

define('model.area.component.collection', ['backbone', 'model.area.component'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });