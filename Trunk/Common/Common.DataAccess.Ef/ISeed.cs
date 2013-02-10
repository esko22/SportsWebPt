using System.Data.Entity;

namespace SportsWebPt.Common.DataAccess.Ef
{
    public interface ISeed<in TTYpeOfDbCntext> where TTYpeOfDbCntext : DbContext
    {
        void Seed(TTYpeOfDbCntext dbContext);
    }
}
