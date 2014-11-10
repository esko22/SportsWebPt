﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.Utilities.Security;
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
            Check.Argument.IsNotNullOrEmpty(request.Id, "Clinic Manager Id must be listed");

            var responseList = new List<ClinicDto>();
            var clinicAdminMatrixItems = ClinicUnitOfWork.GetClinicAdminMatrixList().Where(p => p.User.Hash == request.Id);
    
            Mapper.Map(clinicAdminMatrixItems.Select(s => s.Clinic), responseList);

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

            if (!userToAdd.AccountLinked)
                userToAdd = UserUnitOfWork.AddUser(new User());
            else
            {
                userToAdd =
                    UserUnitOfWork.UserRepository.GetAll()
                        .SingleOrDefault(
                            s => s.Hash.Equals(request.User.Hash, StringComparison.OrdinalIgnoreCase));
            }

            var clinicPatientMatrixItem =
                ClinicUnitOfWork.ClinicPatientRepository.GetAll()
                    .SingleOrDefault(p => p.ClinicId == request.IdAsInt && p.UserId == userToAdd.Id) ??
                ClinicUnitOfWork.AddPatientToClinic(request.IdAsInt, userToAdd.Id);

            //if token is returned, user is newly created or has not confirmed registration yet
            if (!clinicPatientMatrixItem.UserConfirmed)
            {
                var clinic = ClinicUnitOfWork.ClinicRepository.GetById(request.IdAsInt);
                using (var mailClient = new NetSmtpClient())
                {
                    var messageBody =
                        String.Format(
                            "Hello, you have requested by {0} to create an account on SportsWebPt to access your physical therapy information {1}", clinic.Name, Environment.NewLine);
                    messageBody = messageBody +
                                  String.Format(
                                      "Please click on the following link to start the registration process: http://localhost:8022/register/{0}/patient {1}",
                                      clinic.Id, Environment.NewLine);

                    messageBody = messageBody + Environment.NewLine + Environment.NewLine;
                    messageBody = messageBody +
                                  String.Format("You will need to following information to register: {0}",
                                      Environment.NewLine);
                    messageBody = messageBody +
                                  String.Format("Registration Email Given: {0}{1}",request.User.EmailAddress,Environment.NewLine);
                    messageBody = messageBody +
                                  String.Format("Registration Pin: {0}{1}", SymmetricCryptography.Encrypt(clinicPatientMatrixItem.Pin, request.User.EmailAddress), Environment.NewLine);

                    var mailMessage = new MailMessage {Subject = "SportsWebPt Patient Registration", Body = messageBody};
                    mailMessage.To.Add(new MailAddress(request.User.EmailAddress));
                    mailClient.Send(mailMessage);
                }
            }

            clinicPatientMatrixItem.Patient = userToAdd;
            return Ok(new ApiResponse<ClinicPatientDto>()
            {
                Response = Mapper.Map<ClinicPatientDto>(clinicPatientMatrixItem)
            });

        }

        public object Post(AddClinicTherapistRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsInt, "ClinicId");
            Check.Argument.IsNotNull(request.Therapist, "Therapist cannot be null");
            Check.Argument.IsNotNullOrEmpty(request.Therapist.EmailAddress, "Email Address cannot be empty");

            var userToAdd = Mapper.Map<User>(request.Therapist);

            if (!userToAdd.AccountLinked)
            {
                userToAdd = UserUnitOfWork.AddUser(new User());
                UserUnitOfWork.AddTherapist(userToAdd);
            }
            else
            {
                userToAdd =
                    UserUnitOfWork.UserRepository.GetUserDetails()
                        .SingleOrDefault(
                            s => s.Hash.Equals(request.Therapist.Hash, StringComparison.OrdinalIgnoreCase));
                if (userToAdd != null && userToAdd.Therapist == null)
                    UserUnitOfWork.AddTherapist(userToAdd);
            }


            var clinicTherapistMatrixItem =
                ClinicUnitOfWork.ClinicTherapistRepository.GetAll()
                    .SingleOrDefault(p => p.ClinicId == request.IdAsInt && p.TherapistId == userToAdd.Id) ??
                ClinicUnitOfWork.AddTherapistToClinic(request.IdAsInt, userToAdd.Id);


            if (!clinicTherapistMatrixItem.UserConfirmed)
            {
                var clinic = ClinicUnitOfWork.ClinicRepository.GetById(request.IdAsInt);
                using (var mailClient = new NetSmtpClient())
                {
                    var messageBody =
                        String.Format(
                            "Hello, you have requested by {0} to create an account on SportsWebPt to manage your patients and content. {1}", clinic.Name, Environment.NewLine);
                    messageBody = messageBody +
                                  String.Format(
                                      "Please click on the following link to start the registration process: http://localhost:8022/register/{0}/therapist {1}",
                                      clinic.Id, Environment.NewLine);

                    messageBody = messageBody + Environment.NewLine + Environment.NewLine;
                    messageBody = messageBody +
                                  String.Format("You will need to following information to register: {0}",
                                      Environment.NewLine);
                    messageBody = messageBody +
                                  String.Format("Registration Email Given: {0}{1}", request.Therapist.EmailAddress, Environment.NewLine);
                    messageBody = messageBody +
                                  String.Format("Registration Pin: {0}{1}", SymmetricCryptography.Encrypt(clinicTherapistMatrixItem.Pin, request.Therapist.EmailAddress), Environment.NewLine);

                    var mailMessage = new MailMessage { Subject = "SportsWebPt Therapist Registration", Body = messageBody };
                    mailMessage.To.Add(new MailAddress(request.Therapist.EmailAddress));
                    mailClient.Send(mailMessage);
                }
            }

            clinicTherapistMatrixItem.Therapist.User = userToAdd;
            return Ok(new ApiResponse<ClinicTherapistDto>()
            {
                Response = Mapper.Map<ClinicTherapistDto>(clinicTherapistMatrixItem)
            });
        }


        public object Get(ValidatePatientRegistrationRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Pin, "Pin Cannot Be Empty");
            Check.Argument.IsNotNullOrEmpty(request.EmailAddress, "Email Cannot Be Empty");
            Check.Argument.IsNotNullOrEmpty(request.ServiceAccount, "Service Account Cannot Be Empty");

            var clinicPatient = ClinicUnitOfWork.ValidateClinicPatient(request.EmailAddress, request.Pin, request.ServiceAccount);

            return Ok(new ApiResponse<ClinicPatientDto>()
            {
                Response = Mapper.Map<ClinicPatientDto>(clinicPatient)
            });

        }

        public object Get(ValidateTherapistRegistrationRequest request)
        {
            Check.Argument.IsNotNullOrEmpty(request.Pin, "Pin Cannot Be Empty");
            Check.Argument.IsNotNullOrEmpty(request.EmailAddress, "Email Cannot Be Empty");

            var clinicTherapist = ClinicUnitOfWork.ValidateClinicTherapist(request.EmailAddress, request.Pin, request.ServiceAccount);

            return Ok(new ApiResponse<ClinicTherapistDto>()
            {
                Response = Mapper.Map<ClinicTherapistDto>(clinicTherapist)
            });

        }


        #endregion

    }
}
