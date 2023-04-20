﻿using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Assets/bootstrap/css/bootstrap.min.css",
                "~/Assets/grid-mvc/css/Gridmvc.css",
                "~/Assets/toastr/css/toastr.min.css",
                "~/Assets/application/css/main.css",
                "~/Assets/application/css/loaders.css",
                "~/Assets/application/css/sidebar.css"));

            bundles.Add(new StyleBundle("~/content/datetime").Include(
                    "~/Assets/bootstrap-datepicker/css/bootstrap-datepicker.css",
                    "~/Assets/bootstrap-datepicker/css/bootstrap-timepicker.css"));

            bundles.Add(new StyleBundle("~/content/fonts").Include(
                "~/Assets/font-awesome/css/font-awesome.min.css",
                "~/Assets/bootstrap/css/bootstrap-icons.css"));

            bundles.Add(new StyleBundle("~/content/select").Include(
                "~/Assets/select2/css/select2.min.css",
                "~/Assets/select2/css/select2-bootstrap4.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Assets/jquery/jquery-3.3.1.min.js",
                "~/Assets/jquery/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Assets/jquery/jquery.validate*",
                "~/Assets/jquery/jquery.validate.date.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Assets/bootstrap/js/popper.min.js",
                "~/Assets/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/select").Include(
                "~/Assets/select2/js/select2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetime").Include(
                "~/Assets/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/Assets/bootstrap-datepicker/js/bootstrap-datepicker.es.js",
                "~/Assets/bootstrap-datepicker/js/bootstrap-timepicker.js",
                "~/Assets/bootstrap-datepicker/js/datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Assets/grid-mvc/js/gridmvc.js",
                "~/Assets/toastr/js/toastr.min.js",
                "~/Assets/toastr/js/toastr.config.js",
                "~/Assets/application/js/main.js",
                "~/Assets/application/js/sidebar.js",
                "~/Assets/application/js/constantes.js"));

        }
    }
}
