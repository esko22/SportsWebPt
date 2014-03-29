swptApp.service('userManagementService', ['$resource', 'configService', function ($resource, configService) {
        var resource = $resource(configService.apiUris.users + '/:id', { id: '@id' });
        var user = null;

        var getUser = function() {
            var userId = $('#userid').val();

            if (user == null && userId > 0) {
                user = resource.get({ id: userId });
                return user;
            }
            else {
                return user;
            }
        };

        var refreshUser = function() {
            user = resource.get({ id: user.id });
        }

        var isAuthenticated = function() {
            var userId = $('#userid').val();

            return userId > 0;
        };



        return {
            getUser: getUser,
            refreshUser : refreshUser,
            isAuthenticated: isAuthenticated
        };
    }
]);