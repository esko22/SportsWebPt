define('model.video', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            video = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.videos,
                defaults: {
                }
            });

        return video;

    });

define('model.video.collection', ['backbone', 'model.video'],
    function (backbone, component) {
        var
            componentCollection = backbone.Collection.extend({
                model: component
            });

        return componentCollection;

    });