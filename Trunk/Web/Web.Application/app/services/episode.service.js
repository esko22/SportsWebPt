swptApp.factory('episodeService', ['$resource', 'configService', function ($resource, configService) {

    var episodePath = configService.apiUris.episodes;

    return {
        get: function (id) {
            var resource = $resource(episodePath);
            return resource.get({ id: id });
        }
    }
}]);