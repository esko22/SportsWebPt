using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class ClinicService : RestService
    {
        #region Properties

        public IClinicUnitOfWork ClinicUnitOfWork { get; set; }
        public IUserUnitOfWork UserUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(ManagerClinicListRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "Clinic Manager Id must be listed");

            var responseList = new List<ClinicDto>();
            var clinicAdmin = ClinicUnitOfWork.GetClinicAdminList().SingleOrDefault(p => p.Id == request.IdAsInt);
    
            if(clinicAdmin != null)
                Mapper.Map(clinicAdmin.ClinicAdminMatrixItems.Select(s => s.Clinic), responseList);

            return
                Ok(new ApiListResponse<ClinicDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(ClinicPatientListRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "Clinic Id must be listed");

            var responseList = new List<UserDto>();
            var patients = ClinicUnitOfWork.GetClinicPatients().Where(p => p.ClinicId == request.IdAsInt);

            Mapper.Map(patients.Select(s => s.Patient), responseList);

            return
                Ok(new ApiListResponse<UserDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(ClinicTherapistListRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "Clinic Id must be listed");

            var responseList = new List<TherapistDto>();
            var therapists = ClinicUnitOfWork.GetClinicTherapists().Where(p => p.ClinicId == request.IdAsInt).ToList();

            Mapper.Map(therapists.Select(s => s.Therapist), responseList);

            return
                Ok(new ApiListResponse<TherapistDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(ClinicRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "Clinic Id must be listed");

            var clinic = ClinicUnitOfWork.ClinicRepository.GetById(request.IdAsInt);

            if (clinic == null)
                return NotFound("Clinic Not Found");

            return Ok(new ApiResponse<ClinicDto>()
            {
                Response = Mapper.Map<ClinicDto>(clinic)
            });
        }

        public object Post(AddClinicPatientRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "ClinicId" );
            Check.Argument.IsNotNull(request.User, "User cannot be null");
            Check.Argument.IsNotNullOrEmpty(request.User.EmailAddress, "Email Address cannot be empty");

            var userToAdd = Mapper.Map<User>(request.User);

            if (userToAdd.Id == 0)
                userToAdd = UserUnitOfWork.AddUser(userToAdd);

            ClinicUnitOfWork.AddPatientToClinic(request.IdAsInt, userToAdd.Id);

            return userToAdd;
        }

        public object Post(AddClinicTherapistRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "ClinicId");
            Check.Argument.IsNotNull(request.Therapist, "Therapist cannot be null");
            Check.Argument.IsNotNullOrEmpty(request.Therapist.EmailAddress, "Email Address cannot be empty");

            var userToAdd = Mapper.Map<User>(request.Therapist);

            if (userToAdd.Id == 0)
                userToAdd = UserUnitOfWork.AddUser(userToAdd);

            if (UserUnitOfWork.GetTherapistById(userToAdd.Id) == null)
                UserUnitOfWork.AddTherapist(userToAdd);

            ClinicUnitOfWork.AddTherapistToClinic(request.IdAsInt, userToAdd.Id);

            return userToAdd;
        }


        #endregion

    }
}
