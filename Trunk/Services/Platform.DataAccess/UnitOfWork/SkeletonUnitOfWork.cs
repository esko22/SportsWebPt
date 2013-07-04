using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SkeletonUnitOfWork : BaseUnitOfWork, ISkeletonUnitOfWork
    {
        #region Properties

        public ISkeletonRepo SkeletonAreaRepo { get { return GetRepo<ISkeletonRepo>(); } }
        public IRepository<BodyPart> BodyPartRepo { get { return GetStandardRepo<BodyPart>(); } }
        public IRepository<BodyPartMatrixItem> BodyPartMatrixRepo { get { return GetStandardRepo<BodyPartMatrixItem>(); } } 
        public IRepository<Symptom> SymptomRepo { get { return GetStandardRepo<Symptom>(); } }
        public ISymptomMatrixRepo SymptomMatrixRepo { get { return GetRepo<ISymptomMatrixRepo>(); } }
        
        #endregion

        #region Construction

        public SkeletonUnitOfWork(IRepositoryProvider repositoryProvider)
            : base(repositoryProvider, new PlatformDbContext())
        {
        }

        #endregion
    }

    public interface ISkeletonUnitOfWork : IBaseUnitOfWork
    {
        ISkeletonRepo SkeletonAreaRepo { get; }
        IRepository<BodyPart> BodyPartRepo { get; }
        IRepository<Symptom> SymptomRepo { get; }
        ISymptomMatrixRepo SymptomMatrixRepo { get; }
        IRepository<BodyPartMatrixItem> BodyPartMatrixRepo { get; }
    }
}
