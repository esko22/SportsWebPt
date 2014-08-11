using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface IExerciseRepo : IRepository<Exercise>
    {
        IQueryable<Exercise> GetExerciseDetails();
        IQueryable<Exercise> GetExerciseDetailForUpdate();
    }
}
