swptApp.service('userManagementService', ['$resource', 'configService', function ($resource, configService) {
        var resource = $resource(configService.apiUris.users + '/:id', { id: '@id' });


        var getUser = function() {
            var userId = $('#userid').val();

            if (userId > 0) {
                return resource.get({ id: userId });
            }
            else {
                return null;
            }
        };

        var isAuthenticated = function() {
            var userId = $('#userid').val();

            return userId > 0;
        };



        return {
            getUser: getUser,
            isAuthenticated: isAuthenticated
        };
    }
]);