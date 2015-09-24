using System.Web.Optimization;

namespace Icode.Blog.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //后台style
            bundles.Add(
                new StyleBundle("~/Bundles/AdminArea/css")
                    .Include(
                    "~/Areas/AdminArea/Content/vendors/jquery-ui-1.10.4.custom/css/ui-lightness/jquery-ui-1.10.4.custom.min.css",
                    "~/Areas/AdminArea/Content/vendors/font-awesome/css/font-awesome.min.css",
                    "~/Areas/AdminArea/Content/vendors/bootstrap/css/bootstrap.min.css",
                    "~/Areas/AdminArea/Content/vendors/intro.js/introjs.css",
                    "~/Areas/AdminArea/Content/vendors/calendar/zabuto_calendar.min.css",
                    "~/Areas/AdminArea/Content/vendors/sco.message/sco.message.css",
                    "~/Areas/AdminArea/Content/vendors/intro.js/introjs.css",
                    "~/Areas/AdminArea/Content/vendors/animate.css/animate.css",
                    "~/Areas/AdminArea/Content/vendors/jquery-pace/pace.css",
                    "~/Areas/AdminArea/Content/vendors/bootstrap-daterangepicker/daterangepicker-bs3.css",
                    "~/Areas/AdminArea/Content/vendors/jquery-toastr/toastr.min.css"
                    )
                );

            //后台script
            bundles.Add(
                new ScriptBundle("~/Bundles/AdminArea/js")
                .Include(
                   "~/Areas/AdminArea/Content/js/jquery-1.10.2.min.js",
                   "~/Areas/AdminArea/Content/js/jquery-migrate-1.2.1.min.js",
                   "~/Areas/AdminArea/Content/js/jquery-ui.js",
                   "~/Areas/AdminArea/Content/vendors/bootstrap/js/bootstrap.min.js",
                   "~/Areas/AdminArea/Content/vendors/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js",
                    "~/Areas/AdminArea/Content/js/html5shiv.js",
                    "~/Areas/AdminArea/Content/js/respond.min.js",
                    "~/Areas/AdminArea/Content/vendors/metisMenu/jquery.metisMenu.js",
                    "~/Areas/AdminArea/Content/vendors/slimScroll/jquery.slimscroll.js",
                    "~/Areas/AdminArea/Content/vendors/jquery-cookie/jquery.cookie.js",
                    "~/Areas/AdminArea/Content/vendors/jquery-notific8/jquery.notific8.min.js",
                    "~/Areas/AdminArea/Content/vendors/jquery-highcharts/highcharts.js",
                    "~/Areas/AdminArea/Content/js/jquery.menu.js",
                    "~/Areas/AdminArea/Content/vendors/jquery-pace/pace.min.js",
                    "~/Areas/AdminArea/Content/vendors/holder/holder.js",
                    "~/Areas/AdminArea/Content/vendors/responsive-tabs/responsive-tabs.js",
                    "~/Areas/AdminArea/Content/vendors/jquery-news-ticker/jquery.newsTicker.min.js",
                    "~/Areas/AdminArea/Content/vendors/moment/moment.js",
                    "~/Areas/AdminArea/Content/vendors/bootstrap-datepicker/js/bootstrap-datepicker.js",
                    "~/Areas/AdminArea/Content/vendors/bootstrap-daterangepicker/daterangepicker.js",
                    "~/Areas/AdminArea/Content/js/main.js",
                    "~/Areas/AdminArea/Content/vendors/sco.message/sco.message.js",
                    "~/Areas/AdminArea/Content/vendors/jquery-toastr/toastr.min.js"
                    )
                );

            bundles.Add(
                new StyleBundle("~/Bundles/Abp")
                .Include(
                     "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js"
             ));
        }
    }
}