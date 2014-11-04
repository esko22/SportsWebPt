﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using BrockAllen.MembershipReboot;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IClinicService
    {
        IEnumerable<Clinic> GetManagedClinics(String clinicManagerId);
        IEnumerable<User> GetClinicPatients(int clinicId);
        IEnumerable<Therapist> GetClinicTherapists(int clinicId);
        Clinic GetClinic(int clinicId);
        User AddPatientToClinic(int clinicId, User user);
        User AddTherapistToClinic(int clinicId, User user);

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

        public IEnumerable<User> GetClinicPatients(int clinicId)
        {
            var request = GetSync(new ClinicPatientListRequest() { Id = clinicId.ToString() });

            return request.Response == null ? null : Mapper.Map<IEnumerable<User>>(request.Response.Items);
        }

        public IEnumerable<Therapist> GetClinicTherapists(int clinicId)
        {
            var request = GetSync(new ClinicTherapistListRequest() { Id = clinicId.ToString() });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Therapist>>(request.Response.Items);
        }

        public User AddPatientToClinic(int clinicId, User user)
        {
            return AddUserToClinic(clinicId, user, new AddClinicPatientRequest { Id = clinicId.ToString(), User = Mapper.Map<UserDto>(user) });
        }

        public User AddTherapistToClinic(int clinicId, User user)
        {
            return AddUserToClinic(clinicId, user, new AddClinicTherapistRequest { Id = clinicId.ToString(), Therapist = Mapper.Map<UserDto>(user) });
        }

        private User AddUserToClinic(int clinicId, User user, IReturn<ApiResponse<UserDto>> requestDto)
        {
            var userService = UserManagementService.UserAccountServiceFactory();
            var userToAdd = userService.GetByEmail(user.emailAddress);
            if (userToAdd != null && userToAdd.HasClaim("service_account"))
            {
                user.hash = userToAdd.GetClaimValue("service_account");
                user.accountLinked = true;
            }

            var request = PostSync(requestDto);
            
            if (userToAdd != null && !user.accountLinked)
            {
                userService.AddClaim(userToAdd.ID, "service_account", request.Response.Hash);
                userToAdd.ServiceAccount = request.Response.Hash;
                userService.Update(userToAdd);
            }

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        } 



        #endregion
    }
}
