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
        private String _areaComponentUriPath = String.Empty;

        #endregion

        #region Construction

        public ExamineService(BaseServiceStackClientSettings clientSettings)
            : base(clientSettings)
        {
            _skeletonAreasUriPath = String.Format("/{0}/areas", _settings.Version);
            _areaComponentUriPath = String.Format("/{0}/areas/.id/components", _settings.Version);
        }

        #endregion

        public IEnumerable<SkeletonArea> GetSkeletonAreas()
        {
            var response =
                GetSync<ListResponse<SkeletonAreaDto, SkeletonSortBy>>(_skeletonAreasUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<SkeletonArea>>(response.Resource.Items);
        }


        public IEnumerable<AreaComponent> GetAreaComponents(int skeletionAreaId)
        {
            var response =
                GetSync<ListResponse<AreaComponentDto,AreaComponentSortBy>>(_areaComponentUriPath.Replace(".id",
                                                                                                           Convert
                                                                                                               .ToString
                                                                                                               (skeletionAreaId)));

            return response.Resource == null ? null : Mapper.Map<IEnumerable<AreaComponent>>(response.Resource.Items);

        }
    }
}
