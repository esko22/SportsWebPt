using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.Web.Services.Proxies
{
    internal class UserResourceResponse : ApiResourceRequest<UserDto> {}

    internal class VideoResourceResponse : ApiResourceRequest<VideoDto> { }

    internal class EquipmentResourceResponse : ApiResourceRequest<EquipmentDto> { }

    internal class SignResourceResponse : ApiResourceRequest<SignDto> { }

    internal class CauseResourceResponse : ApiResourceRequest<CauseDto> { }

    internal class InjuryResourceResponse : ApiResourceRequest<InjuryDto> { }

}
