using System;
using System.Linq;

using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class UserUnitOfWork : BaseUnitOfWork, IUserUnitOfWork
    {

        #region Properties

        public IUserRepo UserRepository { get { return GetRepo<IUserRepo>(); } }
        public IRepository<Injury> InjuryRepository { get { return GetStandardRepo<Injury>(); } }
        public IRepository<Plan> PlanRepository { get { return GetStandardRepo<Plan>(); } }
        public IRepository<Video> VideoRepository { get { return GetStandardRepo<Video>(); } }
        public IRepository<Exercise> ExerciseRepository { get { return GetStandardRepo<Exercise>(); } }
        public IRepository<Therapist> TherapistRepository { get { return GetStandardRepo<Therapist>(); } } 

        #endregion

        #region Construction

        public UserUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}
        
        #endregion

        #region Methods

        public User GetUserById(int userId)
        {
            return UserRepository.GetUserDetails().SingleOrDefault(w => w.Id == userId);
        }
        
        public User GetUserBySubject(string subjectId)
        {
            return UserRepository.GetUserDetails()
                                .SingleOrDefault(
                                    p =>
                                    p.ExternalAccountId.Equals(subjectId, StringComparison.OrdinalIgnoreCase));
        }

        public User AddUser(User user)
        {
            user.ExternalAccountId = Guid.NewGuid().ToString();
            UserRepository.Add(user);
            Commit();

            return user;
        }

        public Therapist AddTherapist(User user)
        {
            TherapistRepository.Add(new Therapist() { Id = user.Id} );
            Commit();

            return TherapistRepository.GetById(user.Id);
        }

        public Therapist GetTherapistById(int therapistId)
        {
            return TherapistRepository.GetById(therapistId);
        }

        public User UpdateUser(User newUser, int originalUserId)
        {
            var userInDb =
                UserRepository.GetById(originalUserId);

            if (userInDb == null)
                throw new ArgumentNullException("user id", "user id does not exist");

            newUser.Id = originalUserId;

            var userEntry = _context.Entry(userInDb);
            userEntry.CurrentValues.SetValues(newUser);

            Commit();

            return newUser;
        }

        #endregion

    }

    public interface IUserUnitOfWork : IBaseUnitOfWork
    {
        IUserRepo UserRepository { get; }
        IRepository<Injury> InjuryRepository { get; }
        IRepository<Plan> PlanRepository { get; }
        IRepository<Video> VideoRepository { get; }
        IRepository<Exercise> ExerciseRepository { get; }

        User GetUserById(int userId);
        User GetUserBySubject(string subjectId);
        User AddUser(User user);
        User UpdateUser(User newUser, int originalUserId);
        Therapist AddTherapist(User user);
        Therapist GetTherapistById(int therapistId);
    }
}
