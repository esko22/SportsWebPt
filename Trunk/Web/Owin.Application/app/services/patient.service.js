swptApp.factory('patientService', ['$resource', 'configService', function ($resource, configService) {

    var patientPath = configService.apiUris.patients;

    return {
        get: function (id) {
            var resource = $resource(patientPath);
            return resource.get({ id: id });
        },
        getEpisodesForPatient: function (state) {
            var resource = $resource(configService.apiUris.patientEpisodes);
            return resource.query({state: state });
        }
    }
}]);

