﻿using System;
using System.Linq;
using System.Web;
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
            var user = UserUnitOfWork.UserRepository.GetAll()
                                           .FirstOrDefault(
                                               p =>
                                               p.EmailAddress.Equals(request.emailAddress,
                                                   StringComparison.OrdinalIgnoreCase));
                
            if (user != null)
            {
                if(PasswordHashHelper.ValidatePassword(request.hash,user.Hash))
                    userDto = Mapper.Map<UserDto>(user);
            }

            return Ok(new ApiResponse<UserDto>()
            {
                Resource = userDto
            });

        }
    }
}
