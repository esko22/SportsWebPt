using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ClinicUnitOfWork : BaseUnitOfWork, IClinicUnitOfWork
    {
        #region Properties

        public IRepository<ClinicAdmin> ClinicAdminRepository { get { return GetStandardRepo<ClinicAdmin>(); } }
        public IRepository<ClinicTherapistMatrixItem> ClinicTherapistRepository { get { return GetStandardRepo<ClinicTherapistMatrixItem>(); } }
        public IRepository<ClinicPatientMatrixItem> ClinicPatientRepository { get { return GetStandardRepo<ClinicPatientMatrixItem>(); } }

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

        #endregion

    }

    public interface IClinicUnitOfWork : IBaseUnitOfWork
    {
        IClinicRepo ClinicRepository { get; }
        IQueryable<ClinicAdmin> GetClinicAdminList();
        IQueryable<ClinicTherapistMatrixItem> GetClinicTherapists();
        IQueryable<ClinicPatientMatrixItem> GetClinicPatients();

    }
}
