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
            Check.Argument.IsNotNullOrEmpty(request.Id, "Clinic Manager Id must be listed");

            var responseList = new List<ClinicDto>();
            var clinicAdmin = ClinicUnitOfWork.GetClinicAdminList().SingleOrDefault(p => p.User.Hash == request.Id);
    
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

            if (!userToAdd.AccountLinked)
                userToAdd = UserUnitOfWork.AddUser(new User());
            else
            {
                userToAdd =
                    UserUnitOfWork.UserRepository.GetAll()
                        .SingleOrDefault(
                            s => s.Hash.Equals(request.User.Hash, StringComparison.OrdinalIgnoreCase));
            }

            var clinic = ClinicUnitOfWork.ClinicRepository.GetById(request.IdAsInt);

            var token = ClinicUnitOfWork.AddPatientToClinic(request.IdAsInt, userToAdd.Id, request.User.EmailAddress);
            //if token is returned, user is newly created or has not confirmed registration yet
            if (!String.IsNullOrWhiteSpace(token))
            {
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
                                  String.Format("Registration Pin: {0}{1}", token, Environment.NewLine);

                    var mailMessage = new MailMessage {Subject = "SportsWebPt Patient Registration", Body = messageBody};
                    mailMessage.To.Add(new MailAddress(request.User.EmailAddress));
                    mailClient.Send(mailMessage);
                }
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

                mailMessage.To.Add(new MailAddress(request.Therapist.EmailAddress));
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

            var clinicTherapist = ClinicUnitOfWork.ValidateClinicTherapist(request.EmailAddress, request.Pin);

            return Ok(new ApiResponse<ClinicTherapistDto>()
            {
                Response = Mapper.Map<ClinicTherapistDto>(clinicTherapist)
            });

        }


        #endregion

    }
}
