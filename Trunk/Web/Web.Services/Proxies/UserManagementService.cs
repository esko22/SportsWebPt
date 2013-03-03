using System;
using SportsWebPt.Platform.ServiceContracts.Models;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Common.ServiceStackClient;

namespace SportsWebPt.Platform.Web.Services
{
    public class UserManagementService : BaseServiceStackClient, IUserManagementService
    {

        #region Methods

        public UserManagementService(BaseServiceStackClientSettings clientSettings) 
            : base(clientSettings)
        {
        }

        public User GetUser(String emailAddress)
        {

            return new User();
        }

        public User GetUser(int id)
        {
            var response =
                GetSync<UserResponse, UserDto>(String.Format("/{0}/user/{1}", _settings.Version, id));

            return new User() { EmailAddress = response.EmailAddress, FirstName = response.FirstName, LastName = response.LastName} ;
        }

        #endregion
    }
}
