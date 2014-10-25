swptApp.service('userManagementService', ['$resource', 'configService', '$state', function ($resource, configService, $rootScope) {
        var resource = $resource(configService.apiUris.currentUser);

        var getUser = function() {
            return resource.get();
        };

        var isAuthenticated = function() {
            var authTime = $('#authTime').val();
            return authTime > 0;
        };

        var logOut = function () {
            window.location.assign('/signout');
        };

        return {
            getUser: getUser,
            refreshUser: getUser,
            isAuthenticated: isAuthenticated,
            logOut: logOut
        };
    }
]);