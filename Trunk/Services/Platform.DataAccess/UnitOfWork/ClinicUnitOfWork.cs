﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities.Security;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ClinicUnitOfWork : BaseUnitOfWork, IClinicUnitOfWork
    {
        #region Properties

        public IRepository<ClinicAdmin> ClinicAdminRepository { get { return GetStandardRepo<ClinicAdmin>(); } }
        public IRepository<ClinicTherapistMatrixItem> ClinicTherapistRepository { get { return GetStandardRepo<ClinicTherapistMatrixItem>(); } }
        public IRepository<ClinicPatientMatrixItem> ClinicPatientRepository { get { return GetStandardRepo<ClinicPatientMatrixItem>(); } }
        public IRepository<User> UserRepository { get { return GetStandardRepo<User>(); } } 
        public IClinicRepo ClinicRepository { get { return GetRepo<IClinicRepo>(); } } 

        #endregion

        #region Construction

        public ClinicUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion


        #region Methods

        public IQueryable<ClinicAdmin> GetClinicAdminList()
        {
            return ClinicAdminRepository.GetAll()
                .Include(i => i.ClinicAdminMatrixItems.Select(l2 => l2.Clinic));
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

        public String AddPatientToClinic(int clinicId, int userId, string emailAddress)
        {
            var clinicPatientMatrixItem =
                ClinicPatientRepository.GetAll().SingleOrDefault(p => p.ClinicId == clinicId && p.UserId == userId);

            if (clinicPatientMatrixItem == null)
            {
                clinicPatientMatrixItem = new ClinicPatientMatrixItem()
                {
                    ClinicId = clinicId,
                    UserId = userId,
                    Pin = Guid.NewGuid().ToString()
                };
                ClinicPatientRepository.Add(clinicPatientMatrixItem);
                Commit();
            }

            return !clinicPatientMatrixItem.UserConfirmed ? SymmetricCryptography.Encrypt(clinicPatientMatrixItem.Pin, emailAddress) : String.Empty;
        }

        public String AddTherapistToClinic(int clinicId, int therapistId)
        {
            var clinicTherapistMatrixItem =
                ClinicTherapistRepository.GetAll()
                    .SingleOrDefault(p => p.ClinicId == clinicId && p.TherapistId == therapistId);
   
            if (clinicTherapistMatrixItem == null)
            {
                clinicTherapistMatrixItem = new ClinicTherapistMatrixItem()
                {
                    ClinicId = clinicId,
                    TherapistId = therapistId,
                    Pin = Guid.NewGuid().ToString()
                };
                ClinicTherapistRepository.Add(clinicTherapistMatrixItem);
                Commit();

                return clinicTherapistMatrixItem.Pin;
            }

            return String.Empty;
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
                    AssocaiteExistingServiceAccounts(clinicPatient.Patient.Hash, serviceAccount);
                }

                Commit();
            }

            return clinicPatient;
        }

        private void AssocaiteExistingServiceAccounts(String mapToAccount, String mapFromAccount)
        {
            var user = UserRepository.GetAll().SingleOrDefault(s => s.Hash.Equals(mapFromAccount,StringComparison.OrdinalIgnoreCase));
            UserRepository.Delete(user);
        }

        public ClinicTherapistMatrixItem ValidateClinicTherapist(String emailAddress, String pin)
        {
            return ClinicTherapistRepository.GetAll()
                .Include(i => i.Therapist.User)
                .Include(i => i.Clinic)
                .SingleOrDefault(
                    p =>
                        p.Pin.Equals(pin, StringComparison.OrdinalIgnoreCase));
            //TODO: need to think about case sensitivity for security
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

        IQueryable<ClinicAdmin> GetClinicAdminList();
        IQueryable<ClinicTherapistMatrixItem> GetClinicTherapists();
        IQueryable<ClinicPatientMatrixItem> GetClinicPatients();

        String AddPatientToClinic(int clinicId, int userId, String emailAddress);
        String AddTherapistToClinic(int clinicId, int therapistId);
        ClinicPatientMatrixItem ValidateClinicPatient(String emailAddress, String pin, String subjectId);
        ClinicTherapistMatrixItem ValidateClinicTherapist(String emailAddress, String pin);
        void SetPatientConfirmation(int clientPatientMatrixId);
        void SetTherapistConfirmation(int clientTherapistMatrixId);

    }
}