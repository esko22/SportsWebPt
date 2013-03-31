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
                        "~/Scripts/libs/jquery-{version}.js",
                        "~/Scripts/libs/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jsextlibs").Include(
                        "~/Scripts/libs/Uri.js",
                        "~/Scripts/libs/TrafficCop.js",
                        "~/Scripts/libs/infuser.js", // depends on TrafficCop
                        "~/Scripts/libs/knockout-{version}.js",
                        "~/Scripts/libs/underscore.js",
                        "~/Scripts/libs/backbone.js", // depends on underscore
                        "~/Scripts/libs/bootstrap.js", 
                        "~/Scripts/libs/knockback.js" // depends on backbone, knockout
                        ));

            bundles.Add(new ScriptBundle("~/bundles/utils")
                .IncludeDirectory("~/Scripts/utils/", "*.js", searchSubdirectories: false));

            bundles.Add(new ScriptBundle("~/bundles/models")
                .IncludeDirectory("~/Scripts/models/", "*.js", searchSubdirectories: false));


            bundles.Add(new ScriptBundle("~/bundles/baseapplib")
                    .IncludeDirectory("~/Scripts/app/", "*.js", searchSubdirectories: false));

            bundles.Add(new ScriptBundle("~/bundles/sharedapplib")
                    .IncludeDirectory("~/Areas/Main/Scripts/shared", "*.js", searchSubdirectories: false));

            bundles.Add(new ScriptBundle("~/bundles/mainapplib")
                    .IncludeDirectory("~/Areas/Main/Scripts/app", "*.js", searchSubdirectories: false));

            bundles.Add(new ScriptBundle("~/bundles/examineapplib")
                    .IncludeDirectory("~/Areas/Examine/Scripts/app", "*.js", searchSubdirectories: false));

            bundles.Add(new ScriptBundle("~/bundles/dashboardapplib")
                    .IncludeDirectory("~/Areas/Dashboard/Scripts/app", "*.js", searchSubdirectories: false)); 


            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-responsive.css",
                         "~/Content/sportsweb-pt.css",
                         "~/Content/icon-fonts.css"));


        }
    }
}