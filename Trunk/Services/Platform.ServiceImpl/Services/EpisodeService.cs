using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ServiceStack.Redis;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.ServiceModels.Operations;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class EpisodeService : RestService
    {

        #region Properties

        public IEpisodeUnitOfWork EpisodeUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(EpisodeListRequest request)
        {
            var responseList = new List<EpisodeDto>();
            Mapper.Map(EpisodeUnitOfWork.GetFilteredEpisodes(request.ClinicTherapistId, request.ClinicPatientId, request.State.ToString()), responseList);

            return
                Ok(new ApiListResponse<EpisodeDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Get(EpisodeRequest request)
        {
            Check.Argument.IsNotNegativeOrZero(request.IdAsLong, "Episode Id must be given");

            var episode = EpisodeUnitOfWork.EpisodeRepo.GetEpisodeDetails()
                .SingleOrDefault(p => p.Id == request.IdAsLong);

            if (episode == null)
                return NotFound("Episode Not Found");

            return Ok(new ApiResponse<EpisodeDto>(Mapper.Map<EpisodeDto>(episode)));
        }

        public object Get(EpisodeSessionListRequest request)
        {
            var responseList = new List<SessionDto>();

            Mapper.Map(EpisodeUnitOfWork.GetEpisodeSessions().Where(p => p.EpisodeId == request.IdAsLong), responseList);

            return
                Ok(new ApiListResponse<SessionDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        public object Post(CreateEpisodeRequest request)
        {
            Check.Argument.IsNotNull(request, "Session Cannot Be Null");
            Check.Argument.IsNotNegativeOrZero(request.ClinicId, "Clinic Id");
            Check.Argument.IsNotNegativeOrZero(request.PatientId, "Patient Id");
            Check.Argument.IsNotNegativeOrZero(request.TherapistId, "Therapist Id");

            var episode = Mapper.Map<Episode>(request);
            episode.PrognosisId = null;
            episode.State = EpisodeState.Active;
            episode.CreatedOn = DateTime.Now;

            EpisodeUnitOfWork.EpisodeRepo.Add(episode);
            EpisodeUnitOfWork.Commit();

            return Ok(new ApiResponse<EpisodeDto>() { Response = Mapper.Map<EpisodeDto>(episode) });
        }


        #endregion

    }
}
