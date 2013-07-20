using System;
using System.Collections.Generic;
using System.Data.Entity;

using SportsWebPt.Common.DataAccess.Ef;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlatformRepositoryFactory : RepositoryFactory
    {
        protected override IDictionary<Type, Func<DbContext, object>> GetNonStandardRespositories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
                {
                   {typeof(ISkeletonRepo), dbContext => new SkeletonRepo(dbContext)},
                   {typeof(ISymptomMatrixRepo), dbContext => new SymptomMatrixRepo(dbContext)},
                   {typeof(IPlanRepo), dbContext => new PlanRepo(dbContext)},
                   {typeof(IExerciseRepo), dbContext => new ExerciseRepo(dbContext)}
                };
        }

    }
}
