using System.Web;
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
                "~/Assets/jquery/css/jquery-ui.min.css",
                "~/Assets/toastr/css/toastr.min.css",
                "~/Assets/application/css/main.css",
                "~/Assets/application/css/loaders.css",
                "~/Assets/application/css/sidebar.css",
                "~/Assets/datatables/css/jquery.dataTables.min.css"));

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
                "~/Assets/jquery/js/jquery-3.3.1.min.js",
                "~/Assets/jquery/js/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Assets/jquery/js/jquery.validate*",
                "~/Assets/jquery/js/jquery.validate.date.js"));

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
                "~/Assets/toastr/js/toastr.min.js",
                "~/Assets/toastr/js/toastr.config.js",
                "~/Assets/application/js/main.js",
                "~/Assets/application/js/sidebar.js",
                "~/Assets/application/js/constantes.js",
                "~/Assets/datatables/js/jquery.dataTables.min.js",
                "~/Assets/sweetalert/sweetalert2@11.js"));

            bundles.Add(new StyleBundle("~/content/fileinput").Include(
                "~/Assets/fileInput/css/fileinput.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                "~/Assets/fileInput/js/fileinput.min.js",
                "~/Assets/fileInput/js/locales/es.js",
                "~/Assets/jquery/js/jquery.form.js"));

        }
    }
}
