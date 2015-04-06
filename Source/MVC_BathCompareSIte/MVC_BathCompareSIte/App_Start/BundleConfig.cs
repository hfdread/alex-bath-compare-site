using System.Web;
using System.Web.Optimization;

namespace MVC_BathCompareSIte
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                    "~/Scripts/kendo/kendo.all.min.js",
                    "~/Scripts/kendo/kendo.timezones.min.js",
                    "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/page").Include(
                    "~/Scripts/page.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                    "~/Content/kendo/kendo.common.min.css",
                    "~/Content/kendo/kendo.rtl.min.css",
                    "~/Content/kendo/kendo.default.min.css",
                    "~/Content/kendo/kendo.default.mobile.min.css",
                    "~/Content/kendo/kendo.dataviz.min.css",
                    "~/Content/kendo/kendo.dataviz.default.min.css",
                    "~/Content/kendo/kendo.mobile.all.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/placeholder").Include(
                    "~/Scripts/jquery.placeholder.js",
                    "~/Scripts/jquery.placeholder.min.js"
                ));

            bundles.Add(new StyleBundle("~/CPanel/login").Include(
                    "~/Content/cplogin.css",
                    "~/Content/bootstrap.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                "angular.js",
                "angular-animate.js",
                "angular-aria.js",
                "angular-messages.js",
                "angular-sanitize.js"
                ));

            bundles.IgnoreList.Clear();
        }
    }
}
