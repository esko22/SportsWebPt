define('model.potential.symptom', ['backbone', 'config', 'model.symptomatic.component.symptom'],
    function (backbone, config, ComponentSymptom) {
        
            var potentialSymptom = backbone.RelationalModel.extend({
                urlRoot: config.symptoms,
                idAttribute: 'id',
                relations: [{
                    type: backbone.HasMany,
                    key: 'components',
                    relatedModel: ComponentSymptom,
                    reverseRelation: {
                        key: 'symptomId',
                        includeInJSON: 'id'
                    }
                }]

            });

            return potentialSymptom;
    })

//define('model.symptom.collection', ['backbone', 'model.symptom'],
//    function (backbone, symptom) {
//        var
//            symptomCollection = backbone.Collection.extend({
//                model: symptom
//            });

//        return symptomCollection;

//    });