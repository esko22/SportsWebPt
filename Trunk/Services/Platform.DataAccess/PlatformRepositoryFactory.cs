using System;
using System.Collections.Generic;
using System.Data.Entity;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.DataAccess.Repositories;

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
                   {typeof(IUserRepo), dbContext => new UserRepo(dbContext)},
                   {typeof(ISymptomRepo), dbContext => new SymptomRepo(dbContext)},
                   {typeof(IBodyPartRepo), dbContext => new BodyPartRepo(dbContext)},
                   {typeof(IExerciseRepo), dbContext => new ExerciseRepo(dbContext)},
                   {typeof(IInjuryRepo), dbContext => new InjuryRepo(dbContext)},
                   {typeof(IVideoRepo), dbContext => new VideoRepo(dbContext)},
                   {typeof(ICaseRepo), dbContext => new CaseRepo(dbContext)},
                   {typeof(ISessionRepo), dbContext => new SessionRepo(dbContext)},
                   {typeof(IClinicRepo), dbContext => new ClinicRepo(dbContext)}
                };
        }

    }
}
