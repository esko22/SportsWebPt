define('model.injury', ['backbone', 'config', 'model.workout'],
    function (backbone, config, Workout) {
        var
            injury = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.injuries,
                relations: [{
                    type: backbone.HasMany,
                    key: 'workouts',
                    relatedModel: Workout
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