using System;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IResearchService
    {
        Workout GetWorkout(int workoutId);
    }
}
