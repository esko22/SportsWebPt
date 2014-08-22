using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class UserManagementService : BaseServiceStackClient, IUserManagementService
    {

        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion
       

        #region Methods

        public UserManagementService(SportsWebPtClientSettings clientSettings) 
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        public User GetUser(String emailAddress)
        {
            var request = GetSync(new UserRequest() { Id = emailAddress });

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        }

        public User GetUser(int id)
        {
            var request = GetSync(new UserRequest() { Id = id.ToString()} );

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        }

        public int AddUser(User user)
        {
            var userResuest = Mapper.Map<CreateUserRequest>(user);
   
            var request = PostSync(userResuest);

            return request.Response.Id;
        }

        public void AddFavorite(Favorite userFavorite, int userId)
        {
            var userFavRequest = Mapper.Map<CreateUserFavoriteRequest>(userFavorite);
            userFavRequest.Id = userId;

            PostSync(userFavRequest);
        }

        public User Auth(string emailAddress, string hash)
        {
            var request = PostSync(new AuthRequest() { EmailAddress = emailAddress, Hash = hash });

            return request.Response == null ? null : Mapper.Map<User>(request.Response);
        }

        public IEnumerable<Episode> GetEpisodes(int patientId, String state)
        {
            EpisodeStateDto? episodeState = null;
            if (!String.IsNullOrEmpty(state))
                episodeState = (EpisodeStateDto)Enum.Parse(typeof(EpisodeStateDto), state, true);

            var request = GetSync(new PatientEpisodeListRequest() { Id = patientId.ToString(), State = episodeState });

            return request.Response == null ? null : Mapper.Map<IEnumerable<Episode>>(request.Response.Items.OrderBy(p => p.CreatedOn));
        }

        #endregion

    }

    public interface IUserManagementService : IDisposable
    {
        User GetUser(String emailAddress);
        User GetUser(int id);
        int AddUser(User user);
        User Auth(String emailAddress, String hash);
        void AddFavorite(Favorite favorite, int userId);
        IEnumerable<Episode> GetEpisodes(int patientId, String state);

    }


}
