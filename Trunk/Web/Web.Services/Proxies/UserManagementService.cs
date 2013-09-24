using System;
using System.Web;
using AutoMapper;

using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.Web.Services.Proxies;

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
            var response =
                GetSync<UserResourceResponse>(String.Format("{0}?email={1}", _sportsWebPtClientSettings.UserUriPath, HttpUtility.UrlEncode(emailAddress)));

            return response.Resource == null ? null : Mapper.Map<User>(response.Resource);
        }

        public User GetUser(int id)
        {
            var response =
                GetSync<UserResourceResponse>(String.Format("{0}/{1}", _sportsWebPtClientSettings.UserUriPath, id));

            return response.Resource == null ? null : Mapper.Map<User>(response.Resource);
        }

        public int AddUser(User user)
        {
            var userResuest = new ApiResourceRequest<UserDto>
                {
                    Resource = Mapper.Map<UserDto>(user)
                };

            var response =
                PostSync<UserResourceResponse>(_sportsWebPtClientSettings.UserUriPath, userResuest);

            return response.Resource.id;
        }

        public void AddFavorite(UserFavorite userFavorite)
        {
            var userFavResuest = new ApiResourceRequest<UserFavoriteDto>
            {
                Resource = Mapper.Map<UserFavoriteDto>(userFavorite)
            };

            var response =
                PostSync<ApiResourceRequest<UserFavoriteDto>>(_sportsWebPtClientSettings.UserFavoriteUriPath, userFavResuest);
        }

        public User Auth(string emailAddress, string hash)
        {
            var response =
                PostSync<UserResourceResponse>(_sportsWebPtClientSettings.AuthUriPath, new AuthRequestDto() { emailAddress = emailAddress, hash = hash });

            return response.Resource == null ? null : Mapper.Map<User>(response.Resource);
        }

        #endregion

    }
}
