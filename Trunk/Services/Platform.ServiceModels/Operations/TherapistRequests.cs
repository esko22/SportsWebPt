using System;
using ServiceStack.ServiceHost;

using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/therapists/{id}/exercises", "GET")]
    public class TherapistExerciseListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<BriefExerciseDto, BasicSortBy>>
    {
        #region Properties

        public Boolean IsOwner { get; set; }

        #endregion
    }

    [Route("/therapists/{id}/plans", "GET")]
    public class TherapistPlanListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<BriefPlanDto, BasicSortBy>>
    {
        #region Properties

        public Boolean IsOwner { get; set; }

        #endregion
    }

    [Route("/therapists/{id}/sharedplans", "GET")]
    public class TherapistSharedPlanListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<TherapistSharedPlanDto, BasicSortBy>>
    {
        #region Properties

        public int PlanId { get; set; }

        #endregion
    }

    [Route("/therapists/{id}/sharedexercises", "GET")]
    public class TherapistSharedExerciseListRequest : AbstractResourceListRequest,
    IReturn<ApiListResponse<TherapistSharedExerciseDto, BasicSortBy>>
    {
        #region Properties

        public int ExerciseId { get; set; }

        #endregion
    }

    [Route("/therapists/{id}/sharedplans", "PUT")]
    public class UpdateTherapistSharedPlanRequest : AbstractResourceRequest, IReturn<ApiListResponse<TherapistSharedPlanDto, BasicSortBy>>
    {
        #region Properties

        public TherapistSharedPlanDto[] SharedPlans { get; set; }

        #endregion
    }

    [Route("/therapists/{id}/sharedexercises", "PUT")]
    public class UpdateTherapistSharedExerciseRequest : AbstractResourceRequest, IReturn<ApiListResponse<TherapistSharedExerciseDto, BasicSortBy>>
    {
        #region Properties

        public TherapistSharedExerciseDto[] SharedExercises { get; set; }

        #endregion
    }


    [Route("/therapists/{id}", "GET")]
    public class TherapistRequest : AbstractResourceRequest, IReturn<ApiResponse<TherapistDto>>
    {
        
    }

    [Route("/therapists/{id}/episodes", "GET")]
    public class TherapistEpisodeListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<EpisodeDto, BasicSortBy>>
    {
        #region Properties

        public EpisodeStateDto? State { get; set; }

        #endregion

    }
}
