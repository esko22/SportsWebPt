using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Identity.Core;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;
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

        public User GetUser(String id)
        {
            var relationUser = UserAccountServiceFactory().GetByID(new Guid(id));
            var user = new User {hash = relationUser.GetClaimValue("service_account")};

            return user;
        }

        public Boolean ValidateUserByEmail(String emailAddress)
        {
            return UserAccountServiceFactory().GetByEmail(emailAddress) != null;
        }

        public User GetServiceUser(String id)
        {
            var request = GetSync(new UserRequest() { Id = id });

            return request.Response == null ? new User() { hash = id, accountLinked = false} : Mapper.Map<User>(request.Response);
        }

        public int AddUser(User user)
        {
            var userResuest = Mapper.Map<CreateUserRequest>(user);
   
            var request = PostSync(userResuest);

            return request.Response.Id;
        }

        public String CreateServiceAccount(String subjectId)
        {
            var userAccountService = UserAccountServiceFactory();
            var request = PostSync(new CreateUserRequest { AccountLinked = true });
            userAccountService.AddClaim(new Guid(subjectId),"service_account", request.Response.Hash);
            UpdateServiceAccount(subjectId, request.Response.Hash, userAccountService);

            return request.Response.Hash;
        }

        //public String ResetServiceAccount(String subjectId, String serviceAccount)
        //{
        //    var userAccountService = UserAccountServiceFactory();
        //    userAccountService.RemoveClaim(new Guid(subjectId), "service_account");
        //    userAccountService.AddClaim(new Guid(subjectId), "service_account", serviceAccount);

        //    UpdateServiceAccount(subjectId, serviceAccount, userAccountService);

        //    return serviceAccount;
        //}

        private void UpdateServiceAccount(String subjectId, String serviceAccount, UserAccountService<SportsWebUser> userAccountService)
        {
            var userToEdit = userAccountService.GetByID(new Guid(subjectId));
            if (userToEdit != null)
            {
                userToEdit.ServiceAccount = serviceAccount;
                userAccountService.Update(userToEdit);
            }
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

        public IEnumerable<Episode> GetEpisodes(int patientId, String state)
        {
            EpisodeStateDto? episodeState = null;
            if (!String.IsNullOrEmpty(state))
                episodeState = (EpisodeStateDto)Enum.Parse(typeof(EpisodeStateDto), state, true);

            var request = GetSync(new PatientEpisodeListRequest() { Id = patientId.ToString(), State = episodeState });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Episode>>(request.Response.Items.OrderBy(p => p.CreatedOn));
        }

        public ClinicPatient ValidatePatientRegistration(String emailAddress, String pin, String serviceAccount)
        {
            var request = GetSync(new ValidatePatientRegistrationRequest() { EmailAddress = emailAddress, Pin = pin, ServiceAccount = serviceAccount});

            return request.Response == null ? null : Mapper.Map<ClinicPatient>(request.Response);
        }

        public ClinicTherapist ValidateTherapistRegistration(String emailAddress, String pin, String serviceAccount)
        {
            var request = GetSync(new ValidateTherapistRegistrationRequest() { EmailAddress = emailAddress, Pin = pin, ServiceAccount = serviceAccount });

            return request.Response == null ? null : Mapper.Map<ClinicTherapist>(request.Response);
        }

        public int RegisterPatient(User user, int registrationId)
        {
            var request =
                Put(new RegisterPatientRequest() {RegistrationId = registrationId, User = Mapper.Map<UserDto>(user)});

            return request.Response == null ? 0 : request.Response.Id;
        }

        public int RegisterTherapist(User therapist, int registrationId)
        {
            var request =
                Put(new RegisterTherapistRequest() { RegistrationId = registrationId, Therapist = Mapper.Map<UserDto>(therapist) });

            return request.Response == null ? 0 : request.Response.Id;
        }

        public IEnumerable<UserAccountQueryResult> GetUserList(String filter)
        {
            var service = UserAccountServiceFactory();
            var count = 0;
            var accounts = service.Query.Query(list =>
            {
                if (filter != null)
                {
                    list = list.Where(x => x.Username.Contains(filter));
                }
                return list;
            },
            null,
                //list => list.OrderBy(x=>x.Username),
            0, 1000, out count);

            return accounts;
        } 

        public static UserAccountService<SportsWebUser> UserAccountServiceFactory()
        {
            var database = new SportsWebMembershipRebootDatabase(WebPlatformConfigSettings.Instance.IdentityStore);
            var userRepo = new SportsWebUserAccountRepo(database);

            var configuration = new MembershipRebootConfiguration<SportsWebUser>
            {
                PasswordHashingIterationCount = 10000,
                RequireAccountVerification = false
            };

            var service = new UserAccountService<SportsWebUser>(configuration, userRepo);


            return service;
        }

        #endregion

    }

    public interface IUserManagementService : IDisposable
    {
        Boolean ValidateUserByEmail(String emailAddress);
        User GetUser(String subjectId);
        User GetServiceUser(String id);
        User Auth(String emailAddress, String hash);
        void AddFavorite(Favorite favorite, int userId);
        IEnumerable<Episode> GetEpisodes(int patientId, String state);
        String CreateServiceAccount(String subjectId);
        ClinicPatient ValidatePatientRegistration(String emailAddress, String pin, String subjectId);
        ClinicTherapist ValidateTherapistRegistration(String emailAddress, String pin, String subjectId);

        int RegisterPatient(User user, int registrationId);
        int RegisterTherapist(User user, int registrationId);
    }


}
