define('model.symptomatic.body.part', ['backbone', 'config', 'model.potential.symptom', 'model.potential.symptom.collection'],
    function(backbone, config, PotentialSymptom, PotentialSymptomCollection) {

        var symptomaticBodyPart = backbone.RelationalModel.extend({
            urlRoot: config.apiUris.symptomaticComponents,
            idAttribute: 'bodyPartMatrixId',
            relations: [{
                type: backbone.HasMany,
                key: 'potentialSymptoms',
                relatedModel: PotentialSymptom,
                collectionType: PotentialSymptomCollection,
                        reverseRelation: {
                            key: 'bodyPartId',
                            includeInJSON: 'bodyPartMatrixId'
                        }
            }]
        });

        return symptomaticBodyPart;
    });

define('model.symptomatic.body.part.collection', ['backbone', 'model.symptomatic.body.part', 'config'],
    function (backbone, symptomaticBodyPart, config) {
        var
            symptomaticBodyPartCollection = backbone.Collection.extend({
                model: symptomaticBodyPart,
                url: config.apiUris.symptomaticComponents
            });

        return symptomaticBodyPartCollection;

    });

