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