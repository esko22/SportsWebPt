swptApp.factory('clinicService', ['$resource', 'configService', function ($resource, configService) {

    var clinicPath = configService.apiUris.clinic;

    return {
        get: function (id) {
            var resource = $resource(clinicPath);
            return resource.get({ id: id });
        },
        getManagedClinics: function (managerId) {
            var resource = $resource(configService.apiUris.managedClinics);
            return resource.query({ id: managerId});
        },
        getClinicTherapists: function (clinicId) {
            var resource = $resource(configService.apiUris.clinicTherapists);
            return resource.query({ id: clinicId});
        },
        getClinicPatients: function (clinicId) {
            var resource = $resource(configService.apiUris.clinicPatients);
            return resource.query({ id: clinicId });
        }
    }
}]);

