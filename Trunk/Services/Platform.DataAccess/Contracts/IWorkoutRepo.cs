using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface IWorkoutRepo : IRepository<Workout>
    {
        Workout GetFullWorkoutGraphById(int workoutId);
    }
}
