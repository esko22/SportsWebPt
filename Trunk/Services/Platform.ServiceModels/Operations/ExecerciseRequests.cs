using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/exercises", "GET")]
    public class ExerciseListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<ExerciseDto, BasicSortBy>>
    {}

    [Route("/exercises/brief", "GET")]
    public class BriefExerciseListRequest : AbstractResourceListRequest,
        IReturn<ApiListResponse<BriefExerciseDto, BasicSortBy>>
    {

        #region Properties

        public int ClinicId { get; set; }

        public Boolean? IsPublic { get; set; }

        #endregion
    }


    [Route("/exercises", "POST")]
    public class CreateExerciseRequest : ExerciseDto, IReturn<ApiResponse<ExerciseDto>>
    {
    }

    [Route("/exercises/{id}", "PUT")]
    public class UpdateExerciseRequest : ExerciseDto, IReturn<ApiResponse<ExerciseDto>>
    {
    }

    [Route("/exercises/{id}", "GET")]
    public class ExerciseRequest : AbstractResourceRequest, IReturn<ApiResponse<ExerciseDto>>
    {}
}
