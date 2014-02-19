define('model.plan', ['backbone', 'config', 'model.plan.exercise', 'model.exercise.collection', 'model.body.region', 'model.body.region.collection'],
    function (backbone, config, Exercise, ExerciseCollection, BodyRegion, BodyRegionCollection) {
        var
            plan = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.plans,
                idAttribute: 'id',
                defaults: {
                    'id': '',
                    'routineName': '',
                    'category': '',
                    'description': '',
                    'instructions': '',
                    'musclesInvolved': '',
                    'duration': '',
                    'animationTag' : ''
                },
                relations: [{
                    type: backbone.HasMany,
                    key: 'exercises',
                    relatedModel: Exercise,
                    collectionType: ExerciseCollection
                },
                {
                    type: backbone.HasMany,
                    key: 'bodyRegions',
                    relatedModel: BodyRegion,
                    collectionType: BodyRegionCollection
                }]
            });

        return plan;

    });

define('model.injury.plan', ['backbone', 'config', 'model.plan.exercise'],
    function (backbone, config, Exercise) {
        var
            plan = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.plans,
                idAttribute: 'id',
                relations: [{
                    type: backbone.HasMany,
                    key: 'exercises',
                    relatedModel: Exercise
                }]
            });

        return plan;

    });


define('model.plan.collection', ['backbone', 'model.plan', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.plans
            });

        return componentCollection;

    });

define('model.admin.plan', ['backbone', 'config', 'model.admin.plan.exercise', 'model.admin.body.region'],
    function (backbone, config, Exercise, BodyRegion) {
        var
            plan = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminPlans,
                idAttribute: 'id',
                relations: [{
                    type: backbone.HasMany,
                    key: 'exercises',
                    relatedModel: Exercise
                },
                {
                    type: backbone.HasMany,
                    key: 'bodyRegions',
                    relatedModel: BodyRegion
                }]
            });

        return plan;

    });

define('model.admin.plan.collection', ['backbone', 'model.admin.plan', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminPlans
            });

        return componentCollection;

    });


define('model.admin.injury.plan', ['backbone', 'config'],
    function (backbone, config) {
        var
            plan = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminPlans,
                idAttribute: 'id',
            });

        return plan;

    });

define('model.admin.injury.plan.collection', ['backbone', 'model.admin.injury.plan', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminPlans
            });

        return componentCollection;

    });
