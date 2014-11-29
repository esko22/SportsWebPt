using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.Repositories
{
    public class CaseRepo : EFRepository<Case>,ICaseRepo
    {
        #region Construction

        public CaseRepo(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Methods

        public IQueryable<Case> GetCaseDetails()
        {
            return GetAll()
                .Include(i => i.Therapist.User)
                .Include(i => i.ClinicPatient.Patient)
                .Include(i => i.Clinic)
                .Include(i => i.Prognosis);
        }

        #endregion

    }

    public interface ICaseRepo : IRepository<Case>
    {
        IQueryable<Case> GetCaseDetails();
    }
}
