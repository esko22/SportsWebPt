using System;
using System.Collections.Generic;
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
        public IRepository<Injury> InjuryRepo { get { return GetStandardRepo<Injury>(); } } 

        #endregion

        #region Construction

        public DiffDiagUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public DifferentialDiagnosis GetDiffDiagWithDetails(Int64 diffDiagId)
        {
            return DiffDiagRepo.GetAll(new[]
                {
                    "SymptomDetails", "SymptomDetails.SymptomMatrixItem",
                    "SymptomDetails.SymptomMatrixItem.BodyPartMatrixItem",
                    "SymptomDetails.SymptomMatrixItem.BodyPartMatrixItem.BodyPart"
                }).SingleOrDefault(p => p.Id == diffDiagId);
        }

        public IQueryable<Injury> GetPotentialInjuries(IEnumerable<int> symptomMatrixIds)
        {
            return
                InjuryRepo.GetAll(new[]
                    {
                        "InjuryPlanMatrixItems", "InjuryPlanMatrixItems.Plan", "InjurySymptomMatrixItems",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.Symptom",
                        "InjurySignMatrixItems", "InjuryPlanMatrixItems.Plan.PlanCategoryMatrixItems",
                        "InjurySignMatrixItems.Sign", "InjuryCauseMatrixItems", "InjuryCauseMatrixItems.Cause",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem",
                        "InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem.BodyPart","InjuryTreatmentMatrixItems", "InjuryPrognosisMatrixItems", 
                        "InjuryPrognosisMatrixItems.Prognosis", "InjuryTreatmentMatrixItems.Treatment"
                    })
                          .Where(
                              p => p.InjurySymptomMatrixItems.Any(s => symptomMatrixIds.Contains(s.SymptomMatrixItemId)));
        } 

        #endregion
    }

    public interface IDiffDiagUnitOfWork : IBaseUnitOfWork
    {
        IRepository<DifferentialDiagnosis> DiffDiagRepo { get; }

        IRepository<SymptomDetail> SymptomResponseRepo { get; }

        IRepository<InjurySymptomMatrixItem> InjurySymptomMatrixItemRepo { get; }

        IRepository<InjuryPlanMatrixItem> InjuryPlanMatrixItemRepo { get; }

        IRepository<Injury> InjuryRepo { get; }

        DifferentialDiagnosis GetDiffDiagWithDetails(Int64 diffDiagId);

        IQueryable<Injury> GetPotentialInjuries(IEnumerable<int> symptomMatrixIds);
    }
}
