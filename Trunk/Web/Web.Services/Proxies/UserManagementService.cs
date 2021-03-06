﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

using AutoMapper;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Logging;
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
        private static readonly ILog _logger = LogManager.GetCommonLogger();

        #endregion
       

        #region Methods

        public UserManagementService(SportsWebPtClientSettings clientSettings) 
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        public User GetUser(String id)
        {
            _logger.Debug(String.Format("Attempt to get user {0}",id));
            var relationUser = UserAccountServiceFactory().GetByID(new Guid(id));

            if(relationUser == null)
                throw new Exception("User does not exist");

            var user = new User
            {
                id = relationUser.ServiceAccount,
                emailAddress = relationUser.Email,
                isAdmin = relationUser.HasClaim("role","admin"),
                isClinicManager = relationUser.HasClaim("role","manager"),
                isTherapist = relationUser.HasClaim("role","therapist")
            };

            return user;
        }

        public Boolean ValidateUserByEmail(String emailAddress)
        {
            return UserAccountServiceFactory().GetByEmail(emailAddress) != null;
        }

        public User GetServiceUser(String id)
        {
            var request = GetSync(new UserRequest() { Id = id });

            return request.Response == null ? new User() { id = id, accountLinked = false} : Mapper.Map<User>(request.Response);
        }

        public String AddUser(User user)
        {
            var userResuest = Mapper.Map<CreateUserRequest>(user);
   
            var request = PostSync(userResuest);

            return request.Response.Id;
        }

        public String CreateServiceAccount(String subjectId)
        {
            var userAccountService = UserAccountServiceFactory();
            var user = userAccountService.GetByID(new Guid(subjectId));
            var serviceAccount = user.ServiceAccount;

            if (String.IsNullOrEmpty(serviceAccount))
            {
                _logger.Info(String.Format("Creating Service Account for {0}", subjectId));

                var request = PostSync(new CreateUserRequest {AccountLinked = true});
                userAccountService.AddClaim(new Guid(subjectId), "service_account", request.Response.Id);
                UpdateServiceAccount(subjectId, request.Response.Id, userAccountService);

                serviceAccount = request.Response.Id;
            }

            return serviceAccount;
        }

        private void UpdateServiceAccount(String subjectId, String serviceAccount, UserAccountService<SportsWebUser> userAccountService)
        {
            var userToEdit = userAccountService.GetByID(new Guid(subjectId));
            if (userToEdit != null)
            {
                _logger.Info(String.Format("Updating Service Account for {0}", subjectId));
                userToEdit.ServiceAccount = serviceAccount;
                userAccountService.Update(userToEdit);
            }
        }

        public void AddFavorite(Favorite userFavorite, String userId)
        {
            var userFavRequest = Mapper.Map<CreateUserFavoriteRequest>(userFavorite);
            userFavRequest.Id = userId;

            PostSync(userFavRequest);
        }

        public IEnumerable<Case> GetCases(String patientId, String state)
        {
            CaseStateDto? caseState = null;
            if (!String.IsNullOrEmpty(state))
                caseState = (CaseStateDto)Enum.Parse(typeof(CaseStateDto), state, true);

            var request = GetSync(new PatientCaseListRequest() { Id = patientId, State = caseState });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Case>>(request.Response.Items.OrderBy(p => p.CreatedOn));
        }

        public PatientSnapshot GetPatientSnapshot(String patientId)
        {
            var request = GetSync(new PatientSnapshotRequest() { Id = patientId});

            return request.Response == null ? null : Mapper.Map<PatientSnapshot>(request.Response);
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

        public String RegisterPatient(User user, int registrationId)
        {
            var request =
                Put(new RegisterPatientRequest() {RegistrationId = registrationId, User = Mapper.Map<UserDto>(user)});

            return request.Response == null ? String.Empty : request.Response.Id;
        }

        public String RegisterTherapist(User therapist, int registrationId)
        {
            var request =
                Put(new RegisterTherapistRequest() { RegistrationId = registrationId, Therapist = Mapper.Map<UserDto>(therapist) });

            return request.Response == null ? String.Empty : request.Response.Id;
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

        public void SetUserDetailsByExternalAccounts(IEnumerable<User> externalAccounts)
        {
            var distinctExternalAccounts = externalAccounts.Select(s => s.id).Distinct();
            using (
                var database = new SportsWebMembershipRebootDatabase(WebPlatformConfigSettings.Instance.IdentityStore))
            {
                var userRepo = new SportsWebUserAccountRepo(database);

                foreach (var identityUser in userRepo.GetUserDetailsByExternalAccount(distinctExternalAccounts))
                {
                    var identityName = identityUser.GetClaimValue("name");
                    foreach (var clinicPatient in externalAccounts.Where(s => s.id.Equals(identityUser.ServiceAccount)))
                    {
                        clinicPatient.emailAddress = identityUser.Email;
                        clinicPatient.fullName = identityName;
                    }
                }
            }
        }

        public IEnumerable<User> GetUserDetailsByExternalAccounts(IEnumerable<String> externalAccounts)
        {
            var users = new List<User>();
            using (
                var database = new SportsWebMembershipRebootDatabase(WebPlatformConfigSettings.Instance.IdentityStore))
            {
                var userRepo = new SportsWebUserAccountRepo(database);

                Mapper.Map(userRepo.GetUserDetailsByExternalAccount(externalAccounts), users);

            }

            return users;
        }

        public User GetUserByServiceAccountId(String serviceAccountId)
        {
            return GetUserDetailsByExternalAccounts(new[] {serviceAccountId}).FirstOrDefault();
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
        void AddFavorite(Favorite favorite, String userId);
        IEnumerable<Case> GetCases(String patientId, String state);
        String CreateServiceAccount(String subjectId);
        ClinicPatient ValidatePatientRegistration(String emailAddress, String pin, String subjectId);
        ClinicTherapist ValidateTherapistRegistration(String emailAddress, String pin, String subjectId);
        void SetUserDetailsByExternalAccounts(IEnumerable<User> externalAccounts);
        IEnumerable<User> GetUserDetailsByExternalAccounts(IEnumerable<String> externalAccounts);
        User GetUserByServiceAccountId(String serviceAccountId);

        String RegisterPatient(User user, int registrationId);
        String RegisterTherapist(User user, int registrationId);
        PatientSnapshot GetPatientSnapshot(String patientId);
    }


}
