
swptApp.service('configService', ['$resource', '$location', function ($resource, $location) {
    var apiUris = {
            currentUser: '/data/users/current',
            users: '/data/users/:id',
            validateByEmail: '/data/users/validate',
            favorites: '/data/users/favorites',
            skeletonAreas: '/data/examine/areas',
            symptomaticRegions: '/data/examine/symptomaticregions',
            potentialSymptoms: '/data/examine/potentialSymptoms',
            componentsByArea: '/data/examine/components?areaId=',
            diagnosisReport: '/data/examine/diagnosisReport',
            injuries: '/data/research/injuries',
            briefInjuries: '/data/injuries/brief',
            adminInjuries: '/data/admin/injuries',
            plans: '/data/research/plans',
            briefPlans: '/data/plans/brief',
            adminPlans: '/data/admin/plans',
            exercises: '/data/research/exercises',
            briefExercises: '/data/exercises/brief',
            adminExercises: '/data/admin/exercises',
            equipment: '/data/research/equipment',
            briefEquipment: '/data/research/equipment/brief',
            adminEquipment: '/data/admin/equipment',
            videos: '/data/research/videos',
            adminVideos: '/data/admin/videos',
            signs: '/data/research/signs',
            causes: '/data/research/causes',
            adminSigns: '/data/admin/signs',
            adminCauses: '/data/admin/causes',
            symptoms: '/data/research/symptoms',
            bodyParts: '/data/research/bodyparts',
            bodyRegion: '/data/research/bodyregions',
            adminBodyRegion: '/data/admin/bodyregions',
            adminBodyParts: '/data/admin/bodyparts',
            adminSymptoms: '/data/admin/symptoms',
            adminBodyPartMatrix: '/data/admin/bodypartmatrix',
            adminSkeletonAreas: '/data/admin/skeletonareas',
            clinics: '/data/research/locate/{zipcode}',
            injuryDetail: '/data/research/injury/detail/',
            planDetail: '/data/research/plan/detail/',
            exerciseDetail: '/data/research/exercise/detail/',
            signFilters: '/data/lookup/signfilters',
            causeFilters: '/data/lookup/causefilters',
            treatments: '/data/research/treatments/',
            adminTreatments: '/data/admin/treatments/',
            prognoses: '/data/research/prognoses/',
            adminPrognoses: '/data/admin/prognoses/',
            therapists: '/data/therapists/:id',
            therapistCases: '/data/therapists/cases',
            therapistPlans: '/data/therapists/plans',
            therapistExercises: '/data/therapists/exercises',
            therapistSharedPlans: '/data/therapists/sharedplans',
            therapistSharedPlanDetail: '/data/therapists/sharedplandetail/:id',
            therapistSharedExercises: '/data/therapists/sharedexercises',
            therapistSharedExerciseDetail: '/data/therapists/sharedexercisedetail/:id',
            patients: '/data/patients/:id',
            patientSnapshot: '/data/patients/snapshot',
            patientCases: '/data/patients/cases',
            cases: '/data/cases/:id',
            caseSessions: '/data/cases/:id/sessions',
            clinic: '/data/clinics/:id',
            clinicTherapists: '/data/clinics/:id/therapists',
            clinicPatients: '/data/clinics/:id/patients',
            managedClinics: '/data/users/current/managedclinics',
            sessions: '/data/sessions/:id',
            sessionPlans: '/data/sessions/:id/plans',
            registerPatient: '/data/registration/patient',
            registerTherapist: '/data/registration/therapist',
        };


        var repetitionRangeValues = [];
        for (var i = 0; i <= 100; i++) {
            repetitionRangeValues.push(i);
        };


        var returnUris = {
            researchExercise: '/#!/research/exercises',
            researchPlans: '/#!/research/plans',
            researchInjuries: '/#!/research/injuries',
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
            prognosisCategories: ['BestCase', 'DelayedRecovery', 'WorstCase'],
            difficulties: ['Beginner', 'Intermediate', 'Advanced'],
            repetitionRangeValues: repetitionRangeValues
        };


    //IE does not set origin
    if (!window.location.origin) {
        window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
    }

    //needed to put the abs location because the static pages for research were not setting the path correctly
    var equipmentProxy = $resource(window.location.origin + apiUris.briefEquipment);
    var bodyRegionProxy = $resource(window.location.origin + apiUris.bodyRegion);
    var signFilterProxy = $resource(window.location.origin + apiUris.signFilters);
    var causeFilterProxy = $resource(window.location.origin + apiUris.causeFilters);
    var symptomaticRegionsProxy = $resource(window.location.origin + apiUris.symptomaticRegions);
    //these should be moved out
    var bodyPartMatrixProxy = $resource(window.location.origin + apiUris.adminBodyPartMatrix);
    var availableSymptomsProxy = $resource(window.location.origin + apiUris.adminSymptoms);

    var equipment = equipmentProxy.query();
    var bodyRegions = bodyRegionProxy.query();
    var signFilters = signFilterProxy.query();
    var causeFilters = causeFilterProxy.query();
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
        causeFilters: causeFilters,
        returnUris: returnUris,
        maxSelectableAreas: maxSelectableAreas,
        symptomaticRegions: symptomaticRegions,
        likelyHoodThresholds: likelyHoodThresholds,
        lookups: lookups,
        kendoEditorOptions: kendoEditorOptions,
        kendoEditorIgnores: kendoEditorIgnores,
        bodyPartMatrixProxy: bodyPartMatrixProxy,
        availableSymptomsProxy: availableSymptomsProxy
    };


}]);
