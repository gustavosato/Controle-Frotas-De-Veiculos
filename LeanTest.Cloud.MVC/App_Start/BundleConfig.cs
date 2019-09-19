using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Lean.Test.Cloud.MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/smartWizard/smart_wizard.css",
                "~/Content/smartWizard/smart_wizard_theme_arrows.css",
                "~/Content/font-awesome.css",
                "~/Content/iziToast/iziToast.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/simuladorDetran/geradorDeDados.js",
                "~/Scripts/simuladorDetran/simuladorDetran.common.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/smartWizard/jquery.smartWizard.min.js",
                "~/Scripts/simuladorDetran/smartWizard.initialize.js",
                "~/Scripts/iziToast/iziToast.js",
                "~/Scripts/simuladorDetran/admin.common.js"));
        }
    }
}