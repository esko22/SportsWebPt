using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsWebPt.Platform.Web.Application
{
    public class TokenValidator
    {
        public void ValidateToken(dynamic tokenInfo, string expectedAudience)
        {
            var audience = tokenInfo.audience.ToString();
            if (string.IsNullOrEmpty(audience) || audience != expectedAudience)
            {
                var e = new HttpException("tokes with unexpected audience: ");
                throw e;
            }

            if (tokenInfo.expires_in == null) return;
            var expiresIn = tokenInfo.expires_in.ToString();
            int intExpiresIn;
            var isInt = int.TryParse(expiresIn, out intExpiresIn);

            //if (!isInt || intExpiresIn)
            //    return;
        }
    }
}