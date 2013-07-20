using System;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface IPlanRepo : IRepository<Plan>
    {
        Plan GetFullPlanGraphById(int planId);

    }
}
