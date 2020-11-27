using System.Web.Optimization;

namespace InternetAuction.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/customScripts/handlePlaceBetRequest").Include(
               "~/Scripts/customScripts/handlePlaceBetRequest.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts/searchBlockToggle").Include(
               "~/Scripts/customScripts/searchBlockToggle.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts/detailsInfoBlockToggle").Include(
               "~/Scripts/customScripts/detailsInfoBlockToggle.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts/userSearchToggle").Include(
               "~/Scripts/customScripts/userSearchToggle.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts/confirmDelete").Include(
               "~/Scripts/customScripts/confirmDelete.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts/confirmPurchase").Include(
               "~/Scripts/customScripts/confirmPurchase.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts/confirmSale").Include(
               "~/Scripts/customScripts/confirmSale.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js").Include("~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
