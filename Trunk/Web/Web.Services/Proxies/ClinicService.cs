using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BrockAllen.MembershipReboot;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IClinicService
    {
        IEnumerable<Clinic> GetManagedClinics(String clinicManagerId);
        IEnumerable<ClinicPatient> GetClinicPatients(int clinicId);
        IEnumerable<ClinicTherapist> GetClinicTherapists(int clinicId);
        Clinic GetClinic(int clinicId);
        ClinicPatient AddPatientToClinic(int clinicId, User user);
        ClinicTherapist AddTherapistToClinic(int clinicId, User user);
    }

    public class ClinicService : BaseServiceStackClient, IClinicService
    {
         #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion

        #region Construction

        public ClinicService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion

        #region Methods

        public Clinic GetClinic(int clinicId)
        {
            var request = GetSync(new ClinicRequest() { Id = clinicId.ToString() });

            return request.Response == null ? null : Mapper.Map<Clinic>(request.Response);
        }


        public IEnumerable<Clinic> GetManagedClinics(String clinicManagerId)
        {
            var request = GetSync(new ManagerClinicListRequest() { Id = clinicManagerId });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Clinic>>(request.Response.Items);
        }

        public IEnumerable<ClinicPatient> GetClinicPatients(int clinicId)
        {
            var request = GetSync(new ClinicPatientListRequest() { Id = clinicId.ToString() });

            return request.Response == null ? null : Mapper.Map<IEnumerable<ClinicPatient>>(request.Response.Items);
        }

        public IEnumerable<ClinicTherapist> GetClinicTherapists(int clinicId)
        {
            var request = GetSync(new ClinicTherapistListRequest() { Id = clinicId.ToString() });

            return request.Response == null ? null : Mapper.Map<IEnumerable<ClinicTherapist>>(request.Response.Items);
        }

        public ClinicPatient AddPatientToClinic(int clinicId, User user)
        {
            var userService = UserManagementService.UserAccountServiceFactory(); 
            var userToAdd = userService.GetByEmail(user.emailAddress);
            if (userToAdd != null && userToAdd.HasClaim("service_account"))
            {
                user.id = userToAdd.GetClaimValue("service_account");
                user.accountLinked = true;
            }

            var request = PostSync(new AddClinicPatientRequest { Id = clinicId.ToString(), User = Mapper.Map<UserDto>(user) });

            if (userToAdd != null && !user.accountLinked)
            {
                userService.AddClaim(userToAdd.ID, "service_account", request.Response.User.Id);
                userToAdd.ServiceAccount = request.Response.User.Id;
                userService.Update(userToAdd);
            }

            return request.Response == null ? null : Mapper.Map<ClinicPatient>(request.Response);
        }

        public ClinicTherapist AddTherapistToClinic(int clinicId, User user)
        {
            var userService = UserManagementService.UserAccountServiceFactory();
            var userToAdd = userService.GetByEmail(user.emailAddress);
            if (userToAdd != null && userToAdd.HasClaim("service_account"))
            {
                user.id = userToAdd.GetClaimValue("service_account");
                user.accountLinked = true; 
                
                if (!userToAdd.HasClaim("role", "therapist"))
                    userService.AddClaim(userToAdd.ID, "role", "therapist");
            }

            var request = PostSync(new AddClinicTherapistRequest { Id = clinicId.ToString(), Therapist = Mapper.Map<UserDto>(user) });

            if (userToAdd != null && !user.accountLinked)
            {
                userService.AddClaim(userToAdd.ID, "service_account", request.Response.Therapist.Id);
                userService.AddClaim(userToAdd.ID, "role", "therapist");
                userService.Update(userToAdd);

                userToAdd.ServiceAccount = request.Response.Therapist.Id;
            }

            return request.Response == null ? null : Mapper.Map<ClinicTherapist>(request.Response);
        }

        #endregion
    }
}
