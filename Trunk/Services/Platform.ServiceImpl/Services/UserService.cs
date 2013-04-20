using System.Web;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceImpl.Operations;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    public class UserService : LoggingRestServiceBase<UserRequest, ApiResponse<UserDto>>
    {

        #region Properties

        public IUserUnitOfWork UserUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(UserRequest request)
        {
            var user = request.IdAsInt > 0
                           ? UserUnitOfWork.UserRepository.GetById((int) request.IdAsInt)
                           : UserUnitOfWork.UserRepository.GetUserByEmailAddress(HttpUtility.UrlDecode(Request.QueryString["email"]));
            UserDto userDto = null;

            if (user != null)
                userDto = Mapper.Map<UserDto>(user);

            return Ok(new ApiResponse<UserDto>()
                {
                    Resource = userDto
                });
        }

        public override object OnPost(UserRequest request)
        {
            var userToAdd = request.Resource;
            Check.Argument.IsNotNull(userToAdd,"UserToAdd");

            var userId = UserUnitOfWork.UserRepository.Add(Mapper.Map<User>(userToAdd));

            return Ok(new ApiResponse<UserDto>()
                {
                    Resource = new UserDto() {id = userId}
                });
        }

        #endregion

    }
}
