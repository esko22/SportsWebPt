swptApp.factory('therapistService', ['$resource', 'configService', function ($resource, configService) {

    var therapistPath = configService.apiUris.therapists;

    return {
        get: function (id) {
            var resource = $resource(therapistPath);
            return resource.get({ id: id });
        },
        getSharedPlansForTherapist: function (therapistId, planId) {
            var resource = $resource(configService.apiUris.therapistSharedPlans);
            return resource.query({ id: therapistId, planId: planId });
        },
        getSharedExercisesForTherapist: function (therapistId, exerciseId) {
            var resource = $resource(configService.apiUris.therapistSharedExercises);
            return resource.query({ id: therapistId, exerciseId: exerciseId });
        },
        getEpisodesForTherapist: function (therapistId, state) {
            var resource = $resource(configService.apiUris.therapistEpisodes);
            return resource.query({ id: therapistId, state: state });
        },
        updateSharedPlans: function (therapistId, sharedPlans) {
            var resource = $resource(configService.apiUris.therapistSharedPlans, null, { 'update': { method: 'PUT' } });
            return resource.update({ id: therapistId }, sharedPlans);
        },
        updateSharedExercises: function (therapistId, sharedExercises) {
            var resource = $resource(configService.apiUris.therapistSharedExercises, null, { 'update': { method: 'PUT' } });
            return resource.update({ id: therapistId }, sharedExercises);
        }
    }
}]);

