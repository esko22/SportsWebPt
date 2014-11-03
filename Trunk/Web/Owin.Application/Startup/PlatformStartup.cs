﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Optimization;

using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Owin;
using Nancy.Security;
using Nancy.TinyIoc;
using Newtonsoft.Json.Serialization;
using Owin;
using SportsWebPt.Common.Logging;
using SportsWebPt.Common.Nancy;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    public class PlatformStartup : NancyWebApiBootstrapper
    {

        #region Methods

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<SportsWebPtClientSettings, SportsWebPtClientSettings>(WebPlatformConfigSettings.Instance.ServiceStackClientSettings);
            container.Register<IUserManagementService, UserManagementService>().AsMultiInstance();
            container.Register<IExamineService, ExamineService>().AsMultiInstance();
            container.Register<IResearchService, ResearchService>().AsMultiInstance();
            container.Register<IAdminService, AdminService>().AsMultiInstance();
            container.Register<IClinicService, ClinicService>().AsMultiInstance();
            container.Register<ITherapistService, TherapistService>().AsMultiInstance();
            container.Register<ILookupService, LookupService>().AsMultiInstance();
            container.Register<IEpisodeService, EpisodeService>().AsMultiInstance();
            container.Register<ISessionService, SessionService>().AsMultiInstance();

            ServicesContentMaps.CreateContentMaps();

        }

        protected override void BuildBundles()
        {
            // Force optimization to be on or off, regardless of web.config setting
            BundleTable.EnableOptimizations = false;

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
            var bundles = BundleTable.Bundles;

            bundles.UseCdn = false;
            
            // .debug.js, -vsdoc.js and .intellisense.js files 
            // are in BundleTable.Bundles.IgnoreList by default.
            // Clear out the list and add back the ones we want to ignore.
            // Don't add back .debug.js.
            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*intellisense.js");
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/libs/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/libs/angular/angular.js",
                        "~/Scripts/libs/angular/angular-route.js",
                        "~/Scripts/libs/angular/angular-sanitize.js",
                        "~/Scripts/libs/angular/angular-resource.js",
                        "~/Scripts/libs/angular/angular-touch.js",
                        "~/Scripts/libs/angular/ng-grid.js",
                        "~/Scripts/libs/angular/angular-google-analytics.js",
                        "~/Scripts/libs/angular-ui/ui-router.js",
                        "~/Scripts/libs/angular/angular-animate.js",
                        "~/Scripts/libs/angular-bootstrap/ui-bootstrap-tpls-{version}.js",
                        "~/Scripts/libs/angular-kendo/angular-kendo.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").IncludeDirectory("~/App/", "*.js", searchSubdirectories: true));

            bundles.Add(new ScriptBundle("~/bundles/jslibs").Include(
            "~/Scripts/libs/Uri.js",
            "~/Scripts/libs/underscore.js",
            "~/Scripts/libs/bootstrap.js",
            "~/Scripts/libs/kendo/kendo.web.min.js",
            "~/Scripts/libs/toastr.js",
            "~/Content/theme/plugins/flexslider/jquery.flexslider.js",
            "~/Content/theme/plugins/clingify/jquery.clingify.js",
            "~/Content/theme/plugins/jPanelMenu/jquery.jpanelmenu.js",
            "~/Content/theme/plugins/jRespond/js/jrespond.js"
            ));



            bundles.Add(new StyleBundle("~/content/kendo/bundle").Include(
                "~/Content/kendo/kendo.blueopal.min.css",
                "~/Content/kendo/kendo.common.min.css"
                ));

            bundles.Add(new StyleBundle("~/content/theme/css/appstrap").Include(
                "~/Content/bootstrap.css",
                "~/Content/theme/plugins/flexslider/flexslider.css",
                "~/Content/theme/plugins/animate/animate.css",
                "~/Content/theme/css/theme-style.css",
                "~/Content/theme/css/swpt-colorway.css",
                "~/Content/theme/plugins/clingify/clingify.css",
                "~/Content/theme/css/font-awesome.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/icon-fonts.css",
                "~/Content/toastr.css",
                "~/Content/ng-grid.css",
                "~/Content/sportsweb-pt.css"
                            ));
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            Csrf.Enable(pipelines);

            base.ApplicationStartup(container, pipelines);
        }


        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("/app")
            );

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("/scripts")
                );

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("/bundles")
                );

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("/content/css")
                );

        }

        protected override void ConfigureAuthMiddleware(IAppBuilder appBuilder)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            appBuilder.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = WebPlatformConfigSettings.Instance.ClientId,
                Authority = WebPlatformConfigSettings.Instance.AuthorityUri,
                RedirectUri = WebPlatformConfigSettings.Instance.CallbackUri,
                ResponseType = "id_token",
                Scope = "openid email profile user_detail",
                SignInAsAuthenticationType = "Cookies"
            });

        }

        protected override void ConfigureWebApi()
        {
            _httpConfiguration.MapHttpAttributeRoutes();
            // comment for IE 10. might be the cause of A callback parameter was not provided in the request URI exception
            //   _httpConfiguration.AddJsonpFormatter(callbackQueryParameter)

            _httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            var jsonFormatter = _httpConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();

            if (jsonFormatter != null)
                jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion


    }
}