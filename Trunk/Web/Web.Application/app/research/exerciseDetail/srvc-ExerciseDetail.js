swptApp.factory('exerciseDetailService', function ($resource, $q, configService) {
    var resource = $resource(configService.apiUris.exerciseDetail + ':id', { id: '@id' });
    return {
        getExercise: function (exerciseId) {
            var deferred = $q.defer();
            resource.get({ id: exerciseId },
                function (exercise) {
                    deferred.resolve(exercise);
                },
                function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        }
    };
})