﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ResumenPlanillaTrabajadorModel
    {
        public int I_TrabajadorID { get; set; }

        public string C_TrabajadorCod { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public string T_ApellidosNombre
        {
            get { return String.Format("{0} {1}, {2}", T_ApellidoPaterno, T_ApellidoMaterno, T_Nombre); }
        }

        public int I_TipoDocumentoID { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public string C_NumDocumento { get; set; }

        public DateTime? D_FechaIngreso { get; set; }

        public string T_FechaIngreso
        {
            get
            {
                return D_FechaIngreso.HasValue ? D_FechaIngreso.Value.ToString("dd/MM/yyyy") : "";
            }

        }

        public int? I_RegimenID { get; set; }

        public string T_RegimenDesc { get; set; }

        public int? I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        public int? I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public int I_TrabajadorPlanillaID { get; set; }

        public decimal I_TotalRemuneracion { get; set; }

        public string T_TotalRemuneracion
        {
            get
            {
                return I_TotalRemuneracion.ToString("#,0.00");
            }

        }

        public decimal I_TotalDescuento { get; set; }

        public string T_TotalDescuento
        {
            get
            {
                return I_TotalDescuento.ToString("#,0.00");
            }

        }

        public decimal I_TotalReintegro { get; set; }

        public string T_TotalReintegro
        {
            get
            {
                return I_TotalReintegro.ToString("#,0.00");
            }

        }

        public decimal I_TotalDeduccion { get; set; }

        public string T_TotalDeduccion
        {
            get
            {
                return I_TotalDeduccion.ToString("#,0.00");
            }

        }

        public decimal I_TotalSueldo { get; set; }

        public string T_TotalSueldo
        {
            get
            {
                return I_TotalSueldo.ToString("#,0.00");
            }

        }

        public int I_PlanillaID { get; set; }

        public int I_PeriodoID { get; set; }

        public int I_Anio { get; set; }

        public int I_Mes { get; set; }

        public string T_MesDesc { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }
    }
}
