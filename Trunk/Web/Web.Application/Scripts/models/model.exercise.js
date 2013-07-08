define('model.exercise', ['backbone', 'config', 'model.equipment', 'model.video', 'model.equipment.collection', 'model.video.collection'],
    function (backbone, config, Equipment, Video, EquipmentCollection, VideoCollection) {
        var
            exercise = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.exercises,
                idAttribute: 'exerciseId',
                relations: [
                    {
                        type: backbone.HasMany,
                        key: 'equipment',
                        relatedModel: Equipment,
                        collectionType: EquipmentCollection
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

define('model.exercise.collection', ['backbone', 'model.exercise'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });


define('model.admin.exercise', ['backbone', 'config', 'model.admin.equipment', 'model.admin.video', 'model.admin.equipment.collection', 'model.admin.video.collection'],
    function (backbone, config, Equipment, Video, EquipmentCollection, VideoCollection) {
        var
            exercise = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminExercises,
                idAttribute: 'exerciseId',
                relations: [
                    {
                        type: backbone.HasMany,
                        key: 'equipment',
                        relatedModel: Equipment,
                        collectionType: EquipmentCollection
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