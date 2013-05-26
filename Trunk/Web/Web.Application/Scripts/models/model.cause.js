define('model.cause', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            cause = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.causes,
                defaults: {
                }
            });

        return cause;

    });

define('model.cause.collection', ['backbone', 'model.cause'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });