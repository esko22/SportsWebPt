using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities.ServiceApi;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceContracts.Models;
using SportsWebPt.Platform.ServiceImpl.Operations;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    [ApiResource("User CRUD endpoint", "user", "/operations?resource=user")]
    public class UserService : LoggingRestServiceBase<UserRequest, UserResponse>
    {

        #region Properties

        public IUserUnitOfWork UserUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(UserRequest request)
        {

            var user = UserUnitOfWork.UserRepository.GetById((int)request.IdAsInt);

            return Ok(new UserResponse()
                {
                    Response = new UserDto() {emailAddress = user.EmailAddress, firstName = user.FirstName, lastName = user.LastName}
                });
        }

        public override object OnPost(UserRequest request)
        {
            UserUnitOfWork.UserRepository.Add(new User()
                {
                    EmailAddress = request.emailAddress,
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    Password = request.password,
                    UserName = request.userName
                });

            UserUnitOfWork.Commit();

            return Ok(new UserResponse()
                {
                    Response = new UserDto()
                });
        }

        #endregion

    }
}
