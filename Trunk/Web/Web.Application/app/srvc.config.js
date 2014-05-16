
swptApp.service('configService', ['$resource', '$location', function ($resource, $location) {
        var apiUris = {
            users: 'users',
            favorites: 'users/favorites',
            skeletonAreas: 'examine/areas',
            symptomaticRegions: 'examine/symptomaticregions',
            potentialSymptoms: 'examine/potentialSymptoms',
            componentsByArea: 'examine/components?areaId=',
            diagnosisReport: 'examine/diagnosisReport',
            injuries: 'research/injuries',
            briefInjuries: 'data/injuries/brief',
            adminInjuries: 'admin/injuries',
            plans: 'research/plans',
            briefPlans: 'data/plans/brief',
            adminPlans: 'admin/plans',
            exercises: 'research/exercises',
            briefExercises: 'data/exercises/brief',
            adminExercises: 'admin/exercises',
            equipment: 'research/equipment',
            briefEquipment: 'research/equipment/brief',
            adminEquipment: 'admin/equipment',
            videos: 'research/videos',
            adminVideos: 'admin/videos',
            signs: 'research/signs',
            causes: 'research/causes',
            adminSigns: 'admin/signs',
            adminCauses: 'admin/causes',
            symptoms: 'research/symptoms',
            bodyParts: 'research/bodyparts',
            bodyRegion: 'research/bodyregions',
            adminBodyRegion: '/admin/bodyregions',
            adminBodyParts: 'admin/bodyparts',
            adminSymptoms: 'admin/symptoms',
            adminBodyPartMatrix: 'admin/bodypartmatrix',
            adminSkeletonAreas: 'admin/skeletonareas',
            clinics: 'research/locate/{zipcode}',
            injuryDetail: 'research/injury/detail/',
            planDetail: 'research/plan/detail/',
            exerciseDetail: 'research/exercise/detail/',
            signFilters: 'lookup/signfilters',
            causeFilters: 'lookup/causefilters',
            treatments: 'research/treatments/',
            adminTreatments: 'admin/treatments/',
            prognoses: 'research/prognoses/',
            adminPrognoses: 'admin/prognoses/'
        };

        var returnUris = {
            researchExercise: '/#/research/exercises',
            researchPlans: '/#/research/plans',
            researchInjuries: '/#/research/injuries',
        };

        var lookups = {
            exerciseCategories: ['Stretching', 'Spinal Stabilization', 'Strengthening', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization'],
            planCategories: ['Rehabilitation', 'Stretching', 'Preventative', 'Spinal Stabilization', 'Strengthening', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization'],
            functionPlanCategories: ['Rehabilitation', 'Stretching', 'Preventative', 'Spinal Stabilization', 'Strengthening', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization'],
            functionExerciseCategories: ['Stretching', 'Spinal Stabilization', 'Strengthening', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization'],
            regionCategories: ['UpperExtremity', 'LowerExtremity', 'Spine'],
            holdTypes: ['Seconds', 'Minutes', 'Breaths'],
            causeCategories: ['Lifestyle', 'Physiological'],
            signCategories: ['Functional', 'Subjective', 'Visual'],
            treatmentCategories: ['ManualTherapy', 'TherEx', 'Modalities', 'Education'],
            treatmentProviders: ['Self', 'PhysicalTherapist', 'MessageTherapist', 'Physican', 'Surgeon', 'Chiropracter'],
            prognosisCategories: ['BestCase', 'DelayedRecovery', 'WorstCase']
        };


    //IE does not set origin
    if (!window.location.origin) {
        window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
    }

    //needed to put the abs location because the static pages for research were not setting the path correctly
    var equipmentProxy = $resource(window.location.origin + '/' + apiUris.briefEquipment);
    var bodyRegionProxy = $resource(window.location.origin + '/' + apiUris.bodyRegion);
    var signFilterProxy = $resource(window.location.origin + '/' + apiUris.signFilters);
    var symptomaticRegionsProxy = $resource(window.location.origin + '/' + apiUris.symptomaticRegions);

    var equipment = equipmentProxy.query();
    var bodyRegions = bodyRegionProxy.query();
    var signFilters = signFilterProxy.query();
    var symptomaticRegions = symptomaticRegionsProxy.query();
    var maxSelectableAreas = 2;
    var likelyHoodThresholds = { high: .75, medium: .20 };

    var kendoEditorOptions = [
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

    return {
        apiUris: apiUris,
        equipment: equipment,
        bodyRegions: bodyRegions,
        signFilters: signFilters,
        returnUris: returnUris,
        maxSelectableAreas: maxSelectableAreas,
        symptomaticRegions: symptomaticRegions,
        likelyHoodThresholds: likelyHoodThresholds,
        lookups: lookups,
        kendoEditorOptions: kendoEditorOptions,
        kendoEditorIgnores: kendoEditorIgnores
    };


}]);
