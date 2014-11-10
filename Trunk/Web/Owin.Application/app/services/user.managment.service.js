swptApp.service('userManagementService', ['$resource', 'configService', '$state', '$window', function ($resource, configService, $rootScope, $window) {
        var resource = $resource(configService.apiUris.currentUser);

        var getUser = function() {
            return resource.get();
        };
     
        return {
            getUser: getUser,
            refreshUser: getUser
        };
    }
]);

swptApp.service('authenticationService', ['$window', function ($window) {

    var isAuthenticated = function () {
        return $window.localStorage.getItem('access_token') !== null;
    };

    var signIn = function (redirectUri) {
        $window.localStorage.removeItem('access_token');
        $window.sessionStorage.redirectUrl = redirectUri;
        var uri = URI('http://localhost:3333/core/connect/authorize')
           .addSearch('response_type', 'token')
           .addSearch('client_id', 'implicitclient')
           .addSearch('scope', 'user_detail')
           .addSearch('redirect_uri', 'http://localhost:8022/auth')
           .addSearch('nonce', Math.floor(Math.random() * 99999));
        $window.location.href = uri;
    }

    var signOut = function () {
        var uri = URI('http://localhost:3333/core/logout')
            .addSearch('post_logout_redirect_uri', 'http://localhost:8022');
        $window.location.href = uri;
        $window.localStorage.removeItem('access_token');
    }

    return {
        signOut: signOut,
        signIn: signIn,
        isAuthenticated: isAuthenticated
    };
}
]);