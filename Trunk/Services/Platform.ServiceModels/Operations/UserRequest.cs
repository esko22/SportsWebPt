
using System;
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

    [Route("/register/patient", "PUT")]
    public class RegisterPatientRequest : AbstractResourceRequest, IReturn<ApiResponse<UserDto>>
    {
        #region Properties

        public int RegistrationId { get; set; }

        public UserDto User { get; set; }

        #endregion
    }

    [Route("/users/{id}", "GET")]
    public class UserRequest : AbstractResourceRequest, IReturn<ApiResponse<UserDto>>
    {}

    [Route("/users/{id}/favorites", "POST")]
    public class CreateUserFavoriteRequest : FavoriteDto, IReturn<ApiResponse<FavoriteDto>>
    {
        #region Properties

        public String Id { get; set; }

        #endregion
    }

}
