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

        public String emailAddress { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public String userName { get; set; }

        public String password { get; set; }

        public String phone { get; set; }

        public String skypeHandle { get; set; }

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
