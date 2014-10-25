swptApp.factory('clinicService', ['$resource', 'configService','$http', function ($resource, configService,$http) {

    var clinicPath = configService.apiUris.clinic;

    return {
        get: function (id) {
            var resource = $resource(clinicPath);
            return resource.get({ id: id });
        },
        getManagedClinics: function () {
            var resource = $resource(configService.apiUris.managedClinics);
            return resource.query();
        },
        getClinicTherapists: function (clinicId) {
            var resource = $resource(configService.apiUris.clinicTherapists);
            return resource.query({ id: clinicId});
        },
        getClinicPatients: function (clinicId) {
            var resource = $resource(configService.apiUris.clinicPatients);
            return resource.query({ id: clinicId });
        },
        validateUserByEmail: function(emailAddress) {
            return $http.post(configService.apiUris.validateByEmail, '"' + emailAddress + '"');
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

