using System;
using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class ExamineService : BaseServiceStackClient, IExamineService
    {

        #region Fields

        private String _skeletonAreasUriPath = String.Empty;

        #endregion


        #region Construction

        public ExamineService(BaseServiceStackClientSettings clientSettings)
            : base(clientSettings)
        {
            _skeletonAreasUriPath = String.Format("/{0}/areas", _settings.Version);
        }

        #endregion

        public IEnumerable<SkeletonArea> GetSkeletonAreas()
        {
            var response =
                GetSync<ListResponse<SkeletonAreaDto, SkeletonSortBy>>(_skeletonAreasUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<SkeletonArea>>(response.Resource.Items);
        }
    }
}
