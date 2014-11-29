swptApp.factory('therapistService', ['$resource', 'configService', function ($resource, configService) {

    var therapistPath = configService.apiUris.therapists;

    return {
        get: function (id) {
            var resource = $resource(therapistPath);
            return resource.get({ id: id });
        },
        getSharedPlansForTherapist: function (planId) {
            var resource = $resource(configService.apiUris.therapistSharedPlans);
            return resource.query({ planId: planId });
        },
        getSharedExercisesForTherapist: function (exerciseId) {
            var resource = $resource(configService.apiUris.therapistSharedExercises);
            return resource.query({exerciseId: exerciseId });
        },
        getCasesForTherapist: function (state) {
            var resource = $resource(configService.apiUris.therapistCases);
            return resource.query({ state: state });
        },
        updateSharedPlans: function (sharedPlans) {
            var resource = $resource(configService.apiUris.therapistSharedPlans, null, { 'update': { method: 'PUT' } });
            return resource.update(sharedPlans);
        },
        updateSharedExercises: function (sharedExercises) {
            var resource = $resource(configService.apiUris.therapistSharedExercises, null, { 'update': { method: 'PUT' } });
            return resource.update(sharedExercises);
        }
    }
}]);

