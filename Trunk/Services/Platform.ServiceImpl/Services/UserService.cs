using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
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
            var user = request.IdAsInt > 0
                           ? UserUnitOfWork.UserRepository.GetById((int) request.IdAsInt)
                           : UserUnitOfWork.UserRepository.GetUserByEmailAddress(request.User.emailAddress);
            var userDto = new UserDto();

            //TODO: this needs to cleaned up for sure

            if (user != null)
                userDto = new UserDto()
                    {
                        emailAddress = user.EmailAddress,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        id = user.Id,
                        gender = user.Gender,
                        locale = user.Locale,
                        provider = user.Provider,
                        providerId = user.ProviderId,
                        skypeHandle = user.SkypeHandle
                    };

            return Ok(new UserResponse()
                {
                    Response = userDto
                });
        }

        public override object OnPost(UserRequest request)
        {
            var userToAdd = request.User;
            Check.Argument.IsNotNull(userToAdd,"UserToAdd");

            var userId = UserUnitOfWork.UserRepository.Add(new User()
                {
                    EmailAddress = userToAdd.emailAddress,
                    FirstName = userToAdd.firstName,
                    LastName = userToAdd.lastName,
                    Password = userToAdd.password,
                    UserName = userToAdd.userName,
                    Gender = userToAdd.gender,
                    Locale = userToAdd.locale,
                    Provider = userToAdd.provider,
                    ProviderId = userToAdd.providerId
                });

            return Ok(new UserResponse()
                {
                    Response = new UserDto() {id = userId}
                });
        }

        #endregion

    }
}
