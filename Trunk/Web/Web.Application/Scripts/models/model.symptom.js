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

define('model.injury.symptom', ['backbone', 'config', 'model.body.part.matrix.item'],
    function (backbone, config, BodyPartMatrixItem) {

        var injurySymptom = backbone.RelationalModel.extend({
            urlRoot: config.apiUris.symptoms,
            relations: [
                    {
                        type: backbone.HasOne,
                        key: 'bodyPartMatrixItem',
                        relatedModel: BodyPartMatrixItem,
                    }
            ]
        });

        return injurySymptom;
    });

define('model.injury.symptom.collection', ['backbone', 'model.injury.symptom', 'config'],
    function (backbone, injurySymptom, config) {
        var
            injurySymptomCollection = backbone.Collection.extend({
                model: injurySymptom,
                url: config.apiUris.adminInjurySymptoms
            });

        return injurySymptomCollection;

    });