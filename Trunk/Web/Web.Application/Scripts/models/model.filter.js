define('model.filter', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            filter = backbone.RelationalModel.extend({
                defaults: {
                }
            });

        return filter;

    });

define('model.filter.collection', ['backbone', 'model.filter', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
            });

        return componentCollection;

    });
