using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class UserFavoriteDto
    {
        #region Properties

        public FavoriteType Entity { get; set; }

        public int EntityId { get; set; }

        public int UserId { get; set; }

        #endregion
    }

    public enum FavoriteType
    {
        Injury,
        Plan,
        Exercise,
        Video
    }
}
