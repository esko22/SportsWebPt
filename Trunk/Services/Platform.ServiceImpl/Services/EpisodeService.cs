using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
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

        #endregion

    }
}
