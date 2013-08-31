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

define('model.video.collection', ['backbone', 'model.video', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.videos
            });

        return componentCollection;

    });



define('model.admin.video', ['backbone', 'config', 'jquery'],
    function (backbone, config, $) {
        var
            video = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminVideos,
                defaults: {
                    'name': '',
                    'medicalName': '',
                    'description': '',
                    'filename': '',
                    'youtubeVideoId': '',
                    'creationDate': '',
                    'category' : ''
                }
            });

        return video;

    });

define('model.admin.video.collection', ['backbone', 'model.admin.video', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminVideos
            });

        return componentCollection;

    });