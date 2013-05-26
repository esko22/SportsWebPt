define('model.sign', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            sign = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.signs,
                defaults: {
                }
            });

        return sign;

    });

define('model.sign.collection', ['backbone', 'model.sign'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });