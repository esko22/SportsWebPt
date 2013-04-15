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

        private String _skeletorHotspotsUriPath = String.Empty;

        #endregion


        #region Construction

        public ExamineService(BaseServiceStackClientSettings clientSettings)
            : base(clientSettings)
        {
            _skeletorHotspotsUriPath = String.Format("/{0}/skeletor", _settings.Version);
        }

        #endregion

        public IEnumerable<SkeletonHotspot> GetSkeletonHotspots()
        {
            var response =
                GetSync<ListResponse<SkeletorHotspotDto, SkeletorSortBy>>(_skeletorHotspotsUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<SkeletonHotspot>>(response.Resource.Items);
        }
    }
}
