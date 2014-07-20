using System;
using System.Data.Entity;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SymptomRepo: EFRepository<Symptom>, ISymptomRepo
    {
        #region Construction

        public SymptomRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public override IQueryable<Symptom> GetAll()
        {
            return base.GetAll()
                .Include(i => i.RenderType);
        }

        #endregion
    }

    public interface ISymptomRepo : IRepository<Symptom>
    {
        
    }
}
