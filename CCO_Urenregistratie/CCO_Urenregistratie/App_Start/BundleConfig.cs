using System.Web;
using System.Web.Optimization;

namespace CCO_Urenregistratie
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
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap.tooltip.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
                      "~/Scripts/icheck.min.js",      
                      "~/Scripts/icheck.config.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Scripts/chart.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/selector").Include(
                      "~/Scripts/selector.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert2").Include(
                      "~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                       "~/Scripts/toastr.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/icheck/all.css",
                      "~/Content/bootflat.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/bootstrap-datepicker3.standalone.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/toastr.min.css",
                      "~/Content/main.css"));
        }
    }
}
