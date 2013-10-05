
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/users", "POST")]
    public class CreateUserRequest : UserDto, IReturn<ApiResponse<UserDto>>
    {}

    [Route("/users/{id}", "PUT")]
    public class UpdateUserRequest : UserDto, IReturn<ApiResponse<UserDto>>
    {}

    [Route("/users/{id}", "GET")]
    public class UserRequest : AbstractResourceRequest, IReturn<ApiResponse<UserDto>>
    {}

    [Route("/users/favorites", "POST")]
    public class CreateUserFavoriteRequest : UserFavoriteDto, IReturn<ApiResponse<UserFavoriteDto>>
    {}

    [Route("/auth", "POST")]
    public class AuthRequest : AuthRequestDto, IReturn<ApiResponse<UserDto>> { }
}
