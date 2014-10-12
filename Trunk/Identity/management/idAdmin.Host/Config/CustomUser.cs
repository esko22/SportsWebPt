using BrockAllen.MembershipReboot.Relational;
using System.ComponentModel.DataAnnotations;

namespace SportsWebPt.Identity.Admin
{
    public class CustomUser : RelationalUserAccount
    {
        [Display(Name="First Name")]
        public virtual string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }
        public virtual int? Age { get; set; }
    }
}