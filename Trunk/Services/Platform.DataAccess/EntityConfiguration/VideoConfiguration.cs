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
            Property(p => p.YoutubeVideoId).HasColumnName("youtube_video_id").HasMaxLength(20);
            Property(p => p.Description).HasColumnName("description").HasColumnType("TEXT");
            Property(p => p.CreationDate).HasColumnName("created_on").IsRequired();
            Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
            Property(p => p.MedicalName).IsOptional().HasColumnName("medical_name").HasMaxLength(100);
            Property(p => p.Id).IsRequired().HasColumnName("video_id");
            Property(p => p.Category).IsRequired().HasColumnName("function_category");
        }

        #endregion
    }
}
