swptApp.service('userManagementService', ['$resource', 'configService', '$rootScope', function ($resource, configService, $rootScope) {
        var resource = $resource(configService.apiUris.currentUser);

        var getUser = function() {
            return resource.get();
        };

        var refreshUser = function() {
            getUser().$promise.then(function(user) {
                $rootScope.currentUser = user;
            });
        }
     
        return {
            getUser: getUser,
            refreshUser: refreshUser
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
        var uri = URI(SportsWebPt.Config.AuthorityUrl + '/connect/authorize')
           .addSearch('response_type', 'token')
           .addSearch('client_id', '94CDBD19-3B0D-4D64-980C-6DC68D26B31B')
           .addSearch('scope', 'user_detail')
           .addSearch('redirect_uri', SportsWebPt.Config.AuthRedirectPath)
           .addSearch('nonce', Math.floor(Math.random() * 99999));
        $window.location.href = uri;
    }

    var signOut = function () {
        var uri = URI(SportsWebPt.Config.AuthorityUrl +'/logout');
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