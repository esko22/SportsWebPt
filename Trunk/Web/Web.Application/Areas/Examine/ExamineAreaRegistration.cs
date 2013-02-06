using System.Web.Mvc;

namespace SportsWebPt.Platform.Web.Examine
{
    public class ExaminateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Examine";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Examine_default",
            //    "Examine/{controller}/{action}/{id}",
            //    new {action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
