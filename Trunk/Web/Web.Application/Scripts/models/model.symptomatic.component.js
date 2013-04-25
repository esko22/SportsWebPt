define('model.symptomatic.component', ['backbone', 'config', 'model.symptomatic.region.component', 'model.symptomatic.component.symptom'],
    function(backbone, config, RegionComponent, ComponentSymptom) {

        var symptomaticComponent = backbone.RelationalModel.extend({
            urlRoot: config.symptomaticComponents,
            defaults: {
                
            },
            relations: [{
                    type: backbone.HasMany,
                    key: 'symptoms',
                    relatedModel: ComponentSymptom,
                    reverseRelation: {
                        key: 'componentId',
                        includeInJSON: 'id'
                    }
                },
                {
                    type: backbone.HasMany,
                    key: 'regions',
                    relatedModel: RegionComponent,
                    reverseRelation: {
                        key: 'componentId',
                        includeInJSON: 'id'
                    }
                }
            ]
        });

        return symptomaticComponent;
    });

define('model.symptomatic.component.symptom', ['backbone'],
    function(backbone) {
        var componentSymptom = backbone.RelationalModel.extend({
            
        });

        return componentSymptom;
    });

define('model.symptomatic.component.collection', ['backbone', 'model.symptomatic.component'],
    function (backbone, symptomaticComponent) {
        var
            symptomaticComponentCollection = backbone.Collection.extend({
                model: symptomaticComponent
            });

        return symptomaticComponentCollection;

    });