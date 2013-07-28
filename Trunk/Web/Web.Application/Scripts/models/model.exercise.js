define('model.exercise', ['backbone', 'config', 'model.equipment', 'model.video', 'model.equipment.collection', 'model.video.collection', 'model.body.region', 'model.body.region.collection'],
    function (backbone, config, Equipment, Video, EquipmentCollection, VideoCollection, BodyRegion, BodyRegionCollection) {
        var
            exercise = backbone.Model.extend({
                urlRoot: config.apiUris.exercises,
                relations: [
                    {
                        type: backbone.HasMany,
                        key: 'equipment',
                        relatedModel: Equipment,
                        collectionType: EquipmentCollection
                    },
                    {
                        type: backbone.HasMany,
                        key: 'bodyRegions',
                        relatedModel: BodyRegion,
                        collectionType: BodyRegionCollection
                    },
                    {
                        type: backbone.HasMany,
                        key: 'videos',
                        relatedModel: Video,
                        collectionType: VideoCollection
                    }
                ]
            });

        return exercise;

    });



define('model.exercise.collection', ['backbone', 'model.exercise2', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.exercises
            });

        return componentCollection;

    });


define('model.exercise2', ['backbone', 'config', 'model.equipment', 'model.video', 'model.equipment.collection', 'model.video.collection', 'model.body.region', 'model.body.region.collection'],
    function (backbone, config, Equipment, Video, EquipmentCollection, VideoCollection, BodyRegion, BodyRegionCollection) {
        var
            exercise = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.exercises,
                relations: [
                    {
                        type: backbone.HasMany,
                        key: 'equipment',
                        relatedModel: Equipment,
                        collectionType: EquipmentCollection
                    },
                    {
                        type: backbone.HasMany,
                        key: 'bodyRegions',
                        relatedModel: BodyRegion,
                        collectionType: BodyRegionCollection
                    },
                    {
                        type: backbone.HasMany,
                        key: 'videos',
                        relatedModel: Video,
                        collectionType: VideoCollection
                    }
                ]
            });

        return exercise;

    });




define('model.admin.exercise', ['backbone', 'config', 'model.admin.equipment', 'model.admin.video', 'model.admin.equipment.collection', 'model.admin.video.collection', 'model.admin.body.region' , 'model.admin.body.region.collection'],
    function (backbone, config, Equipment, Video, EquipmentCollection, VideoCollection, BodyRegion, BodyRegionCollection) {
        var
            exercise = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminExercises,
                defaults : {
                    'name' : '',
                    'difficulty' : '',
                    'description' : '',
                    'duration': '',
                    'tags': '',
                    'pageName': ''
                },
                relations: [
                    {
                        type: backbone.HasMany,
                        key: 'equipment',
                        relatedModel: Equipment,
                        collectionType: EquipmentCollection
                    },
                    {
                        type: backbone.HasMany,
                        key: 'bodyRegions',
                        relatedModel: BodyRegion,
                        collectionType: BodyRegionCollection
                    },
                    {
                        type: backbone.HasMany,
                        key: 'videos',
                        relatedModel: Video,
                        collectionType: VideoCollection
                    }
                ]
            });

        return exercise;

    });

define('model.admin.exercise.collection', ['backbone', 'model.admin.exercise', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminExercises
            });

        return componentCollection;

    });



define('model.admin.plan.exercise', ['backbone', 'config', 'model.admin.equipment', 'model.admin.video', 'model.admin.equipment.collection', 'model.admin.video.collection', 'model.admin.body.region', 'model.admin.body.region.collection'],
    function (backbone, config, Equipment, Video, EquipmentCollection, VideoCollection, BodyRegion, BodyRegionCollection) {
        var
            exercise = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminExercises,
                defaults: {
                    'name': '',
                    'difficulty': '',
                    'description': '',
                    'duration': '',
                    'tags': '',
                    'pageName': '',
                    'sets': '',
                    'repititions': '',
                    'refExercise': '',
                    'perDay': '',
                    'perWeek': '',
                    'exerciseId' : ''
                },
                relations: [
                    {
                        type: backbone.HasMany,
                        key: 'equipment',
                        relatedModel: Equipment,
                        collectionType: EquipmentCollection
                    },
                    {
                        type: backbone.HasMany,
                        key: 'bodyRegions',
                        relatedModel: BodyRegion,
                        collectionType: BodyRegionCollection
                    },
                    {
                        type: backbone.HasMany,
                        key: 'videos',
                        relatedModel: Video,
                        collectionType: VideoCollection
                    }
                ]
            });

        return exercise;

    });

define('model.admin.plan.exercise.collection', ['backbone', 'model.admin.plan.exercise', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminExercises
            });

        return componentCollection;

    });