define('model.symptomatic.region', ['backbone', 'config', 'model.symptomatic.region.component', 'model.symptomatic.region.collection'],
    function (backbone, config, RegionComponent, RegionCollection) {
        var symptomaticRegion = backbone.RelationalModel.extend({
                urlRoot: config.symptomaticRegions,
                defaults: {
                },
                relations: [{
                    type: backbone.HasMany,
                    //TODO: for some reason when the key is name components, it only maps once
                    //thinking it may be some weird collision with the json dump from the api controller
                    key: 'regionComponents',
                    relatedModel: RegionComponent,
                    collectionType: RegionCollection,
                    reverseRelation: {
                        key: 'regionId',
                        includeInJSON: 'id'
                    }
                }]
            });

        return symptomaticRegion;

    });


define('model.symptomatic.region.component', ['backbone'],
    function(backbone) {
        var regionComponent = backbone.RelationalModel.extend({
            
        });

        return regionComponent;
    });

define('model.symptomatic.region.collection', ['backbone', 'model.symptomatic.region'],
    function (backbone, symptomaticRegion) {
        var
            symptomaticRegionCollection = backbone.Collection.extend({
                model: symptomaticRegion
            });

        return symptomaticRegionCollection;

    });