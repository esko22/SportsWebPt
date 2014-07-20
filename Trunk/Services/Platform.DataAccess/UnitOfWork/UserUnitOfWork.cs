﻿using System;
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
        
        public User GetUserByEmail(string emailAddress)
        {
            return UserRepository.GetUserDetails()
                                .SingleOrDefault(
                                    p =>
                                    p.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
        }

        public User AddUser(User user)
        {
            UserRepository.Add(user);
            Commit();

            return user;
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
        User GetUserByEmail(string emailAddress);
        User AddUser(User user);
    }
}
