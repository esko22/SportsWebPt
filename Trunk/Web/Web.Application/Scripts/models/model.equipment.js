define('model.equipment', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            equipment = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.equipment,
                defaults: {
                    "commonName": "caesar salad"
                }
            });

        return equipment;

    });

define('model.equipment.collection', ['backbone', 'model.equipment', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.equipment
            });

        return componentCollection;

    });