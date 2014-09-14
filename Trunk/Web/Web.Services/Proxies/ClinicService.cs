using System;
using System.Collections.Generic;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IClinicService
    {
        IEnumerable<Clinic> GetManagedClinics(int clinicManagerId);
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


        public IEnumerable<Clinic> GetManagedClinics(int clinicManagerId)
        {
            var request = GetSync(new ManagerClinicListRequest() { Id = clinicManagerId.ToString() });

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
            var request = PostSync(new AddClinicPatientRequest { Id = clinicId.ToString(), User = Mapper.Map<UserDto>(user) });

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        }

        public User AddTherapistToClinic(int clinicId, User user)
        {
            var request = PostSync(new AddClinicTherapistRequest { Id = clinicId.ToString(), Therapist = Mapper.Map<UserDto>(user) });

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        }



        #endregion
    }
}
