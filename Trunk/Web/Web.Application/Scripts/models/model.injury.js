define('model.injury', ['backbone', 'config', 'model.workout', 'model.cause', 'model.sign', 'model.potential.symptom'],
    function (backbone, config, Workout, Cause, Sign, Symptom) {
        var
            injury = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.injuries,
                relations: [{
                    type: backbone.HasMany,
                    key: 'workouts',
                    relatedModel: Workout
                },
                {
                    type: backbone.HasMany,
                    key: 'causes',
                    relatedModel: Cause
                },
                {
                    type: backbone.HasMany,
                    key: 'signs',
                    relatedModel: Sign
                },
                {
                    type: backbone.HasMany,
                    key: 'givenSymptoms',
                    relatedModel: Symptom
                }]
            });

        return injury;

    });

define('model.injury.collection', ['backbone', 'model.injury'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });