using System;
using System.Web;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class UserManagementService : BaseServiceStackClient, IUserManagementService
    {

        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion
       

        #region Methods

        public UserManagementService(SportsWebPtClientSettings clientSettings) 
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        public User GetUser(String emailAddress)
        {
            var request = GetSync(new UserRequest() { Id = emailAddress });

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        }

        public User GetUser(int id)
        {
            var request = GetSync(new UserRequest() { Id = id.ToString()} );

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        }

        public int AddUser(User user)
        {
            var userResuest = Mapper.Map<CreateUserRequest>(user);
   
            var request = PostSync(userResuest);

            return request.Response.Id;
        }

        public void AddFavorite(Favorite userFavorite, int userId)
        {
            var userFavRequest = Mapper.Map<CreateUserFavoriteRequest>(userFavorite);
            userFavRequest.Id = userId;

            PostSync(userFavRequest);
        }

        public User Auth(string emailAddress, string hash)
        {
            var request = PostSync(new AuthRequest() { EmailAddress = emailAddress, Hash = hash });

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        }

        #endregion

    }
}
