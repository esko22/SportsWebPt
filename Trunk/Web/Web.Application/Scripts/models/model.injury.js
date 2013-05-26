define('model.injury', ['backbone', 'config', 'model.workout', 'model.cause', 'model.sign'],
    function (backbone, config, Workout, Cause, Sign) {
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