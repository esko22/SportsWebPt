using System;
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
        public IRepository<Therapist> TherapistRepository { get { return GetStandardRepo<Therapist>(); } } 
        public IRepository<Case> CaseRepository { get { return GetStandardRepo<Case>(); } } 

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

        public ClinicPatientMatrixItem AddPatientToClinic(int clinicId, Guid userId)
        {
            var clinic = ClinicRepository.GetById(clinicId);
            var patientIdentifier = String.Format("{0}-P{1}", clinic.Name.Substring(0, 1),
                    GetClinicPatients().Count(c => c.ClinicId == clinicId) + 1);

            var clinicPatientMatrixItem = new ClinicPatientMatrixItem()
            {
                ClinicId = clinicId,
                UserId = userId,
                Pin = Guid.NewGuid().ToString(),
                AddedOn = DateTime.Now,
                ClinicPatientIdentifier = patientIdentifier
            };
            ClinicPatientRepository.Add(clinicPatientMatrixItem);
            Commit();

            return clinicPatientMatrixItem;
        }

        public ClinicTherapistMatrixItem AddTherapistToClinic(int clinicId, Guid therapistId)
        {
            var clinic = ClinicRepository.GetById(clinicId);
            var therapistIdentifier = String.Format("{0}-T{1}", clinic.Name.Substring(0, 1),
                GetClinicTherapists().Count(c => c.ClinicId == clinicId) + 1);

            var clinicTherapistMatrixItem = new ClinicTherapistMatrixItem()
            {
                ClinicId = clinicId,
                TherapistId = therapistId,
                Pin = Guid.NewGuid().ToString(),
                AddedOn = DateTime.Now,
                ClinicTherapistIdentifier = therapistIdentifier
            };
            ClinicTherapistRepository.Add(clinicTherapistMatrixItem);
            Commit();

            return clinicTherapistMatrixItem;
        }

        public ClinicPatientMatrixItem ValidateClinicPatient(String emailAddress, String encryptedPin, Guid serviceAccount)
        {
            _logger.Info(String.Format("Validate Clinic Patient For {0} - {1}", emailAddress, serviceAccount));

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

                var user = UserRepository.GetAll().SingleOrDefault(s => s.Id == clinicPatient.UserId);
                if (user.Id != serviceAccount)
                {
                    user.AccountLinked = true;
                    //TODO: this remapping / drop is ghetto but I am at a loss right now...
                    //there is a chance that a user can create an account after the clinic AddsPatient, 
                    //that creates an account for the session to move forward, user could create account without registration mapping because there is no PHI with user account in service 
                    //to sync them up... if they go through registration process after, this will associate the accounts together
                    AssociateExistingServiceAccounts(serviceAccount, clinicPatient.Patient.Id);
                }

                Commit();

                _logger.Info(String.Format("Validate Clinic Patient For {0} - {1} Success", emailAddress, serviceAccount));
            }

            return clinicPatient;
        }

        private void AssociateExistingServiceAccounts(Guid mapToAccount, Guid mapFromAccount)
        {
            _logger.Info(String.Format("Associate Existing Serivce Accounts: {0} To {1}", mapFromAccount, mapToAccount));

            var mapToUser = UserRepository.GetAll().SingleOrDefault(s => s.Id == mapToAccount);
            var mapFromUser = UserRepository.GetAll().SingleOrDefault(s => s.Id == mapFromAccount);

            Check.Argument.IsNotNull(mapToUser, "MapToUser");
            Check.Argument.IsNotNull(mapFromUser, "MapFromUser");

            ClinicPatientRepository.GetAll().Where(p => p.UserId == mapFromUser.Id).ForEach(f =>
            {
                f.Patient = mapToUser;
                ClinicPatientRepository.Update(f);
            });

            CaseRepository.GetAll().Where(p => p.ClinicPatient.UserId == mapFromUser.Id).ForEach(f =>
            {
                f.ClinicPatient.Patient = mapToUser;
                CaseRepository.Update(f);
            });

            Commit();

            UserRepository.Delete(mapFromUser);

            _logger.Info(String.Format("Associate Existing Serivce Accounts Completed: {0} To {1}", mapFromAccount, mapToAccount));
        }

        public ClinicTherapistMatrixItem ValidateClinicTherapist(String emailAddress, String encryptedPin, Guid serviceAccount)
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

                var user = UserRepository.GetAll().SingleOrDefault(s => s.Id == clinicTherapist.TherapistId);
                if (user.Id != serviceAccount)
                {
                    user.AccountLinked = true;
                    //TODO: this remapping / drop is ghetto but I am at a loss right now...
                    //there is a chance that a user can create an account after the clinic AddsPatient, 
                    //that creates an account for the session to move forward, user could create account without registration mapping because there is no PHI with user account in service 
                    //to sync them up... if they go through registration process after, this will associate the accounts together
                    AssocaiteExistingTherapistServiceAccounts(serviceAccount, clinicTherapist.Therapist.User.Id);
                }

                Commit();
            }

            return clinicTherapist;
        }

        private void AssocaiteExistingTherapistServiceAccounts(Guid mapToAccount, Guid mapFromAccount)
        {
            var mapToUser = UserRepository.GetAll().Include(t => t.Therapist).SingleOrDefault(s => s.Id == mapToAccount);
            var mapFromUser = UserRepository.GetAll().Include(t => t.Therapist).SingleOrDefault(s => s.Id == mapFromAccount);

            Check.Argument.IsNotNull(mapToUser, "MapToUser");
            Check.Argument.IsNotNull(mapFromUser, "MapFromUser");

            ClinicTherapistRepository.GetAll().Where(p => p.TherapistId == mapFromUser.Id).ForEach(f =>
            {
                if (mapToUser.Therapist == null)
                    f.Therapist = new Therapist() { User = mapToUser };
                else
                    f.Therapist.User = mapToUser;
                
                ClinicTherapistRepository.Update(f);
            });

            Commit();

            TherapistRepository.Delete(mapFromUser.Therapist);
            UserRepository.Delete(mapFromUser);
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

        ClinicPatientMatrixItem AddPatientToClinic(int clinicId, Guid userId);
        ClinicTherapistMatrixItem AddTherapistToClinic(int clinicId, Guid therapistId);
        ClinicPatientMatrixItem ValidateClinicPatient(String emailAddress, String pin, Guid subjectId);
        ClinicTherapistMatrixItem ValidateClinicTherapist(String emailAddress, String pin, Guid subjectId);
    }
}
