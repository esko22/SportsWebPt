swptApp.factory('configService', function () {

    var apiUris = {
        users: 'users',
        favorites: 'users/favorites',
        skeletonAreas: 'examine/areas',
        symptomaticRegions: 'examine/symptomaticregions',
        potentialSymptoms: 'examine/potentialSymptoms',
        componentsByArea: 'examine/components?areaId=',
        diagnosisReport: 'examine/diagnosisReport',
        injuries: 'research/injuries',
        adminInjuries: 'admin/injuries',
        plans: 'data/plans',
        adminPlans: 'admin/plans',
        exercises: 'research/exercises',
        adminExercises: 'admin/exercises',
        equipment: 'research/equipment',
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

    return {
        apiUris: apiUris
    };


});