using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface IDiffDiagUnitOfWork : IBaseUnitOfWork
    {
        IRepository<DifferentialDiagnosis> DiffDiagRepo { get; }

        IRepository<SymptomDetail> SymptomResponseRepo { get; }

        IRepository<InjurySymptomMatrixItem> InjurySymptomMatrixItemRepo { get; }

        IRepository<Injury> InjuryRepo { get; } 
    }
}
