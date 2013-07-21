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

define('model.sign.collection', ['backbone', 'model.sign', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.signs
            });

        return componentCollection;

    });

define('model.admin.sign', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            sign = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminSigns,
                defaults: {
                    'description': '',
                    'category': ''
                }
            });

        return sign;

    });

define('model.admin.sign.collection', ['backbone', 'model.admin.sign', 'config'],
    function (backbone, component,config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminSigns
            });

        return componentCollection;

    });