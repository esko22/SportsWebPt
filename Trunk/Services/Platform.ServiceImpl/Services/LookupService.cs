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

        public object Get(SignFilterListRequest request)
        {
            var responseList = new List<SignFilterDto>();
            Mapper.Map(_lookupUnitOfWork.GetSignFilters(), responseList);

            return
                Ok(new ApiListResponse<SignFilterDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));
        }

        #endregion
    }
}
