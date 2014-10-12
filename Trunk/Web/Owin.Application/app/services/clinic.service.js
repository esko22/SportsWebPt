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
        },
        getUserByEmail: function(emailAddress) {
            var resource = $resource(configService.apiUris.users);
            return resource.get({ id: emailAddress });
        },
        addPatientToClinic: function(clinicId, user) {
            var resource = $resource(configService.apiUris.clinicPatients);
            return resource.save({ id: clinicId }, user);
        },
        addTherapistToClinic: function(clinicId, therapist) {
            var resource = $resource(configService.apiUris.clinicTherapists);
            return resource.save({ id: clinicId }, therapist);
        }

    }
}]);

