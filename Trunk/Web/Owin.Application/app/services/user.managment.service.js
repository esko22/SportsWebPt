swptApp.service('userManagementService', ['$resource', 'configService', '$state', function ($resource, configService, $state) {
        var resource = $resource(configService.apiUris.users);
        var user = null;

        var getUser = function() {
            var userId = $('#authTime').val();

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
            var userId = $('#authTime').val();

            return userId > 0;
        };

        var logOut = function() {
            user = null;
            window.location.assign('/signout');
        };



        return {
            getUser: getUser,
            refreshUser : refreshUser,
            isAuthenticated: isAuthenticated,
            logOut: logOut
        };
    }
]);