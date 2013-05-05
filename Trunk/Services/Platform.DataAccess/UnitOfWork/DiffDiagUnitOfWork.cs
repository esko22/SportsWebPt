using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class DiffDiagUnitOfWork : BaseUnitOfWork, IDiffDiagUnitOfWork
    {
        #region Properties

        public IRepository<DifferentialDiagnosis> DiffDiagRepo { get { return GetStandardRepo<DifferentialDiagnosis>(); } }
        public IRepository<SymptomResponse> SymptomResponseRepo { get { return GetStandardRepo<SymptomResponse>(); } }

        #endregion

        #region Construction

        public DiffDiagUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion
    }
}
