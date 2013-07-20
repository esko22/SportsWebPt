using SportsWebPt.Common.ServiceStack.Infrastructure;

namespace SportsWebPt.Platform.ServiceModels
{
    public class ExecerciseListRequest : ApiResourceListRequest
    {

    }

    public class ExecerciseRequest : ApiResourceRequest<ExerciseDto>
    {
        #region Properties

        public string PageName { get; set; }

        #endregion
    }
}
