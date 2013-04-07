using System;
using AutoMapper;
using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Common.Utilities.Security;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class AuthService : LoggingRestServiceBase<AuthRequestDto, ApiResponse<UserDto>>
    {
        #region Properties

        public IUserUnitOfWork UserUnitOfWork { get; set; }

        #endregion


        public override object OnPost(AuthRequestDto request)
        {
            UserDto userDto = null;
            var user = UserUnitOfWork.UserRepository.GetUserByEmailAddress(request.EmailAddress);

            if (user != null)
            {
                if(PasswordHashHelper.ValidatePassword(request.Hash,user.Hash))
                    userDto = Mapper.Map<UserDto>(user);
            }

            return Ok(new ApiResponse<UserDto>()
            {
                Resource = userDto
            });

        }
    }
}
