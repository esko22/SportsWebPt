define('model.symptomatic.region', ['backbone', 'config', 'model.symptomatic.body.part', 'model.symptomatic.body.part.collection'],
    function (backbone, config, BodyPart, BodyPartCollection) {
        var symptomaticRegion = backbone.RelationalModel.extend({
            urlRoot: config.apiUris.potentialSymptoms,
            idAttribute: 'id',
                relations: [{
                    type: backbone.HasMany,
                    key: 'bodyParts',
                    relatedModel: BodyPart,
                    collectionType: BodyPartCollection,
                    reverseRelation: {
                        key: 'region'
                    }
                }]
            });

        return symptomaticRegion;

    });


define('model.symptomatic.region.collection', ['backbone', 'model.symptomatic.region', 'config'],
    function (backbone, symptomaticRegion, config) {
        var symptomaticRegionCollection = backbone.Collection.extend({
                model: symptomaticRegion,
                url: config.apiUris.symptomaticRegions
            });

        return symptomaticRegionCollection;

    });