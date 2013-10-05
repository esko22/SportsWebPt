using System;
using System.Linq;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities.Security;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class AuthService : RestService
    {
        #region Properties

        public IUserUnitOfWork UserUnitOfWork { get; set; }

        #endregion


        public object Post(AuthRequest request)
        {
            UserDto userDto = null;
            var user = UserUnitOfWork.UserRepository.GetAll()
                                           .FirstOrDefault(
                                               p =>
                                               p.EmailAddress.Equals(request.EmailAddress,
                                                   StringComparison.OrdinalIgnoreCase));
                
            if (user != null)
            {
                if(PasswordHashHelper.ValidatePassword(request.Hash,user.Hash))
                    userDto = Mapper.Map<UserDto>(user);
            }

            return Ok(new ApiResponse<UserDto>()
            {
                Response = userDto
            });

        }
    }
}
