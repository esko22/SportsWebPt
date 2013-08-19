define('model.body.part', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            area = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.areaComponents,
                defaults: {
                }
            });

        return area;

    });

define('model.body.part.collection', ['backbone', 'model.body.part'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });

define('model.admin.body.part', ['backbone', 'config', 'jquery', 'model.admin.skeleton.area'],
    function (backbone, config, $, SkeletonArea) {
        var
            area = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminBodyParts,
                defaults: {
                    'commonName' : '', 
                    'scientificName': ''
                },
                relations: [{
                    type: backbone.HasMany,
                    key: 'primaryAreas',
                    relatedModel: SkeletonArea
                }, {
                    type: backbone.HasMany,
                    key: 'secondaryAreas',
                    relatedModel: SkeletonArea
                }]
            });

        return area;

    });

define('model.admin.body.part.collection', ['backbone', 'model.admin.body.part', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminBodyParts,
            });

        return componentCollection;

    });