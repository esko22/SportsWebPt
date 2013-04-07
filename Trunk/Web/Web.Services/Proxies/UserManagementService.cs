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

        private String _userUriPath = String.Empty;
        private String _authUriPath = String.Empty;

        #endregion

        #region Methods

        public UserManagementService(BaseServiceStackClientSettings clientSettings) 
            : base(clientSettings)
        {
            //TODO: something feels wrong here
            _userUriPath = String.Format("/{0}/users", _settings.Version);
            _authUriPath = String.Format("/{0}/auth", _settings.Version);
        }

        public User GetUser(String emailAddress)
        {
            var response =
                GetSync<UserResourceResponse>(String.Format("{0}?email={1}", _userUriPath, HttpUtility.UrlEncode(emailAddress)));

            if (response.Resource == null)
                return null;

            return Mapper.Map<User>(response.Resource);
        }

        public User GetUser(int id)
        {
            var response =
                GetSync<UserResourceResponse>(String.Format("{0}/{1}", _userUriPath, id));

            if (response.Resource == null)
                return null;

            return Mapper.Map<User>(response.Resource);
        }

        public int AddUser(User user)
        {
            var userResuest = new ApiResourceRequest<UserDto>
                {
                    Resource = Mapper.Map<UserDto>(user)
                };

            var response =
                PostSync<UserResourceResponse>(_userUriPath, userResuest);

            return response.Resource.id;
        }

        public User Auth(string emailAddress, string hash)
        {
            var response =
                PostSync<UserResourceResponse>(_authUriPath, new AuthRequestDto() { EmailAddress = emailAddress, Hash = hash} );

            if (response.Resource == null)
                return null;

            return Mapper.Map<User>(response.Resource);
        }

        #endregion

    }
}
