using System.Collections.Generic;

using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IExamineService
    {
        IEnumerable<SkeletonArea> GetSkeletonAreas();

        IEnumerable<BodyPart> GetBodyParts(int skeletionAreaId);

        IEnumerable<SymptomaticRegion> GetSymptomaticRegions();

        IEnumerable<PotentialSymptom> GetPotentialSymptoms(int bodyPartMatrixId);
    }
}
