﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.Utilities.Security;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ClinicUnitOfWork : BaseUnitOfWork, IClinicUnitOfWork
    {
        #region Properties

        public IRepository<ClinicAdminMatrixItem> ClinicAdminMatrixRepository { get { return GetStandardRepo<ClinicAdminMatrixItem>(); } }
        public IRepository<ClinicTherapistMatrixItem> ClinicTherapistRepository { get { return GetStandardRepo<ClinicTherapistMatrixItem>(); } }
        public IRepository<ClinicPatientMatrixItem> ClinicPatientRepository { get { return GetStandardRepo<ClinicPatientMatrixItem>(); } }
        public IRepository<User> UserRepository { get { return GetStandardRepo<User>(); } } 
        public IClinicRepo ClinicRepository { get { return GetRepo<IClinicRepo>(); } }
        public IRepository<Episode> EpisodeRepository { get { return GetStandardRepo<Episode>(); } } 

        #endregion

        #region Construction

        public ClinicUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion


        #region Methods

        public IQueryable<ClinicAdminMatrixItem> GetClinicAdminMatrixList()
        {
            return ClinicAdminMatrixRepository.GetAll()
                .Include(i => i.Clinic).Include(i => i.User);
        }

        public IQueryable<ClinicTherapistMatrixItem> GetClinicTherapists()
        {
            return ClinicTherapistRepository.GetAll()
                .Include(i => i.Therapist.User);
        }

        public IQueryable<ClinicPatientMatrixItem> GetClinicPatients()
        {
            return ClinicPatientRepository.GetAll()
                .Include(i => i.Patient);
        }

        public ClinicPatientMatrixItem AddPatientToClinic(int clinicId, int userId)
        {
            var clinicPatientMatrixItem = new ClinicPatientMatrixItem()
            {
                ClinicId = clinicId,
                UserId = userId,
                Pin = Guid.NewGuid().ToString(),
                AddedOn = DateTime.Now
            };
            ClinicPatientRepository.Add(clinicPatientMatrixItem);
            Commit();

            return clinicPatientMatrixItem;
        }

        public ClinicTherapistMatrixItem AddTherapistToClinic(int clinicId, int therapistId)
        {
            var clinicTherapistMatrixItem = new ClinicTherapistMatrixItem()
            {
                ClinicId = clinicId,
                TherapistId = therapistId,
                Pin = Guid.NewGuid().ToString(),
                AddedOn = DateTime.Now
            };
            ClinicTherapistRepository.Add(clinicTherapistMatrixItem);
            Commit();

            return clinicTherapistMatrixItem;
        }

        public ClinicPatientMatrixItem ValidateClinicPatient(String emailAddress, String encryptedPin, String serviceAccount)
        {
            var pin = SymmetricCryptography.Decrypt(encryptedPin, emailAddress);

            var clinicPatient = ClinicPatientRepository.GetAll()
                .Include(i => i.Patient)
                .Include(i => i.Clinic)
                .SingleOrDefault(
                    p => p.Pin.Equals(pin, StringComparison.OrdinalIgnoreCase));

            if (clinicPatient != null)
            {
                clinicPatient.UserConfirmed = true;
                ClinicPatientRepository.Update(clinicPatient);

                var user = UserRepository.GetById(clinicPatient.UserId);
                if (!user.Hash.Equals(serviceAccount, StringComparison.OrdinalIgnoreCase))
                {
                    user.AccountLinked = true;
                    //TODO: this remapping / drop is ghetto but I am at a loss right now...
                    //there is a chance that a user can create an account after the clinic AddsPatient, 
                    //that creates an account for the session to move forward, user could create account without registration mapping because there is no PHI with user account in service 
                    //to sync them up... if they go through registration process after, this will associate the accounts together
                    AssocaiteExistingServiceAccounts(serviceAccount, clinicPatient.Patient.Hash);
                }

                Commit();
            }

            return clinicPatient;
        }

        private void AssocaiteExistingServiceAccounts(String mapToAccount, String mapFromAccount)
        {
            var mapToUser = UserRepository.GetAll().SingleOrDefault(s => s.Hash.Equals(mapToAccount,StringComparison.OrdinalIgnoreCase));
            var mapFromUser = UserRepository.GetAll().SingleOrDefault(s => s.Hash.Equals(mapFromAccount,StringComparison.OrdinalIgnoreCase));

            Check.Argument.IsNotNull(mapToUser, "MapToUser");
            Check.Argument.IsNotNull(mapFromUser, "MapFromUser");

            ClinicPatientRepository.GetAll().Where(p => p.UserId == mapFromUser.Id).ForEach(f =>
            {
                f.Patient = mapToUser;
                ClinicPatientRepository.Update(f);
            });

            EpisodeRepository.GetAll().Where(p => p.PatientId == mapFromUser.Id).ForEach(f =>
            {
                f.Patient = mapToUser;
                EpisodeRepository.Update(f);
            });

            Commit();

            UserRepository.Delete(mapFromUser);
        }

        public ClinicTherapistMatrixItem ValidateClinicTherapist(String emailAddress, String encryptedPin, String serviceAccount)
        {
            var pin = SymmetricCryptography.Decrypt(encryptedPin, emailAddress);

            var clinicTherapist = ClinicTherapistRepository.GetAll()
                .Include(i => i.Therapist.User)
                .Include(i => i.Clinic)
                .SingleOrDefault(
                    p => p.Pin.Equals(pin, StringComparison.OrdinalIgnoreCase));

            if (clinicTherapist != null)
            {
                clinicTherapist.UserConfirmed = true;
                ClinicTherapistRepository.Update(clinicTherapist);

                var user = UserRepository.GetById(clinicTherapist.TherapistId);
                if (!user.Hash.Equals(serviceAccount, StringComparison.OrdinalIgnoreCase))
                {
                    user.AccountLinked = true;
                    //TODO: this remapping / drop is ghetto but I am at a loss right now...
                    //there is a chance that a user can create an account after the clinic AddsPatient, 
                    //that creates an account for the session to move forward, user could create account without registration mapping because there is no PHI with user account in service 
                    //to sync them up... if they go through registration process after, this will associate the accounts together
                    AssocaiteExistingTherapistServiceAccounts(serviceAccount, clinicTherapist.Therapist.User.Hash);
                }

                Commit();
            }

            return clinicTherapist;
        }

        private void AssocaiteExistingTherapistServiceAccounts(String mapToAccount, String mapFromAccount)
        {
            var mapToUser = UserRepository.GetAll().SingleOrDefault(s => s.Hash.Equals(mapToAccount, StringComparison.OrdinalIgnoreCase));
            var mapFromUser = UserRepository.GetAll().SingleOrDefault(s => s.Hash.Equals(mapFromAccount, StringComparison.OrdinalIgnoreCase));

            Check.Argument.IsNotNull(mapToUser, "MapToUser");
            Check.Argument.IsNotNull(mapFromUser, "MapFromUser");

            ClinicTherapistRepository.GetAll().Where(p => p.TherapistId == mapFromUser.Id).ForEach(f =>
            {
                f.Therapist.User = mapToUser;
                ClinicTherapistRepository.Update(f);
            });

            Commit();

            UserRepository.Delete(mapFromUser);
        }

        public void SetPatientConfirmation(int clientPatientMatrixId)
        {
            var clinicPatient = ClinicPatientRepository.GetById(clientPatientMatrixId);

            if (clinicPatient != null)
            {
                clinicPatient.UserConfirmed = true;
                ClinicPatientRepository.Update(clinicPatient);
                Commit();
            }
        }

        public void SetTherapistConfirmation(int clientTherapistMatrixId)
        {
            var clinicTherapist = ClinicTherapistRepository.GetById(clientTherapistMatrixId);

            if (clinicTherapist != null)
            {
                clinicTherapist.UserConfirmed = true;
                ClinicTherapistRepository.Update(clinicTherapist);
                Commit();
            }
        }

        #endregion

    }

    public interface IClinicUnitOfWork : IBaseUnitOfWork
    {
        IClinicRepo ClinicRepository { get; }
        IRepository<ClinicTherapistMatrixItem> ClinicTherapistRepository { get; }
        IRepository<ClinicPatientMatrixItem> ClinicPatientRepository { get; }

        IQueryable<ClinicTherapistMatrixItem> GetClinicTherapists();
        IQueryable<ClinicPatientMatrixItem> GetClinicPatients();
        IQueryable<ClinicAdminMatrixItem> GetClinicAdminMatrixList();

        ClinicPatientMatrixItem AddPatientToClinic(int clinicId, int userId);
        ClinicTherapistMatrixItem AddTherapistToClinic(int clinicId, int therapistId);
        ClinicPatientMatrixItem ValidateClinicPatient(String emailAddress, String pin, String subjectId);
        ClinicTherapistMatrixItem ValidateClinicTherapist(String emailAddress, String pin, String subjectId);
        void SetPatientConfirmation(int clientPatientMatrixId);
        void SetTherapistConfirmation(int clientTherapistMatrixId);

    }
}
