using System;
using System.Linq;
using System.Web;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl.Services
{
    public class UserService : RestService
    {

        #region Properties

        public IUserUnitOfWork UserUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(UserRequest request)
        {
            var user = request.IdAsInt > 0
                           ? UserUnitOfWork.GetUserById(request.IdAsInt)
                           : UserUnitOfWork.GetUserBySubject(request.Id);

            UserDto userDto = null;

            if (user != null)
                userDto = Mapper.Map<UserDto>(user);

            return Ok(new ApiResponse<UserDto>()
                {
                    Response = userDto
                });
        }

        public object Post(CreateUserRequest request)
        {
            var userToAdd = request;
            Check.Argument.IsNotNull(userToAdd,"UserToAdd");

            var userEntity = UserUnitOfWork.AddUser(Mapper.Map<User>(userToAdd));

            return Ok(new ApiResponse<UserDto>()
                {
                    Response = new UserDto() { Id = userEntity.Id, Hash = userEntity.Hash}
                });
        }

        public object Post(CreateUserFavoriteRequest request)
        {
            var favoriteToAdd = request;
            Check.Argument.IsNotNull(favoriteToAdd, "FavoriteToAdd");

            var user =
                UserUnitOfWork.UserRepository
                .GetUserDetails()
                .SingleOrDefault(p => p.Id == favoriteToAdd.Id);

            if (user == null)
                return BadRequest("Invalid User Id");


            switch (favoriteToAdd.Entity)
            {
                case FavoriteTypeDto.Injury:
                    if (user.InjuryFavorites.All(p => p.Id != favoriteToAdd.EntityId))
                    {
                        var injury = UserUnitOfWork.InjuryRepository.GetById(favoriteToAdd.EntityId);
                        if (injury != null)
                            user.InjuryFavorites.Add(injury);
                    }
                    break;
                case FavoriteTypeDto.Plan:
                    if (user.PlanFavorites.All(p => p.Id != favoriteToAdd.EntityId))
                    {
                        var plan = UserUnitOfWork.PlanRepository.GetById(favoriteToAdd.EntityId);
                        if (plan != null)
                            user.PlanFavorites.Add(plan);
                    }
                    break;
                case FavoriteTypeDto.Exercise:
                    if (user.ExerciseFavorites.All(p => p.Id != favoriteToAdd.EntityId))
                    {
                        var exercise = UserUnitOfWork.ExerciseRepository.GetById(favoriteToAdd.EntityId);
                        if (exercise != null)
                            user.ExerciseFavorites.Add(exercise);
                    }
                    break;
                case FavoriteTypeDto.Video:
                    if (user.VideoFavorites.All(p => p.Id != favoriteToAdd.EntityId))
                    {
                        var video = UserUnitOfWork.VideoRepository.GetById(favoriteToAdd.EntityId);
                        if (video != null)
                            user.VideoFavorites.Add(video);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UserUnitOfWork.Commit();

            return Ok(new ApiResponse<FavoriteDto>()
            {
                Response = request
            });
        }


        #endregion

    }
}
