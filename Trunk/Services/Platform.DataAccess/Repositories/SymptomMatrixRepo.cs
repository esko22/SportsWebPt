using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class SymptomMatrixRepo : EFRepository<SymptomMatrixItem>, ISymptomMatrixRepo 
    {
        #region Construction

        public SymptomMatrixRepo(DbContext context) : base(context) { }

        #endregion

        #region Methods
        
        public IQueryable<SymptomMatrixItem> GetPotentialSymptoms(int bodyPartMatrixId)
        {
            return DbSet.Where(p => p.BodyPartMatrixItemId == bodyPartMatrixId).Include("symptom").Include("symptom.renderType");
        } 

        #endregion
    }
}
