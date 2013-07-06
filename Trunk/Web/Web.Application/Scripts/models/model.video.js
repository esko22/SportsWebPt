define('model.video', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            video = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.videos,
                defaults: {
                    'name': '',
                    'description': '',
                    'filename': '',
                    'youtubeVideoId': '',
                    'creationDate' : ''
                }
            });

        return video;

    });

define('model.video.collection', ['backbone', 'model.video', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.videos
            });

        return componentCollection;

    });