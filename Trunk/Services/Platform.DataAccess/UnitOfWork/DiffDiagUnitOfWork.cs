using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class DiffDiagUnitOfWork : BaseUnitOfWork, IDiffDiagUnitOfWork
    {
        #region Properties

        public IRepository<DifferentialDiagnosis> DiffDiagRepo { get { return GetStandardRepo<DifferentialDiagnosis>(); } }
        public IRepository<SymptomDetail> SymptomResponseRepo { get { return GetStandardRepo<SymptomDetail>(); } }
        public IRepository<InjurySymptomMatrixItem> InjurySymptomMatrixItemRepo { get { return GetStandardRepo<InjurySymptomMatrixItem>(); } }
        public IRepository<InjuryPlanMatrixItem> InjuryPlanMatrixItemRepo { get { return GetStandardRepo<InjuryPlanMatrixItem>(); } }
        public IInjuryRepo InjuryRepo { get { return GetRepo<IInjuryRepo>(); } }

        #endregion

        #region Construction

        public DiffDiagUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public DifferentialDiagnosis GetDiffDiagWithDetails(Int64 diffDiagId)
        {
            return DiffDiagRepo.GetAll()
                .Include(i => i.SymptomDetails.Select(l2 => l2.SymptomMatrixItem.BodyPartMatrixItem.BodyPart))
                .SingleOrDefault(p => p.Id == diffDiagId);
        }

        public IEnumerable<Injury> GetPotentialInjuries(IEnumerable<int> symptomMatrixIds, int clinicId)
        {
            return InjuryRepo.GetInjuryDetails()
                .Where(p => p.ClinicInjuryMatrixItems.Any(s => s.IsActive && s.ClinicId == clinicId) && p.InjurySymptomMatrixItems.Any(s => symptomMatrixIds.Contains(s.SymptomMatrixItemId)));
        } 

        #endregion
    }

    public interface IDiffDiagUnitOfWork : IBaseUnitOfWork
    {
        #region Properties
        
        IRepository<DifferentialDiagnosis> DiffDiagRepo { get; }
        IRepository<SymptomDetail> SymptomResponseRepo { get; }
        IRepository<InjurySymptomMatrixItem> InjurySymptomMatrixItemRepo { get; }
        IRepository<InjuryPlanMatrixItem> InjuryPlanMatrixItemRepo { get; }
        IInjuryRepo InjuryRepo { get; }

        #endregion

        #region Methods
		
        DifferentialDiagnosis GetDiffDiagWithDetails(Int64 diffDiagId);
        IEnumerable<Injury> GetPotentialInjuries(IEnumerable<int> symptomMatrixIds, int clinicId);

	    #endregion    
    }
}
