using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using SportsWebPt.Common.Logging;

namespace SportsWebPt.Common.Web
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        #region Fields

        private ILog _logger = LogManager.GetCommonLogger();

        #endregion


        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            _logger.Error(context.Exception.Message, context.Exception);

            return base.LogAsync(context, cancellationToken);
        }
    }
}
