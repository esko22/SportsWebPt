using System.Data.Entity;

namespace SportsWebPt.Common.DataAccess.Ef
{
    public interface ISeed<TTYpeOfDbCntext>
    {
        void Seed(TTYpeOfDbCntext dbContext);
    }
}
