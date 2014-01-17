using System;
using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class LookupService : RestService
    {

        #region Fields

        private ILookupUnitOfWork _lookupUnitOfWork;

        #endregion

        #region Construction

        public LookupService(ILookupUnitOfWork lookupUnitOfWork)
        {
            Check.Argument.IsNotNull(lookupUnitOfWork, "LookupUnitOfWork");
            _lookupUnitOfWork = lookupUnitOfWork;
        }

        #endregion

        #region Methods

        public object Get(FilterListRequest request)
        {
            var responseList = new List<FilterDto>();

            switch (request.FilterType)
            {
                case FilterTypeDto.Cause:
                    Mapper.Map(_lookupUnitOfWork.GetCauseFilters(), responseList);
                    break;
                case FilterTypeDto.Sign:
                    Mapper.Map(_lookupUnitOfWork.GetSignFilters(), responseList);
                    break;
                default:
                    Mapper.Map(_lookupUnitOfWork.GetFilters(), responseList);
                    break;
            }


            return
                Ok(new ApiListResponse<FilterDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
