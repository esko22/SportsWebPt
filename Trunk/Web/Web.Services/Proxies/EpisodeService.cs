using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class EpisodeService : BaseServiceStackClient, IEpisodeService
    {
        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion

        #region Construction

        public EpisodeService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion

        #region Methods
        
        public Episode GetEpisode(Int64 episodeId)
        {
            var request = GetSync(new EpisodeRequest {Id = episodeId.ToString()});

            return Mapper.Map<Episode>(request.Response);
        }

        public IEnumerable<Session> GetEpisodeSessions(Int64 episodeId)
        {
            var request = GetSync(new EpisodeSessionListRequest { Id = episodeId.ToString() });

            return Mapper.Map<IEnumerable<Session>>(request.Response.Items);
        }

        public Int64 AddEpisode(Episode episode)
        {
            var request = PostSync(Mapper.Map<CreateEpisodeRequest>(episode));

            return request.Response.Id;
        }


        #endregion
    }

    public interface IEpisodeService
    {
        Int64 AddEpisode(Episode episode);
        Episode GetEpisode(Int64 episodeId);
        IEnumerable<Session> GetEpisodeSessions(Int64 episodeId);

    }
}
