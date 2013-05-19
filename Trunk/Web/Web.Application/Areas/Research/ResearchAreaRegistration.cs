using System.Web.Mvc;

namespace SportsWebPt.Platform.Web.Application.Areas.Research
{
    public class ResearchAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Research";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Research_default",
            //    "Research/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
