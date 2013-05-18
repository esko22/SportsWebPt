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

        public override IQueryable<Exercise> GetAll()
        {
            return base.GetAll()
                       .Include("ExerciseVideoMatrixItems")
                       .Include("ExerciseEquipmentMatrixItems")
                       .Include("ExerciseEquipmentMatrixItems.Equipment")
                       .Include("ExerciseVideoMatrixItems.Video");
        }

        #endregion
    }
}
