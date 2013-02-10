using System;
using System.Web;

namespace SportsWebPt.Platform.ServiceHost
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}