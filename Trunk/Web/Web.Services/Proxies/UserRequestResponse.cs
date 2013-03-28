using System;

using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.ServiceContracts.Models;

namespace SportsWebPt.Platform.Web.Services
{
    public class UserRequest : AbstractRequest
    {
        #region v1.0

        public string EmailAddress { get; set; }

        #endregion
    }

    public class AddUserRequest : AbstractRequest
    {
        #region v1.0

        public UserDto User { get; set; }

        #endregion
    }

    public class AddUserResponse : BaseResponse<UserDto>
    {
        #region v1.0

        public override UserDto Response { get; set; }

        #endregion
    }

    public class UserResponse : BaseResponse<UserDto>
    {
        #region v1.0

        public override UserDto Response { get; set; }

        #endregion
    }
}
