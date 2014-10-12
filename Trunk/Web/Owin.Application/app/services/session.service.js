swptApp.factory('sessionService', [
    '$resource', 'configService', function($resource, configService) {

        var sessionsPath = configService.apiUris.sessions;

        return {
            get: function(id) {
                var resource = $resource(sessionsPath);
                return resource.get({ id: id });
            },
            addSession: function(session) {
                return $resource(sessionsPath).save(session);
            },
            addSessionPlans: function(sessionId, planIds) {
                return $resource(configService.apiUris.sessionPlans).save({ id: sessionId }, planIds);
            }
        }

    }
]);