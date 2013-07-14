﻿using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.Web.Services.Proxies
{
    internal class UserResourceResponse : ApiResourceRequest<UserDto> {}

    internal class VideoResourceResponse : ApiResourceRequest<VideoDto> { }

    internal class EquipmentResourceResponse : ApiResourceRequest<EquipmentDto> { }


}