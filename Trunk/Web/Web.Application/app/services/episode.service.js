swptApp.factory('episodeService', ['$resource', 'configService', function ($resource, configService) {

    var episodePath = configService.apiUris.episodes;

    return {
        get: function (id) {
            var resource = $resource(episodePath);
            return resource.get({ id: id });
        },
        addEpisode: function (episode) {
            return $resource(episodePath).save(episode);
        },
        getSessions: function(id) {
            var resource = $resource(configService.apiUris.episodeSessions);
            return resource.query({ id: id });
        }
    }
}]);