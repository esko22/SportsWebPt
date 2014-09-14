using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

            var token = ClinicUnitOfWork.AddPatientToClinic(request.IdAsInt, userToAdd.Id);

            using (var mailClient = new NetSmtpClient())
            {
                var mailMessage = new MailMessage();
                if (!String.IsNullOrWhiteSpace(token))
                {
                    mailMessage.Subject = "SportsWebPt Patient Registration";
                    mailMessage.Body = String.Format("Register here: {0}", token);
                }
                else
                {
                    mailMessage.Subject = "New Clinic Confirmation Request";
                    mailMessage.Body = String.Format("Login to SportsWebPt to see your new clinic request");
                }

                mailMessage.To.Add(new MailAddress(userToAdd.EmailAddress));
                mailClient.Send(mailMessage);
            }

            return Ok(new ApiResponse<UserDto>()
            {
                Response = Mapper.Map<UserDto>(userToAdd)
            });

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

            var token = ClinicUnitOfWork.AddTherapistToClinic(request.IdAsInt, userToAdd.Id);

            using (var mailClient = new NetSmtpClient())
            {
                var mailMessage = new MailMessage();
                if (!String.IsNullOrWhiteSpace(token))
                {
                    mailMessage.Subject = "SportsWebPt Therapist Registration";
                    mailMessage.Body = String.Format("Register here: {0}", token);
                }
                else
                {
                    mailMessage.Subject = "New Clinic Confirmation Request";
                    mailMessage.Body = String.Format("Login to SportsWebPt to see your new clinic request");
                }

                mailMessage.To.Add(new MailAddress(userToAdd.EmailAddress));
                mailClient.Send(mailMessage);
            }

            return Ok(new ApiResponse<UserDto>()
            {
                Response = Mapper.Map<UserDto>(userToAdd)
            });
        }


        public object Get(ValidatePatientRegistrationRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Pin, "Pin Cannot Be Empty");
            Check.Argument.IsNotNullOrEmpty(request.EmailAddress, "Email Cannot Be Empty");

            var clinicPatient = ClinicUnitOfWork.ValidateClinicPatient(request.EmailAddress, request.Pin);

            return Ok(new ApiResponse<ClinicPatientDto>()
            {
                Response = Mapper.Map<ClinicPatientDto>(clinicPatient)
            });

        }

        public object Get(ValidateTherapistRegistrationRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Pin, "Pin Cannot Be Empty");
            Check.Argument.IsNotNullOrEmpty(request.EmailAddress, "Email Cannot Be Empty");

            var clinicTherapist = ClinicUnitOfWork.ValidateClinicTherapist(request.EmailAddress, request.Pin);

            return Ok(new ApiResponse<ClinicTherapistDto>()
            {
                Response = Mapper.Map<ClinicTherapistDto>(clinicTherapist)
            });

        }

        public object Put(RegisterPatientRequest request)
        {
            var clinicPatient = ClinicUnitOfWork.GetClinicPatients()
                .SingleOrDefault(p => p.Id == request.RegistrationId);

            var newUser = Mapper.Map<User>(request.User);

            if (clinicPatient != null)
            {
                var existingUser = UserUnitOfWork.GetUserByEmail(newUser.EmailAddress);

                if (existingUser != null)
                {
                    if (existingUser.EmailAddress != clinicPatient.Patient.EmailAddress)
                        return Ok(new ApiResponse<UserDto>() {Response = new UserDto() {Id = 0}});
                }
                else
                    existingUser = clinicPatient.Patient;

                newUser = UserUnitOfWork.UpdateUser(newUser, existingUser.Id);
                ClinicUnitOfWork.SetPatientConfirmation(request.RegistrationId);
            }

            return Ok(new ApiResponse<UserDto>()
            {
                Response = Mapper.Map<UserDto>(newUser)
            });
        }

        public object Put(RegisterTherapistRequest request)
        {
            var clinicTherapist = ClinicUnitOfWork.GetClinicTherapists()
                .SingleOrDefault(p => p.Id == request.RegistrationId);

            var newUser = Mapper.Map<User>(request.Therapist);

            if (clinicTherapist != null)
            {
                var existingUser = UserUnitOfWork.GetUserByEmail(newUser.EmailAddress);

                if (existingUser != null)
                {
                    if (existingUser.EmailAddress != clinicTherapist.Therapist.User.EmailAddress)
                        return Ok(new ApiResponse<UserDto>() {Response = new UserDto() {Id = 0}});
                }
                else
                    existingUser = clinicTherapist.Therapist.User;

                newUser = UserUnitOfWork.UpdateUser(newUser, existingUser.Id);
                ClinicUnitOfWork.SetTherapistConfirmation(request.RegistrationId);
            }

            return Ok(new ApiResponse<UserDto>()
            {
                Response = Mapper.Map<UserDto>(newUser)
            });
        }


        #endregion

    }
}
