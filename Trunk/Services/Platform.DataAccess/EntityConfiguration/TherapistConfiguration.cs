using System.Data.Entity.ModelConfiguration;

using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class TherapistConfiguration : EntityTypeConfiguration<Therapist>
    {

        #region Construction
        
        public TherapistConfiguration()
        {
            ToTable("Therapist");
            HasKey(p => p.Id);
            Property(p => p.Credentials).HasColumnName("credentials").HasMaxLength(1000);
            Property(p => p.Licenses).HasColumnName("licenses").HasMaxLength(1000);
            Property(p => p.Id).HasColumnName("therapist_id");

            HasRequired(r => r.User).WithOptional(o => o.Therapist);
        } 

        #endregion

    }
}
