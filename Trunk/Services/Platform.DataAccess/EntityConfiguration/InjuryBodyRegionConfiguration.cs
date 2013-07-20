using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess.EntityConfiguration
{
    public class InjuryBodyRegionMatrixConfiguration : EntityTypeConfiguration<InjuryBodyRegionMatrixItem>
    {

        #region Construction

        public InjuryBodyRegionMatrixConfiguration()
        {
            ToTable("InjuryBodyRegionMatrix");
            Property(p => p.BodyRegionId).IsRequired().HasColumnName("body_region_id");
            Property(p => p.InjuryId).IsRequired().HasColumnName("injury_id");
            Property(p => p.Id).IsRequired().HasColumnName("injury_body_region_matrix_id");
        }

        #endregion

    }
}
