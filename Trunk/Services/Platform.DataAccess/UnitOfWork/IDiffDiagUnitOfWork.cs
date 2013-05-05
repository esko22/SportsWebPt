using System;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public interface IDiffDiagUnitOfWork : IBaseUnitOfWork
    {
        IRepository<DifferentialDiagnosis> DiffDiagRepo { get; }

        IRepository<SymptomResponse> SymptomResponseRepo { get; } 
    }
}
