using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        #region Construction

        public VideoConfiguration()
        {
            ToTable("Video");
            Property(p => p.Filename).IsRequired().HasColumnName("filename").HasMaxLength(100);
            Property(p => p.Description).HasColumnName("description").HasMaxLength(500);
            Property(p => p.CreationDate).HasColumnName("created_on").IsRequired();
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
            Property(p => p.Id).IsRequired().HasColumnName("video_id");
        }

        #endregion
    }
}
