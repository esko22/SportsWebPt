var configModule = angular.module('config.module', []);

configModule.service('configService', ['$resource','$location', function ($resource, $location) {
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
        adminBodyRegion: 'admin/bodyregions',
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

    var exerciseCategories = ['Stretching', 'Spinal Stabilization', 'Strengthening', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization'];
    var planCategories = ['Rehabilitation', 'Stretching', 'Preventative', 'Spinal Stabilization', 'Strengthening', 'Self Massage', 'Range Of Motion', 'Balance', 'Mobilization'];

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

    return {
        apiUris: apiUris,
        exerciseCategories: exerciseCategories,
        planCategories : planCategories,
        equipment: equipment,
        bodyRegions: bodyRegions,
        signFilters: signFilters,
        returnUris: returnUris,
        maxSelectableAreas: maxSelectableAreas,
        symptomaticRegions: symptomaticRegions,
        likelyHoodThresholds: likelyHoodThresholds
    };


}]);
