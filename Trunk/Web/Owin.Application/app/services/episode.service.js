swptApp.factory('caseService', ['$resource', 'configService', function ($resource, configService) {

    var casePath = configService.apiUris.cases;

    return {
        get: function (id) {
            var resource = $resource(casePath);
            return resource.get({ id: id });
        },
        addCase: function (caseInstance) {
            return $resource(casePath).save(caseInstance);
        },
        getSessions: function(id) {
            var resource = $resource(configService.apiUris.caseSessions);
            return resource.query({ id: id });
        }
    }
}]);