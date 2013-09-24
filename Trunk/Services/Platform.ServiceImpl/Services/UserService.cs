using System;
using System.Linq;
using System.Web;

using AutoMapper;
using ServiceStack.ServiceInterface.ServiceModel;
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

    public class UserFavoriteService : LoggingRestServiceBase<UserFavoriteRequest, ApiResponse<UserFavoriteDto>>
    {
        #region Properties

        public IUserUnitOfWork UserUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnPost(UserFavoriteRequest request)
        {
            var favoriteToAdd = request.Resource;
            Check.Argument.IsNotNull(favoriteToAdd, "FavoriteToAdd");

            var user =
                UserUnitOfWork.UserRepository.GetAll(new[] { "InjuryFavorites", "ExerciseFavorites", "VideoFavorites", "PlanFavorites" }).SingleOrDefault(p => p.Id == favoriteToAdd.UserId);

            if (user == null)
                return BadRequest("Invalid User Id");


            switch (favoriteToAdd.Entity)
            {
                case FavoriteType.Injury:
                    if (user.InjuryFavorites.All(p => p.Id != favoriteToAdd.EntityId))
                    {
                        var injury = UserUnitOfWork.InjuryRepository.GetById(favoriteToAdd.EntityId);
                        if(injury != null)
                            user.InjuryFavorites.Add(injury);
                    }
                    break;
                case FavoriteType.Plan:
                    if (user.PlanFavorites.All(p => p.Id != favoriteToAdd.EntityId))
                    {
                        var plan = UserUnitOfWork.PlanRepository.GetById(favoriteToAdd.EntityId);
                        if(plan != null)
                            user.PlanFavorites.Add(plan);
                    }
                    break;
                case FavoriteType.Exercise:
                    if (user.ExerciseFavorites.All(p => p.Id != favoriteToAdd.EntityId))
                    {
                        var exercise = UserUnitOfWork.ExerciseRepository.GetById(favoriteToAdd.EntityId);
                        if(exercise != null)
                            user.ExerciseFavorites.Add(exercise);
                    }
                    break;
                case FavoriteType.Video:
                    if (user.VideoFavorites.All(p => p.Id != favoriteToAdd.EntityId))
                    {
                        var video = UserUnitOfWork.VideoRepository.GetById(favoriteToAdd.EntityId);
                        if(video != null)
                            user.VideoFavorites.Add(video);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UserUnitOfWork.Commit();

            return Ok(new ApiResponse<UserFavoriteDto>()
            {
                Resource = request.Resource
            });
        }


        #endregion
    }
}
