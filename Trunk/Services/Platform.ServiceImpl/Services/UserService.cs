using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities.ServiceApi;
using SportsWebPt.Platform.ServiceImpl.Operations;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    [ApiResource("User CRUD endpoint", "user", "/operations?resource=user")]
    public class UserService : LoggingRestServiceBase<UserRequest, UserResponse>
    {

        #region Properties


        #endregion

        #region Methods

        public override object OnGet(UserRequest request)
        {
            return Ok(new UserResponse()
                {
                    Response = new UserDto() {EmailAddress = "nunya@swpt.com", FirstName = "Fat", LastName = "Jon"}
                });
        }

        #endregion

    }
}
