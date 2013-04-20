define('config',['ko','jquery'],
    function (ko, $) {

        var viewIds = {
            header: '#master-header',
            footer: '#master-footer',
            login: '#login-dialog'
        },
        hostUri = 'http://localhost:8022/',
        currentUser = ko.observable();

        var apiUris = {
            users: $.format('{0}/{1}', hostUri, 'users'),
            skeletonAreas: $.format('{0}/{1}', hostUri, 'examine/areas'),
            areaComponents: $.format('{0}/{1}/{2}', hostUri, 'examine/areas', '.id/components')
        };


        return {
            viewIds: viewIds,
            currentUser: currentUser,
            hostUri: hostUri,
            apiUris: apiUris
        };

    });
