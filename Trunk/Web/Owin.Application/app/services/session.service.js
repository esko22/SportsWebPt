swptApp.factory('sessionService', [
    '$resource', 'configService', '$http', function($resource, configService) {

        var sessionsPath = configService.apiUris.sessions;
        
        return {
            get: function(id) {
                var resource = $resource(sessionsPath);
                return resource.get({ id: id });
            },
            getAsTherapist: function (id) {
                var resource = $resource(configService.apiUris.sessionsAsTherapist);
                return resource.get({ id: id });
            },
            addSession: function (session) {
                return $resource(sessionsPath).save(session);
            },
            setSessionPlanList: function(sessionId, planIds) {
                return $resource(configService.apiUris.sessionPlans).save({ id: sessionId }, planIds);
            },
            startSessionPay: function (id) {
                var resource = $resource(sessionsPath + '/pay');
                return resource.get({ id: id });
            },
            update: function (session) {
                var resource = $resource(sessionsPath, null, {
                    'update': { method: 'PUT' }
                });

                return resource.update({ id: session.id }, session);
            }
        }

    }
]);