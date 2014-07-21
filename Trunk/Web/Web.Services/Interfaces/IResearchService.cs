using System.Collections.Generic;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IResearchService
    {
        Plan GetPlan(int planId);
        IEnumerable<BodyPart> GetBodyParts();
        IEnumerable<BodyRegion> GetBodyRegions();
        IEnumerable<BriefExercise> GetResearchExercises();
        IEnumerable<BriefPlan> GetResearchPlans();
        IEnumerable<BriefInjury> GetResearchInjuries();
        IEnumerable<Sign> GetSigns();
        IEnumerable<Equipment> GetEquipment();
        IEnumerable<BriefEquipment> GetBriefEquipment();
        Exercise GetExerciseByPageName(string pageName);
        Injury GetInjuryByPageName(string pageName);
        Plan GetPlanByPageName(string pageName);




        IEnumerable<BriefPlan> GetClinicPlans(int clinicId);
    }
}
