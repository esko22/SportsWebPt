using System;
using System.Web;
using AutoMapper;
using SportsWebPt.Platform.ServiceContracts.Models;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.Web.Services.Proxies;

namespace SportsWebPt.Platform.Web.Services
{
    public class UserManagementService : BaseServiceStackClient, IUserManagementService
    {

        #region Fields

        private String _userUriPath = String.Empty;

        #endregion

        #region Methods

        public UserManagementService(BaseServiceStackClientSettings clientSettings) 
            : base(clientSettings)
        {
            _userUriPath = String.Format("/{0}/users", _settings.Version);
        }

        public User GetUser(String emailAddress)
        {
            var response =
                GetSync<UserResourceResponse>(String.Format("{0}?email={1}", _userUriPath, HttpUtility.UrlEncode(emailAddress)));

            return Mapper.Map<User>(response.Resource);
        }

        public User GetUser(int id)
        {
            var response =
                GetSync<UserResourceResponse>(String.Format("{0}/{1}", _userUriPath, id));

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

        #endregion
    }
}
