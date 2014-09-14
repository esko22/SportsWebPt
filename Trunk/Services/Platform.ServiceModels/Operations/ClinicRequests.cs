﻿using System;
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
    public class AddClinicPatientRequest : AbstractResourceListRequest, IReturn<ApiResponse<UserDto>>
    {
        public UserDto User { get; set; }
    }

    [Route("/clinics/{id}/therapists", "POST")]
    public class AddClinicTherapistRequest : AbstractResourceListRequest, IReturn<ApiResponse<UserDto>>
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
