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
            symptomaticRegions: $.format('{0}/{1}', hostUri, 'examine/symptomaticregions'),
            symptomaticComponents: $.format('{0}/{1}', hostUri, 'examine/symptomaticcomponents'),
            componentsByArea: $.format('{0}/{1}', hostUri, 'examine/components?areaId='),
            diagnosisReport: $.format('{0}/{1}', hostUri, 'examine/diagnosisReport'),
            injuries: $.format('{0}/{1}', hostUri, 'research/injuries'),
            adminInjuries: $.format('{0}/{1}', hostUri, 'admin/injuries'),
            plans: $.format('{0}/{1}', hostUri, 'research/plans'),
            adminPlans: $.format('{0}/{1}', hostUri, 'admin/plans'),
            exercises: $.format('{0}/{1}', hostUri, 'research/exercises'),
            adminExercises: $.format('{0}/{1}', hostUri, 'admin/exercises'),
            equipment: $.format('{0}/{1}', hostUri, 'research/equipment'),
            adminEquipment: $.format('{0}/{1}', hostUri, 'admin/equipment'),
            videos: $.format('{0}/{1}', hostUri, 'research/videos'),
            adminVideos: $.format('{0}/{1}', hostUri, 'admin/videos'),
            signs: $.format('{0}/{1}', hostUri, 'research/signs'),
            causes: $.format('{0}/{1}', hostUri, 'research/causes'),
            symptoms: $.format('{0}/{1}', hostUri, 'research/symptoms'),
            bodyParts: $.format('{0}/{1}', hostUri, 'research/bodyparts'),
            bodyRegion: $.format('{0}/{1}', hostUri, 'research/bodyregions'),
            adminBodyRegion: $.format('{0}/{1}', hostUri, 'admin/bodyregions'),
            adminBodyParts: $.format('{0}/{1}', hostUri, 'admin/bodyparts')
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
