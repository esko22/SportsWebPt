using BrockAllen.MembershipReboot;

namespace SportsWebPt.Identity.Admin
{
    public class CustomGroup : RelationalGroup
    {
        public virtual string Description { get; set; }
    }
}