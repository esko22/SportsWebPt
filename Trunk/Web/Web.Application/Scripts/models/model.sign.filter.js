define('model.sign.filter', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            signFilter = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.signFilters,
                defaults: {
                }
            });

        return signFilter;

    });

define('model.sign.filter.collection', ['backbone', 'model.sign.filter', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.signFilters
            });

        return componentCollection;

    });
