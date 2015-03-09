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

    [Route("/therapists/{id}/sharedplans", "PUT")]
    public class UpdateTherapistSharedPlanRequest : AbstractResourceRequest, IReturn<ApiListResponse<ClinicPlanDto, BasicSortBy>>
    {
        #region Properties

        public ClinicPlanDto[] SharedPlans { get; set; }

        #endregion
    }

    [Route("/therapists/{id}/sharedexercises", "PUT")]
    public class UpdateTherapistSharedExerciseRequest : AbstractResourceRequest, IReturn<ApiListResponse<ClinicExerciseDto, BasicSortBy>>
    {
        #region Properties

        public ClinicExerciseDto[] SharedExercises { get; set; }

        #endregion
    }

    [Route("/therapists/{id}", "GET")]
    public class TherapistRequest : AbstractResourceRequest, IReturn<ApiResponse<TherapistDto>>
    {}

    [Route("/therapists/{id}/cases", "GET")]
    public class TherapistCaseListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<CaseDto, BasicSortBy>>
    {
        #region Properties

        public CaseStateDto? State { get; set; }

        #endregion

    }

    [Route("/register/therapist", "PUT")]
    public class RegisterTherapistRequest : AbstractResourceRequest, IReturn<ApiResponse<TherapistDto>>
    {
        #region Properties

        public int RegistrationId { get; set; }

        public UserDto Therapist { get; set; }

        #endregion
    }

}
