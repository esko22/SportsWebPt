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
    }

    public interface IDiffDiagUnitOfWork : IBaseUnitOfWork
    {
        IRepository<DifferentialDiagnosis> DiffDiagRepo { get; }

        IRepository<SymptomDetail> SymptomResponseRepo { get; }

        IRepository<InjurySymptomMatrixItem> InjurySymptomMatrixItemRepo { get; }

        IRepository<InjuryPlanMatrixItem> InjuryPlanMatrixItemRepo { get; }

        IRepository<Injury> InjuryRepo { get; }
    }
}
