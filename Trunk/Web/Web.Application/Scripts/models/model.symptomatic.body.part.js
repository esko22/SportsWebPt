define('model.symptomatic.body.part', ['backbone', 'config', 'model.body.part.matrix.item', 'model.symptom.matrix.item'],
    function(backbone, config, BodyPartMatrixItem, SymptomMatrixItem) {

        var symptomaticBodyPart = backbone.RelationalModel.extend({
            urlRoot: config.symptomaticComponents,
            defaults: {
                
            },
            relations: [{
                    type: backbone.HasMany,
                    key: 'potentialSymptoms',
                    relatedModel: SymptomMatrixItem,
                    reverseRelation: {
                        key: 'bodyPartId',
                        includeInJSON: 'id'
                    }
                },
                {
                    type: backbone.HasMany,
                    key: 'regions',
                    relatedModel: BodyPartMatrixItem,
                    reverseRelation: {
                        key: 'bodyPartId',
                        includeInJSON: 'id'
                    }
                }
            ]
        });

        return symptomaticBodyPart;
    });

define('model.symptom.matrix.item', ['backbone'],
    function(backbone) {
        var symptomMatrixItem = backbone.RelationalModel.extend({
            
        });

        return symptomMatrixItem;
    });

define('model.symptomatic.body.part.collection', ['backbone', 'model.symptomatic.body.part'],
    function (backbone, symptomaticBodyPart) {
        var
            symptomaticBodyPartCollection = backbone.Collection.extend({
                model: symptomaticBodyPart,
                url: config.symptomaticComponents
            });

        return symptomaticBodyPartCollection;

    });