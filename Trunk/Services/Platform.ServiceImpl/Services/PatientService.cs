using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class PatientService : RestService
    {

        #region Properties

        public IEpisodeUnitOfWork EpisodeUnitOfWork { get; set; }

        #endregion


        #region Methods

        public object Get(PatientEpisodeListRequest request)
        {
            var responseList = new List<EpisodeDto>();
            var episodes = EpisodeUnitOfWork.GetFilteredEpisodes(clinicPatientId: request.IdAsInt, state: request.State.ToString());

            Mapper.Map(episodes, responseList);

            return
                Ok(new ApiListResponse<EpisodeDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }


        #endregion
    }
}
