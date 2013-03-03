using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities.ServiceApi;
using SportsWebPt.Platform.DataAccess.UOW;
using SportsWebPt.Platform.ServiceContracts.Models;
using SportsWebPt.Platform.ServiceImpl.Operations;

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

            var userUow = new UserUOW();
            var user = userUow.GetUser(int.Parse(request.Id));

            return Ok(new UserResponse()
                {
                    Response = new UserDto() {EmailAddress = user.EmailAddress, FirstName = user.FirstName, LastName = user.LastName}
                });
        }

        #endregion

    }
}
