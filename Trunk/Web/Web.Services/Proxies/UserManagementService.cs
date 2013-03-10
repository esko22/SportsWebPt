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
            //TODO: need to put rest uris into a static config
            var response =
                GetSync<UserResponse, UserDto>(String.Format("/{0}/users/{1}", _settings.Version, id));

            return new User() { emailAddress = response.emailAddress, firstName = response.firstName, lastName = response.lastName} ;
        }

        public int AddUser(User user)
        {
            var userResuest = new AddUserRequest
                {
                    emailAddress = user.emailAddress,
                    firstName = user.firstName,
                    lastName = user.lastName,
                    password = user.password
                };

            var response =
                PostSync<AddUserResponse, UserDto>(String.Format("/{0}/users", _settings.Version), userResuest);

            return 0;
        }

        #endregion
    }
}
