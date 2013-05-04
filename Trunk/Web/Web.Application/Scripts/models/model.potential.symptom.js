define('model.potential.symptom', ['backbone', 'config'],
    function(backbone, config) {

        var potentialSymptom = backbone.RelationalModel.extend({
            urlRoot: config.apiUris.symptoms,
            idAttribute: 'symptomMatrixId'
        });

        return potentialSymptom;
    });

define('model.potential.symptom.collection', ['backbone', 'model.potential.symptom', 'config'],
    function (backbone, potentialSymptom, config) {
        var
            potentialSymptomCollection = backbone.Collection.extend({
                model: potentialSymptom,
                url: config.apiUris.symptomaticComponents
            });

        return potentialSymptomCollection;

    });
