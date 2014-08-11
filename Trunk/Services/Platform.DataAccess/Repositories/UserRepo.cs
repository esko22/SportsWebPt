using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class UserRepo : EFRepository<User>, IUserRepo 
    {
        #region Construction 

        public UserRepo(DbContext context)
            : base(context)
        {}

        #endregion

        #region Methods

        public IQueryable<User> GetUserDetails()
        {
            return GetAll()
                .Include(i => i.Therapist)
                .Include(i => i.ClinicAdmin)
                .Include(i => i.VideoFavorites)
                .Include(i => i.PlanFavorites)
                .Include(i => i.ExerciseFavorites)
                .Include(i => i.InjuryFavorites);
        }

        public IQueryable<User> GetTherapistDetails()
        {
            return GetAll()
                .Include(i => i.Therapist.ClinicTherapistMatrixItems.Select(l2 => l2.Clinic));
        } 


        #endregion
    }

    public interface IUserRepo : IRepository<User>
    {
       IQueryable<User> GetUserDetails();
       IQueryable<User> GetTherapistDetails();
    }
}
