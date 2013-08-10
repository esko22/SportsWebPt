define('model.body.part.matrix.item', ['backbone', 'config', 'model.body.part', 'model.skeleton.area'],
    function (backbone, config, BodyPart, SkeletonArea) {
        var
            bodyPartMatrixItem = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.bodyPartMatrixItems,
                relations: [
                    {
                        type: backbone.HasOne,
                        key: 'bodyPart',
                        relatedModel: BodyPart,
                    },
                    {
                        type: backbone.HasOne,
                        key: 'skeletonArea',
                        relatedModel: SkeletonArea,
                    }
                ]
            });

        return bodyPartMatrixItem;

    });