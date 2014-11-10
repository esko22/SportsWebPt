using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;

using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels.Operations
{
    [Route("/clinics/{id}/plans")]
    public class ClinicPlanRequest : AbstractResourceRequest, IReturn<ApiResponse<PlanDto>>
    { }

    [Route("/clinics/{id}/patients", "GET")]
    public class ClinicPatientListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<UserDto, BasicSortBy>>
    { }

    [Route("/clinics/{id}/patients", "POST")]
    public class AddClinicPatientRequest : AbstractResourceListRequest, IReturn<ApiResponse<ClinicPatientDto>>
    {
        public UserDto User { get; set; }
    }

    [Route("/patients/validate", "GET")]
    public class ValidatePatientRegistrationRequest : AbstractResourceListRequest, IReturn<ApiResponse<ClinicPatientDto>>
    {
        #region Properties

        public String EmailAddress { get; set; }

        public String Pin { get; set; }

        public String ServiceAccount { get; set; }

        #endregion
    }

    [Route("/therapists/validate", "GET")]
    public class ValidateTherapistRegistrationRequest : AbstractResourceListRequest, IReturn<ApiResponse<ClinicTherapistDto>>
    {
        #region Properties

        public String EmailAddress { get; set; }

        public String Pin { get; set; }

        public String ServiceAccount { get; set; }

        #endregion

    }

    [Route("/clinics/{id}/therapists", "POST")]
    public class AddClinicTherapistRequest : AbstractResourceListRequest, IReturn<ApiResponse<ClinicTherapistDto>>
    {
        public UserDto Therapist { get; set; }
    }

    [Route("/clinics/{id}/therapists")]
    public class ClinicTherapistListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<TherapistDto, BasicSortBy>>
    { }

    [Route("/clinics/{id}")]
    public class ClinicRequest : AbstractResourceRequest, IReturn<ApiResponse<ClinicDto>>
    { }

    [Route("/clinics/managers/{id}")]
    public class ManagerClinicListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<ClinicDto, BasicSortBy>>
    {}


}
