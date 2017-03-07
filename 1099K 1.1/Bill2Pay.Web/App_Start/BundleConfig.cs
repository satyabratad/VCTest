using System.Web;
using System.Web.Optimization;

namespace Bill2Pay.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*",
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/pace.js",
                "~/Scripts/jquery.min.js",
                "~/Scripts/angular.min.js",
                "~/Scripts//bootstrap.min.js",
                "~/Scripts/blockui.min.js",
                "~/Scripts/d3.min.js",
                "~/Scripts/d3_tooltip.js",
                "~/Scripts/switchery.min.js",
                "~/Scripts/uniform.min.js",
                "~/Scripts/bootstrap_multiselect.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/daterangepicker.js",
                "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                      "~/Scripts/Custom/Common.js",
                      "~/Scripts/Custom/Account.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/Site.css",
                      "~/Content/css.css",
                       "~/Content/styles.css",
                        "~/Content/core.css",
                          "~/Content/components.css",
                           "~/Content/colors.css",
                "~/Content/font-awesome-4.7.0/css/font-awesome.min.css",
                            "~/Content/bootstrap.css"));
        }
    }
}
