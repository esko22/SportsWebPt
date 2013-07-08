using System;
using System.Linq;
using System.Web;

using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
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
            var userEmailAddress = HttpUtility.UrlDecode(Request.QueryString["email"]);
            var user = request.IdAsInt > 0
                           ? UserUnitOfWork.UserRepository.GetById((int) request.IdAsInt)
                           : UserUnitOfWork.UserRepository.GetAll()
                                           .FirstOrDefault(
                                               p =>
                                               p.EmailAddress.Equals(userEmailAddress,
                                                   StringComparison.OrdinalIgnoreCase));
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

            var userEntity = Mapper.Map<User>(userToAdd);
            UserUnitOfWork.UserRepository.Add(userEntity);
            UserUnitOfWork.Commit();

            return Ok(new ApiResponse<UserDto>()
                {
                    Resource = new UserDto() { id = userEntity.Id}
                });
        }

        #endregion

    }
}
