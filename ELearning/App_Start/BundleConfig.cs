using System.Web;
using System.Web.Optimization;
using System.Configuration;

namespace ELearning
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/jquery-dateFormat.js",
                      "~/Scripts/bootstrap-table-2.3.js",
                      "~/Scripts/bootstrap-table-zh-CN.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/bootstrap-datetimepicker.zh-CN.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/Models/DataGrid.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-table.css",
                      "~/Content/bootstrap-datetimepicker.css",
                       "~/Content/layout.css",
                       "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Login/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style-metro.css",
                      "~/Content/site.css",
                      "~/Content/login.css"));


            // 将 EnableOptimizations 设为 false 以进行调试。有关详细信息，
            // 请访问 http://go.microsoft.com/fwlink/?LinkId=301862
            var env = ConfigurationManager.AppSettings.Get("Env");
            BundleTable.EnableOptimizations = env == "Prod";
        }
    }
}
