﻿using System.Web;
using System.Web.Optimization;

namespace SportsWebPt.Platform.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Force optimization to be on or off, regardless of web.config setting
            BundleTable.EnableOptimizations = false;

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif

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
    }
}