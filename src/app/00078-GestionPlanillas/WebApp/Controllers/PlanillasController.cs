﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;

namespace WebApp.Controllers
{
    public class PlanillasController : Controller
    {
        private IPlanillaServiceFacade planillaServiceFacade;

        public PlanillasController()
        {
            planillaServiceFacade = new PlanillaServiceFacade();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Resumen Planilla de Trabajadores";

            var lista = planillaServiceFacade.ListarResumenPlanillaTrabajador();

            return View(lista);
        }
    }
}