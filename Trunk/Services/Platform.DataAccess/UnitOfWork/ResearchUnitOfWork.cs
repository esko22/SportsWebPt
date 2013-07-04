using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.UnitOfWork
{
    public class ResearchUnitOfWork : BaseUnitOfWork, IResearchUnitOfWork
    {

        #region Properties

        public IRepository<Equipment> EquipmentRepo { get { return GetStandardRepo<Equipment>(); } }
        public IRepository<Video> VideoRepo { get { return GetStandardRepo<Video>(); } }
        public IRepository<Exercise> ExerciseRepo { get { return GetStandardRepo<Exercise>(); } } 
	    
        #endregion

        #region Construction

        public ResearchUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion
    }

    public interface IResearchUnitOfWork
    {
        IRepository<Equipment> EquipmentRepo { get; }
        IRepository<Video> VideoRepo { get; }
        IRepository<Exercise> ExerciseRepo { get; }
    }
}
