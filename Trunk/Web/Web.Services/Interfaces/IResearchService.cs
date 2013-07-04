using System;
using System.Collections.Generic;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IResearchService
    {
        Workout GetWorkout(int workoutId);

        IEnumerable<Equipment> GetEquipment();

        IEnumerable<Video> GetVideos();

    }
}
