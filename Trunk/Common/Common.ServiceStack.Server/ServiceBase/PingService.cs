using System;

namespace SportsWebPt.Common.ServiceStack
{
    public class PingService : RestService
    {

        #region Methods

        public object Get(PingRequest request)
        {
            return Ok(new ApiResponse<String>() {Response = "PONG"});
        }

        #endregion

    }
}
