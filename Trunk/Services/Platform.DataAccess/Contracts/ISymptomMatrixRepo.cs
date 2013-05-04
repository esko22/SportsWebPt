using System.Linq;

using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface ISymptomMatrixRepo : IRepository<SymptomMatrixItem>
    {
        IQueryable<SymptomMatrixItem> GetPotentialSymptoms(int bodyPartMatrixId);
    }
}
