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
        
        IEnumerable<Plan> GetPlans();
        
        IEnumerable<Injury> GetInjuries();

        IEnumerable<Sign> GetSigns();

        IEnumerable<Equipment> GetEquipment();
        
        Exercise GetExerciseByPageName(string pageName);

        Injury GetInjuryByPageName(string pageName);

        Plan GetPlanByPageName(string pageName);

    }
}
