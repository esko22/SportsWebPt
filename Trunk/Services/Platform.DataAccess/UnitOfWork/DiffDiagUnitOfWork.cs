using System;
using System.Collections.Generic;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core;
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
        public IRepository<ClinicInjuryMatrixItem> ClinicInjuryRepo { get { return GetStandardRepo<ClinicInjuryMatrixItem>();}}

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

        public IEnumerable<Injury> GetPotentialInjuries(IEnumerable<int> symptomMatrixIds, int clinicId)
        {
            var clinicInjuries = ClinicInjuryRepo.GetAll(new[]
            {
                "Injury.InjuryPlanMatrixItems",
                "Injury.InjuryPlanMatrixItems.Plan",
                "Injury.InjurySymptomMatrixItems",
                "Injury.InjurySymptomMatrixItems.SymptomMatrixItem.Symptom",
                "Injury.InjurySignMatrixItems",
                "Injury.InjuryPlanMatrixItems.Plan.PlanCategoryMatrixItems",
                "Injury.InjurySignMatrixItems.Sign",
                "Injury.InjuryCauseMatrixItems",
                "Injury.InjuryCauseMatrixItems.Cause",
                "Injury.InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem",
                "Injury.InjurySymptomMatrixItems.SymptomMatrixItem.BodyPartMatrixItem.BodyPart",
                "Injury.InjuryTreatmentMatrixItems",
                "Injury.InjuryPrognosisMatrixItems",
                "Injury.InjuryPrognosisMatrixItems.Prognosis",
                "Injury.InjuryTreatmentMatrixItems.Treatment"
            })
                .Where(
                    p => (p.IsPublic && p.ClinicId == clinicId) && p.Injury.InjurySymptomMatrixItems.Any(s => symptomMatrixIds.Contains(s.SymptomMatrixItemId)));

            var injuries = new List<Injury>();
            clinicInjuries.ForEach(p => injuries.Add(p.Injury));

            return injuries;
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
        IRepository<Injury> InjuryRepo { get; }
        IRepository<ClinicInjuryMatrixItem> ClinicInjuryRepo { get; }

        #endregion

        #region Methods
		
        DifferentialDiagnosis GetDiffDiagWithDetails(Int64 diffDiagId);
        IEnumerable<Injury> GetPotentialInjuries(IEnumerable<int> symptomMatrixIds, int clinicId);

	    #endregion    
    }
}
