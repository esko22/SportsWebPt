define('model.symptomatic.component', ['backbone', 'config', 'model.symptom'],
    function (backbone, config, symptom) {
        var
            symptomaticComponent = backbone.RelationalModel.extend({
                urlRoot: config.symptomaticComponents,
                defaults: {
                },
                relations: [{
                    type: backbone.HasMany,
                    key: 'symptoms',
                    relatedModel: new symptom(),
                    reverseRelation: {
                        key: 'componentId',
                        includeInJSON: 'id'
                    }
                }]
            });

        return symptomaticComponent;

    });

define('model.symptomatic.component.collection', ['backbone', 'model.symptomatic.component'],
    function (backbone, symptomaticComponent) {
        var
            symptomaticComponentCollection = backbone.Collection.extend({
                model: symptomaticComponent
            });

        return symptomaticComponentCollection;

    });