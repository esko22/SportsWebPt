define('model.symptomatic.region', ['backbone', 'config', 'model.body.part.matrix.item', 'model.symptomatic.region.collection'],
    function (backbone, config, BodyPartMatrixItem, RegionCollection) {
        var symptomaticRegion = backbone.RelationalModel.extend({
                urlRoot: config.symptomaticRegions,
                defaults: {
                },
                relations: [{
                    type: backbone.HasMany,
                    //TODO: for some reason when the key is name components, it only maps once
                    //thinking it may be some weird collision with the json dump from the api controller
                    key: 'parts',
                    relatedModel: BodyPartMatrixItem,
                    collectionType: RegionCollection,
                    reverseRelation: {
                        key: 'regionId',
                        includeInJSON: 'id'
                    }
                }]
            });

        return symptomaticRegion;

    });


define('model.body.part.matrix.item', ['backbone'],
    function(backbone) {
        var bodyPartMatrixItem = backbone.RelationalModel.extend({
            
        });

        return bodyPartMatrixItem;
    });

define('model.symptomatic.region.collection', ['backbone', 'model.symptomatic.region', 'config'],
    function (backbone, symptomaticRegion, config) {
        var symptomaticRegionCollection = backbone.Collection.extend({
                model: symptomaticRegion,
                url: config.symptomaticRegions
            });

        return symptomaticRegionCollection;

    });