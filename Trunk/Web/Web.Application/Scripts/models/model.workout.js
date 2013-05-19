define('model.workout', ['backbone', 'config', 'model.exercise', 'model.exercise.collection'],
    function (backbone, config, Exercise, ExerciseCollection) {
        var
            workout = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.workouts,
                idAttribute: 'workoutId',
                relations: [{
                    type: backbone.HasMany,
                    key: 'exercises',
                    relatedModel: Exercise,
                    collectionType: ExerciseCollection
                }]
            });

        return workout;

    });

define('model.workout.collection', ['backbone', 'model.workout'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });