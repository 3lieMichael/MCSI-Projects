using System.Web;
using System.Web.Optimization;

namespace Web.SP
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/lib/jquery.preloader.js",
                        "~/Scripts/lib/nivo-lightbox.min.js",
                        "~/Scripts/lib/jquery.superslides.min.js",
                        "~/Scripts/lib/smoothscroll.js",
                        "~/Scripts/lib/jquery.bxslider.min.js",
                        "~/Scripts/lib/jquery.mixitup.min.js",
                        "~/Scripts/lib/jquery.backtotop.js",
                        "~/Scripts/lib/jquery.carouFredSel-6.2.1-packed.js",
                        "~/Scripts/lib/retina.min.js",
                        "~/Scripts/lib/retina.min.js",
                        "~/Scripts/semantic.min.js",
                        "~/Scripts/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/fontawesome-all.min.js",
                      "~/Scripts/main.js",
                      "~/Scripts/myMain.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/helper.css",
                      "~/Content/semantic.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/nivo-lightbox.css",
                      "~/Content/default.css",
                      "~/Content/datatables.min.css",
                      "~/Content/bootstrap-select.min.css",
                      "~/Content/style.css",
                      "~/Content/myStyles.css",
                      "~/Content/purple.css",
                      "~/Content/Site.css"));
        }
    }
}
