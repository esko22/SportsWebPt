using System;
using System.Collections.Generic;
using System.Linq;

using SportsWebPt.Common.Utilities;
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
        public IRepository<BodyRegion> BodyRegionRepo { get { return GetStandardRepo<BodyRegion>(); } }
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

        #region Methods

        public void UpdateBodyPart(BodyPart bodyPart)
        {
            var bodyPartInDb = BodyPartRepo.GetAll(new[] { "BodyPartMatrix" }).SingleOrDefault(p => p.Id == bodyPart.Id);

            if (bodyPartInDb == null)
                throw new ArgumentNullException("body part id", "part does not exist");

            var deletedSkeletonAreas = new List<BodyPartMatrixItem>();
            var addedSkeletonAreas = new List<BodyPartMatrixItem>();

            foreach (var bodyPartMatrixItem in bodyPartInDb.BodyPartMatrix)
            {
                if(!bodyPart.BodyPartMatrix.Any(p => p.BodyPartId == bodyPartMatrixItem.BodyPartId && p.SkeletonAreaId == bodyPartMatrixItem.SkeletonAreaId))
                    deletedSkeletonAreas.Add(bodyPartMatrixItem);
            }

            foreach (var bodyPartMatrixItem in bodyPart.BodyPartMatrix)
            {
                if (!bodyPartInDb.BodyPartMatrix.Any(p => p.BodyPartId == bodyPartMatrixItem.BodyPartId && p.SkeletonAreaId == bodyPartMatrixItem.SkeletonAreaId))
                    addedSkeletonAreas.Add(bodyPartMatrixItem);
            }

            deletedSkeletonAreas.ForEach(e => BodyPartMatrixRepo.Delete(e));
            addedSkeletonAreas.ForEach(e =>
            {
                e.BodyPartId = bodyPart.Id;
                BodyPartMatrixRepo.Add(e);
            });

            var bodyPartEntry = _context.Entry(bodyPartInDb);
            bodyPartEntry.CurrentValues.SetValues(bodyPart);

            Commit();
        }

        #endregion
    }

    public interface ISkeletonUnitOfWork : IBaseUnitOfWork
    {
        ISkeletonRepo SkeletonAreaRepo { get; }
        IRepository<BodyPart> BodyPartRepo { get; }
        IRepository<Symptom> SymptomRepo { get; }
        IRepository<BodyRegion> BodyRegionRepo { get; } 
        ISymptomMatrixRepo SymptomMatrixRepo { get; }
        IRepository<BodyPartMatrixItem> BodyPartMatrixRepo { get; }

        void UpdateBodyPart(BodyPart bodyPart);
    }
}
