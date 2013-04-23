using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class AreaComponentConfiguration : EntityTypeConfiguration<AreaComponent>
    {
        #region Construction

        public AreaComponentConfiguration()
        {
            ToTable("AreaComponent");
            Property(p => p.CommonName).IsRequired().HasColumnName("common_name").HasMaxLength(50);
            Property(p => p.ScientificName).HasColumnName("scientific_name").HasMaxLength(100);
            Property(p => p.Id).IsRequired().HasColumnName("part_id");

            HasMany(p => p.SkeletonAreas)
                .WithMany(p => p.Components)
                .Map(mc => mc.ToTable("skeletonareacomponent")
                .MapLeftKey("skeleton_area_id")
                .MapRightKey("area_component_id"));

            HasMany(p => p.Symptoms)
                    .WithMany(p => p.Components)
                    .Map(mc => mc.ToTable("areacomponentsymptom")
                    .MapLeftKey("symptom_id")
                    .MapRightKey("area_component_id"));

        }
        
        #endregion
    }
}
