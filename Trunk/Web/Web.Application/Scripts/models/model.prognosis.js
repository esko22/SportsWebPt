define('model.prognosis', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            prognosis = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.prognoses
            });

        return prognosis;

    });

define('model.prognosis.collection', ['backbone', 'model.prognosis', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.prognoses
            });

        return componentCollection;

    });

define('model.admin.prognosis', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            prognosis = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminPrognoses,
                defaults: {
                    'description': '',
                    'category': '',
                    'name': ''
                }
            });

        return prognosis;

    });

define('model.admin.prognosis.collection', ['backbone', 'model.admin.prognosis', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminPrognoses
            });

        return componentCollection;

    });

define('model.injury.prognosis', ['backbone', 'config'],
    function (backbone, config) {

        var injurySymptom = backbone.RelationalModel.extend({
            urlRoot: config.apiUris.prognoses,
        });

        return injurySymptom;
    });

define('model.injury.prognosis.collection', ['backbone', 'model.injury.prognosis', 'config'],
    function (backbone, injuryPrognosis, config) {
        var
            injuryPrognosisCollection = backbone.Collection.extend({
                model: injuryPrognosis,
                url: config.apiUris.adminPrognoses
            });

        return injuryPrognosisCollection;

    });