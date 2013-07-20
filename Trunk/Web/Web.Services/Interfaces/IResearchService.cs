using System.Collections.Generic;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IResearchService
    {
        Plan GetPlan(int planId);

        IEnumerable<BodyPart> GetBodyParts();

        IEnumerable<BodyRegion> GetBodyRegions();

        IEnumerable<Exercise> GetExercises();

        Exercise GetExerciseByPageName(string pageName);
    }
}
