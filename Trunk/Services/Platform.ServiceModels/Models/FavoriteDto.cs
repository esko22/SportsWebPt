using System;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    public class FavoriteDto
    {
        #region Properties

        public FavoriteTypeDto Entity { get; set; }

        public int EntityId { get; set; }

        public String EntityName { get; set; }

        #endregion
    }

    public enum FavoriteTypeDto
    {
        Injury,
        Plan,
        Exercise,
        Video
    }
}
