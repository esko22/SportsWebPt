define('model.plan', ['backbone', 'config', 'model.exercise', 'model.exercise.collection'],
    function (backbone, config, Exercise, ExerciseCollection) {
        var
            plan = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.plans,
                idAttribute: 'id',
                relations: [{
                    type: backbone.HasMany,
                    key: 'exercises',
                    relatedModel: Exercise,
                    collectionType: ExerciseCollection
                }]
            });

        return plan;

    });

define('model.plan.collection', ['backbone', 'model.plan'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });