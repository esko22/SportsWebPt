using System;

using SportsWebPt.Common.DataAccess.Ef;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlatformRepositoryProvider : RepositoryProvider
    {
        #region Construction

        public PlatformRepositoryProvider(RepositoryFactory repositoryFactories) : base(repositoryFactories) { }

        #endregion
    }
}
