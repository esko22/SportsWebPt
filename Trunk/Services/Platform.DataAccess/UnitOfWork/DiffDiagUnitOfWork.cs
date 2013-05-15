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
        public IRepository<Injury> InjuryRepo { get { return GetStandardRepo<Injury>(); } } 

        #endregion

        #region Construction

        public DiffDiagUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion
    }
}
