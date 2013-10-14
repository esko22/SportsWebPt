define('model.potential.symptom', ['backbone', 'config'],
    function(backbone, config) {

        var potentialSymptom = backbone.RelationalModel.extend({
            urlRoot: config.apiUris.potentialSymptoms,
            idAttribute: 'symptomMatrixId'
    });

        return potentialSymptom;
    });

define('model.potential.symptom.collection', ['backbone', 'model.potential.symptom', 'config'],
    function (backbone, potentialSymptom, config) {
        var
            potentialSymptomCollection = backbone.Collection.extend({
                model: potentialSymptom,
                url: config.apiUris.potentialSymptoms
            });

        return potentialSymptomCollection;

    });

define('model.injury.symptom', ['backbone', 'config', 'model.admin.body.part.matrix.item'],
    function (backbone, config, BodyPartMatrixItem) {

        var injurySymptom = backbone.RelationalModel.extend({
            urlRoot: config.apiUris.symptoms,
            defaults: { 'givenResponse' : []},
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

define('model.admin.symptom', ['backbone', 'config'],
    function (backbone, config) {

        var symptom = backbone.RelationalModel.extend({
            urlRoot: config.apiUris.adminSymptoms,
        });

        return symptom;
    });

define('model.admin.symptom.collection', ['backbone', 'model.admin.symptom', 'config'],
    function (backbone, injurySymptom, config) {
        var
            adminSymptomCollection = backbone.Collection.extend({
                model: injurySymptom,
                url: config.apiUris.adminSymptoms
            });

        return adminSymptomCollection;

    });