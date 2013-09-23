using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class VideoCategoryMatrixConfiguration : EntityTypeConfiguration<VideoCategoryMatrixItem>
    {
        #region Construction

        public VideoCategoryMatrixConfiguration()
        {
            ToTable("VideoCategoryMatrix");
            HasKey(p => new {p.VideoId, p.Category});
            Property(p => p.Category).IsRequired().HasColumnName("function_category_id");
            Property(p => p.VideoId).IsRequired().HasColumnName("video_id");
        }

        #endregion
    }
}
