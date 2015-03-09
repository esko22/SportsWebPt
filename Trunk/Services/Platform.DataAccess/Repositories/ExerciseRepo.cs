using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ExerciseRepo : EFRepository<Exercise>, IExerciseRepo
    {
        #region Construction

        public ExerciseRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public IQueryable<Exercise> GetExerciseDetails()
        {
            return base.GetAll()
            .Include(i => i.TherapistExerciseMatrixItems)
            .Include(i => i.ClinicExerciseMatrixItems.Select(l2 => l2.Clinic))
            .Include(i => i.ExerciseEquipmentMatrixItems.Select(l2 => l2.Equipment))
            .Include(i => i.ExerciseVideoMatrixItems.Select(l2 => l2.Video.VideoCategoryMatrixItems))
            .Include(i => i.ExerciseBodyRegionMatrixItems.Select(l2 => l2.BodyRegion))
            .Include(i => i.ExerciseCategoryMatrixItems)
            .Include(i => i.PublishDetail)
            .Include(i => i.ExerciseBodyPartMatrixItems.Select(l2 => l2.BodyPart));
            
        }

        public IQueryable<Exercise> GetExerciseDetailForUpdate()
        {
            return base.GetAll()
                .Include(i => i.ExerciseEquipmentMatrixItems)
                .Include(i => i.ExerciseBodyPartMatrixItems)
                .Include(i => i.ExerciseBodyRegionMatrixItems)
                .Include(i => i.ExerciseCategoryMatrixItems)
                .Include(i => i.ExerciseVideoMatrixItems);
        }


        #endregion
    }
}
