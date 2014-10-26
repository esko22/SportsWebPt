swptApp.factory('registrationService', ['$resource', 'configService', function ($resource, configService) {


    return {
        validatePatient: function (emailAddress, pin) {
            var resource = $resource(configService.apiUris.registerPatient);
            return resource.save({ emailAddress: emailAddress, pin: pin });
        },
        validateTherapist: function (emailAddress, pin) {
            var resource = $resource(configService.apiUris.registerTherapist);
            return resource.save({ emailAddress: emailAddress, pin: pin });
        }

    }
}]);

