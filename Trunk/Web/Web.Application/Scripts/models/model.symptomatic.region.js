define('model.symptomatic.region', ['backbone', 'config', 'model.symptomatic.component'],
    function (backbone, config, symptomaticComponent) {
        var
            symptomaticRegion = backbone.RelationalModel.extend({
                urlRoot: config.symptomaticRegions,
                defaults: {
                },
                relations: [{
                    type: backbone.HasMany,
                    key: 'components',
                    relatedModel: new symptomaticComponent(),
                    reverseRelation: {
                        key: 'regionId',
                        includeInJSON: 'id'
                    }
                }]
            });

        return symptomaticRegion;

    });

define('model.symptomatic.region.collection', ['backbone', 'model.symptomatic.region'],
    function (backbone, symptomaticRegion) {
        var
            symptomaticRegionCollection = backbone.Collection.extend({
                model: symptomaticRegion
            });

        return symptomaticRegionCollection;

    });