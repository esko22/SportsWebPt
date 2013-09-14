define('model.clinic', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            clinic = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.clinics,
                defaults: {
                }
            });

        return clinic;

    });

define('model.clinic.collection', ['backbone', 'model.clinic', 'config'],
    function (backbone, component, config) {
        var
            clinicCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.clinics
            });

        return clinicCollection;

    });
