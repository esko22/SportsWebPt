swptApp.factory('patientService', ['$resource', 'configService', function ($resource, configService) {

    var patientPath = configService.apiUris.patients;

    return {
        get: function (id) {
            var resource = $resource(patientPath);
            return resource.get({ id: id });
        },
        getPatientSnapshot: function() {
            var resource = $resource(configService.apiUris.patientSnapshot);
            return resource.get();
        },
        getCasesForPatient: function (state) {
            var resource = $resource(configService.apiUris.patientCases);
            return resource.query({state: state });
        }
    }
}]);

