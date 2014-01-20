define('model.treatment', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            treatment = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.treatments
            });

        return treatment;

    });

define('model.treatment.collection', ['backbone', 'model.treatment', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.treatments
            });

        return componentCollection;

    });

define('model.admin.treatment', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            treatment = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminTreatments,
                defaults: {
                    'description': '',
                    'category': '',
                    'provider': '',
                    'name': ''
                }
            });

        return treatment;

    });

define('model.admin.treatment.collection', ['backbone', 'model.admin.treatment', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminTreatments
            });

        return componentCollection;

    });