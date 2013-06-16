define('config',['ko','jquery','infuser'],
    function (ko, $, infuser) {

        var viewIds = {
            header: '#master-header',
            footer: '#master-footer',
            login: '#login-dialog'
        },
        hostUri = 'http://localhost:8022',
        currentUser = ko.observable();

        var apiUris = {
            users: $.format('{0}/{1}', hostUri, 'users'),
            skeletonAreas: $.format('{0}/{1}', hostUri, 'examine/areas'),
            areaComponents: $.format('{0}/{1}', hostUri, 'examine/components'),
            symptoms: $.format('{0}/{1}', hostUri, 'examine/symptoms'),
            symptomaticRegions: $.format('{0}/{1}', hostUri, 'examine/symptomaticregions'),
            symptomaticComponents: $.format('{0}/{1}', hostUri, 'examine/symptomaticcomponents'),
            componentsByArea: $.format('{0}/{1}', hostUri, 'examine/components?areaId='),
            injuries: $.format('{0}/{1}', hostUri, 'examine/injuries'),
            diagnosisReport: $.format('{0}/{1}', hostUri, 'examine/diagnosisReport'),
            workouts: $.format('{0}/{1}', hostUri, 'research/workouts'),
            exercises: $.format('{0}/{1}', hostUri, 'research/exercises'),
            equipment: $.format('{0}/{1}', hostUri, 'research/equipment'),
            videos: $.format('{0}/{1}', hostUri, 'research/videos'),
            signs: $.format('{0}/{1}', hostUri, 'research/signs'),
            causes: $.format('{0}/{1}', hostUri, 'research/causes')
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
