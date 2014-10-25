swptApp.service('userManagementService', ['$resource', 'configService', '$state', function ($resource, configService, $state) {
        var resource = $resource(configService.apiUris.currentUser);
        var user = null;

        var getUser = function() {
            if (user == null && isAuthenticated) {
                user = resource.get();
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
            var authTime = $('#authTime').val();

            return authTime !== '';
        };

        var logOut = function() {
            user = null;
            $('#authTime').val(0);
            $resource('/logout').get();
            $state.go('public.splash');
        };



        return {
            getUser: getUser,
            refreshUser : refreshUser,
            isAuthenticated: isAuthenticated,
            logOut: logOut
        };
    }
]);