define('model.injury', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            injury = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.injuries,
                defaults: {
                }
            });

        return injury;

    });

define('model.injury.collection', ['backbone', 'model.injury'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });