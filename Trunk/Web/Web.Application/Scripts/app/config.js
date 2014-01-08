define('config',['ko','jquery','infuser', 'toastr','knockback'],
    function (ko, $, infuser, toastr, kb) {

        var viewIds = {
            header: '#master-header',
            footer: '#master-footer',
            login: '#login-dialog',
            masterContainer: '#master-container',
            mainSplash: '#main-splash-panel',
            examineSkeleton: '#examine-skeleton-pane',
            examineContent: '#examine-content',
            examineDetail: '#examine-detail-pane',
            examineReport: '#examine-report-pane',
            researchNav: '#research-nav-pane',
            researchInjury: '#research-injury-pane',
            researchPlan: '#research-plan-pane',
            researchExercise: '#research-exercise-pane',
            researchLocate: '#research-locate-pane',
            researchInjuryDetail: '#research-injury-detail-pane',
            researchPlanDetail: '#research-plan-detail-pane',
            researchExerciseDetail: '#research-exercise-detail-pane'
        },
            templateIds = {
                injuryTemplate : 'examine.report.injury',
                researchInjuryPlanTemplate: 'research.injury.plan',
                exerciseTemplate : 'exercise',
                sublimeYoutubeVideoTemplate: 'sublime.youtube.video'
            },
            favoriteUri = 'http://sportswebpt.com',
            favoriteHashTags = {
                injuryHash: '#/research/injuries',
                planHash: '#/research/plans',
                exerciseHash: '#/research/exercises'
            },
            hostUri = '',
            maxSelectableAreas = ko.observable(2),
            notifier = toastr,
            apiUris = {
                users: $.format('{0}/{1}', hostUri, 'users'),
                favorites: $.format('{0}/{1}', hostUri, 'users/favorites'),
                skeletonAreas: $.format('{0}/{1}', hostUri, 'examine/areas'),
                symptomaticRegions: $.format('{0}/{1}', hostUri, 'examine/symptomaticregions'),
                potentialSymptoms: $.format('{0}/{1}', hostUri, 'examine/potentialSymptoms'),
                componentsByArea: $.format('{0}/{1}', hostUri, 'examine/components?areaId='),
                diagnosisReport: $.format('{0}/{1}', hostUri, 'examine/diagnosisReport'),
                injuries: $.format('{0}/{1}', hostUri, 'research/injuries'),
                adminInjuries: $.format('{0}/{1}', hostUri, 'admin/injuries'),
                plans: $.format('{0}/{1}', hostUri, 'data/plans'),
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
                clinics: $.format('{0}/{1}', hostUri, 'research/locate/{zipcode}'),
                injuryDetail: $.format('{0}/{1}', hostUri, 'research/injury/detail/'),
                planDetail: $.format('{0}/{1}', hostUri, 'research/plan/detail/'),
                exerciseDetail: $.format('{0}/{1}', hostUri, 'research/exercise/detail/'),
                signFilters: $.format('{0}/{1}', hostUri, 'lookup/signfilters')
            },
            likelyHoodThresholds = {
                high: .80,
                medium: .40
            },
            configureExternalTemplates = function() {
                infuser.defaults.templatePrefix = "_";
                infuser.defaults.templateSuffix = ".tmpl.html";
                infuser.defaults.templateUrl = "/Tmpl";
            },
            functionCategories = ko.observableArray(['Rehabilitation', 'Stretching', 'Preventative', 'Spinal Stabilization', 'Strengthing', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization']),
            regionCategories = ko.observableArray(['UpperExtremity', 'LowerExtremity', 'Spine']),
            signGroups = ko.observableArray(['Painful','Throbbing','Bruising','Popping','Tingling','Tightness']),
            holdTypes = ko.observableArray(['Seconds', 'Minutes', 'Breaths']),
            kendoEditorOptions = [
                "bold",
                "italic",
                "underline",
                "strikethrough",
                "justifyLeft",
                "justifyCenter",
                "justifyRight",
                "justifyFull",
                "insertUnorderedList",
                "insertOrderedList",
                "indent",
                "outdent",
                "createLink",
                "unlink",
                "insertImage",
                "subscript",
                "superscript",
                "createTable",
                "addRowAbove",
                "addRowBelow",
                "addColumnLeft",
                "addColumnRight",
                "deleteRow",
                "deleteColumn",
                "viewHtml",
                "formatting",
                "fontName",
                "fontSize",
                "foreColor",
                "backColor"
            ],
            kendoEditorIgnores = ".k-input, .k-fontName, .k-fontSize, k-formatBlock,:hidden.k-formatBlock ";
        
            function init() {
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
            hostUri: hostUri,
            apiUris: apiUris,
            functionCategories: functionCategories,
            regionCategories: regionCategories,
            holdTypes: holdTypes,
            maxSelectableAreas: maxSelectableAreas,
            notifier: notifier,
            kendoEditorIgnores: kendoEditorIgnores,
            kendoEditorOptions: kendoEditorOptions,
            likelyHoodThresholds: likelyHoodThresholds,
            templateIds: templateIds,
            favoriteUri: favoriteUri,
            favoriteHashTags: favoriteHashTags,
            signGroups : signGroups
        };

    });
