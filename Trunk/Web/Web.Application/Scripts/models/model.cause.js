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

define('model.admin.cause', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            cause = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminCauses,
                defaults: {
                    'description' :''
                }
            });

        return cause;

    });

define('model.admin.cause.collection', ['backbone', 'model.admin.cause', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminCauses
            });

        return componentCollection;

    });