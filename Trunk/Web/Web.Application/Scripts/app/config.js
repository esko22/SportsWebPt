define('config',['ko','jquery','infuser'],
    function (ko, $, infuser) {

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
            areaComponents: $.format('{0}/{1}', hostUri, 'examine/components'),
            componentsByArea: $.format('{0}/{1}', hostUri, 'examine/components?areaId=')
        };

        var configureExternalTemplates = function() {
            infuser.defaults.templatePrefix = "_";
            infuser.defaults.templateSuffix = ".tmpl.html";
            infuser.defaults.templateUrl = "/Tmpl";
        };

        var init = function () {
            configureExternalTemplates();
        };

        init();

        return {
            viewIds: viewIds,
            currentUser: currentUser,
            hostUri: hostUri,
            apiUris: apiUris
        };

    });
