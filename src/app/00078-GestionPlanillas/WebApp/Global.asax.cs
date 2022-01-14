﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("BD_GestionPlanillasConnection", "TC_Usuario", "UserId", "UserName", autoCreateTables: false);
                if (Roles.Enabled)
                {
                    if (!Roles.RoleExists("Administrador"))
                        Roles.CreateRole("Administrador");

                    if (!Roles.RoleExists("Consulta"))
                        Roles.CreateRole("Consulta");

                    if (!Roles.RoleExists("Remuneraciones"))
                        Roles.CreateRole("Remuneraciones");

                }
            }

        }
    }
}
