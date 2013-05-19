define('model.equipment', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            equipment = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.equipment,
                defaults: {
                }
            });

        return equipment;

    });

define('model.equipment.collection', ['backbone', 'model.equipment'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });