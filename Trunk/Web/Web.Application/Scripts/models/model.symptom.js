define('model.symptom', ['backbone', 'config'],
    function (backbone, config) {
        var
            symptom = backbone.RelationalModel.extend({
                urlRoot: config.symptoms,
                idAttribute: 'id'
               
            });

        return symptom;

    });

define('model.symptom.collection', ['backbone', 'model.symptom'],
    function (backbone, symptom) {
        var
            symptomCollection = backbone.Collection.extend({
                model: symptom
            });

        return symptomCollection;

    });