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

define('model.equipment.collection', ['backbone', 'model.equipment', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.equipment
            });

        return componentCollection;

    });


define('model.admin.equipment', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            equipment = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminEquipment,
                defaults: {
                    'commonName': '',
                    'technicalName': '',
                    'recommendedVendor': '',
                    'priceRange': '',
                    'category': ''
                }
            });

        return equipment;

    });

define('model.admin.equipment.collection', ['backbone', 'model.admin.equipment', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminEquipment
            });

        return componentCollection;

    });