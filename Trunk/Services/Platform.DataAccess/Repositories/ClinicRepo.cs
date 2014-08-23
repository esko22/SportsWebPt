using System;
using System.Data.Entity;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ClinicRepo: EFRepository<Clinic>, IClinicRepo
    {
        #region Construction

        public ClinicRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

      

    }

    public interface IClinicRepo : IRepository<Clinic>
    {
    }
}
