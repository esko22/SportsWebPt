define('model.potential.symptom', ['backbone', 'config', 'model.symptom.matrix.item'],
    function (backbone, config, SymptomMatrixItem) {
        
            var potentialSymptom = backbone.RelationalModel.extend({
                urlRoot: config.symptoms,
                idAttribute: 'id',
                relations: [{
                    type: backbone.HasOne,
                    key: 'bodyPart',
                    relatedModel: SymptomMatrixItem
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