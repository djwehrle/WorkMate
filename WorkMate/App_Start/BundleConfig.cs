using System.Web.Optimization;

namespace WorkMate
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/core").Include(
                        "~/Scripts/Shared/jquery-{version}.js",
                        "~/Scripts/Shared/bootstrap.js",
                        "~/Scripts/Shared/bootbox.js",
                        "~/Scripts/Shared/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/jqueryval").Include(
                        "~/Scripts/Shared/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/dataTables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js"));

            bundles.Add(new StyleBundle("~/bundles/content/core").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/content/dataTables").Include(
                        "~/Content/DataTables/css/jquery.dataTables.css"));
        }
    }
}