using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface IWorkoutUnitOfWork
    {
        #region Properties

        IWorkoutRepo WorkoutRepo { get; }

        IExerciseRepo ExerciseRepo { get; }

        #endregion
    }
}
