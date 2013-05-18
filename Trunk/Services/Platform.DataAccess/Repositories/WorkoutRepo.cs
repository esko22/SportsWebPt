using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class WorkoutRepo :  EFRepository<Workout>, IWorkoutRepo 
    {
        #region Construction

        public WorkoutRepo(DbContext context)
            : base(context)
        {}

        #endregion

        #region Methods

        public Workout GetFullWorkoutGraphById(int workoutId)
        {
            //TODO: investigate why I cant do both joins to equip and video in one query
            return DbSet
                .Include("WorkoutExerciseMatrixItems")
                .Include("WorkoutExerciseMatrixItems.Exercise")
                .SingleOrDefault(w => w.Id == workoutId);
        }


        #endregion
    }
}
