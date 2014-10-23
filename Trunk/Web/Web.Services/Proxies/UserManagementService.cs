using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;
using SportsWebPt.Common.ServiceStack;
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
            //var request = GetSync(new UserRequest() { Id = id} );

            //return request.Response == null ? null : Mapper.Map<User>(request.Response);

            var user = UserAccountServiceFactory().GetByID(new Guid(id));

            return new User();
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

        public IEnumerable<Episode> GetEpisodes(int patientId, String state)
        {
            EpisodeStateDto? episodeState = null;
            if (!String.IsNullOrEmpty(state))
                episodeState = (EpisodeStateDto)Enum.Parse(typeof(EpisodeStateDto), state, true);

            var request = GetSync(new PatientEpisodeListRequest() { Id = patientId.ToString(), State = episodeState });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Episode>>(request.Response.Items.OrderBy(p => p.CreatedOn));
        }

        public ClinicPatient ValidatePatientRegistration(String emailAddress, String pin)
        {
            var request = GetSync(new ValidatePatientRegistrationRequest() { EmailAddress = emailAddress, Pin = pin });

            return request.Response == null ? null : Mapper.Map<ClinicPatient>(request.Response);
        }

        public ClinicTherapist ValidateTherapistRegistration(String emailAddress, String pin)
        {
            var request = GetSync(new ValidateTherapistRegistrationRequest () { EmailAddress = emailAddress, Pin = pin });

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

        private static UserAccountService<RelationalUserAccount> UserAccountServiceFactory()
        {
            var userRepo = new DefaultUserAccountRepository(WebPlatformConfigSettings.Instance.IdentityStore);
            var configuration = new MembershipRebootConfiguration<RelationalUserAccount>
            {
                PasswordHashingIterationCount = 10000,
                RequireAccountVerification = false
            };

            return new UserAccountService<RelationalUserAccount>(configuration, userRepo);
        }

        #endregion

    }

    public interface IUserManagementService : IDisposable
    {
        User GetUser(String id);
        int AddUser(User user);
        User Auth(String emailAddress, String hash);
        void AddFavorite(Favorite favorite, int userId);
        IEnumerable<Episode> GetEpisodes(int patientId, String state);

        ClinicPatient ValidatePatientRegistration(String emailAddress, String pin);
        ClinicTherapist ValidateTherapistRegistration(String emailAddress, String pin);

        int RegisterPatient(User user, int registrationId);
        int RegisterTherapist(User user, int registrationId);
    }


}
