define('model.admin.body.part.matrix.item', ['backbone', 'config', 'model.body.part', 'model.skeleton.area'],
    function (backbone, config, BodyPart, SkeletonArea) {
        var
            bodyPartMatrixItem = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminBodyPartMatrix,
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

define('model.admin.body.part.matrix.item.collection', ['backbone', 'model.admin.body.part.matrix.item', 'config'],
    function (backbone, matrixItem, config) {
        var
            adminMatrixCollection = backbone.Collection.extend({
                model: matrixItem,
                url: config.apiUris.adminBodyPartMatrix
            });

        return adminMatrixCollection;

    });