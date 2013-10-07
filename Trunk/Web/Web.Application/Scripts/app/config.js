define('config',['ko','jquery','infuser', 'toastr'],
    function (ko, $, infuser, toastr) {

        var viewIds = {
            header: '#master-header',
            footer: '#master-footer',
            login: '#login-dialog'
        },
            hostUri = '',
            currentUser = ko.observable(),
            maxSelectableAreas = ko.observable(2),
            notifier = toastr;

        var apiUris = {
            users: $.format('{0}/{1}', hostUri, 'users'),
            skeletonAreas: $.format('{0}/{1}', hostUri, 'examine/areas'),
            symptomaticRegions: $.format('{0}/{1}', hostUri, 'examine/symptomaticregions'),
            potentialSymptoms: $.format('{0}/{1}', hostUri, 'examine/potentialSymptoms'),
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
            adminSigns: $.format('{0}/{1}', hostUri, 'admin/signs'),
            adminCauses: $.format('{0}/{1}', hostUri, 'admin/causes'),
            symptoms: $.format('{0}/{1}', hostUri, 'research/symptoms'),
            bodyParts: $.format('{0}/{1}', hostUri, 'research/bodyparts'),
            bodyRegion: $.format('{0}/{1}', hostUri, 'research/bodyregions'),
            adminBodyRegion: $.format('{0}/{1}', hostUri, 'admin/bodyregions'),
            adminBodyParts: $.format('{0}/{1}', hostUri, 'admin/bodyparts'),
            adminSymptoms: $.format('{0}/{1}', hostUri, 'admin/symptoms'),
            adminBodyPartMatrix: $.format('{0}/{1}', hostUri, 'admin/bodypartmatrix'),
            adminSkeletonAreas: $.format('{0}/{1}', hostUri, 'admin/skeletonareas'),
            clinics: $.format('{0}/{1}', hostUri, 'research/locate/{zipcode}')
        };

        var configureExternalTemplates = function() {
            infuser.defaults.templatePrefix = "_";
            infuser.defaults.templateSuffix = ".tmpl.html";
            infuser.defaults.templateUrl = "/Tmpl";
        };

        var functionCategories = ko.observableArray(['Rehabilitation', 'Stretching', 'Preventative', 'Spinal Stabilization', 'Strengthing', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization']);
        var regionCategories = ko.observableArray(['UpperExtremity', 'LowerExtremity', 'Spine']);
        var holdTypes = ko.observableArray(['Seconds', 'Minutes', 'Breaths']);
        var kendoEditorIgnores = ".k-input, .k-fontName, .k-fontSize, k-formatBlock,:hidden.k-formatBlock ";
        var init = function () {
            configureExternalTemplates();
            notifier.options = {
                "closeButton": false,
                "debug": false,
                "positionClass": "toast-center",
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };

        };

        init();

        return {
            viewIds: viewIds,
            currentUser: currentUser,
            hostUri: hostUri,
            apiUris: apiUris,
            functionCategories: functionCategories,
            regionCategories: regionCategories,
            holdTypes: holdTypes,
            maxSelectableAreas: maxSelectableAreas,
            notifier: notifier,
            kendoEditorIgnores: kendoEditorIgnores
        };

    });
