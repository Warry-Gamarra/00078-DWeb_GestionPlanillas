﻿using Data.Connection;
using Data.Views;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    internal static class Mapper
    {
        public static Response Result_To_Response(Result result)
        {
            var response = new Response()
            {
                Success = result.Success,
                Message = result.Message
            };

            return response;
        }

        public static TrabajadorDTO VW_Trabajadores_To_TrabajadorDTO(VW_Trabajadores view)
        {
            var trabajadorDTO = new TrabajadorDTO()
            {
                I_TrabajadorID = view.I_TrabajadorID,
                C_TrabajadorCod = view.C_TrabajadorCod,
                T_Nombre = view.T_Nombre,
                T_ApellidoPaterno = view.T_ApellidoPaterno,
                T_ApellidoMaterno = view.T_ApellidoMaterno,
                I_TipoDocumentoID = view.I_TipoDocumentoID,
                T_TipoDocumentoDesc = view.T_TipoDocumentoDesc,
                C_NumDocumento = view.C_NumDocumento,
                D_FechaIngreso = view.D_FechaIngreso,
                I_RegimenID = view.I_RegimenID,
                T_RegimenDesc = view.T_RegimenDesc,
                I_EstadoID = view.I_EstadoID,
                T_EstadoDesc = view.T_EstadoDesc,
                I_VinculoID = view.I_VinculoID,
                T_VinculoDesc = view.T_VinculoDesc
            };

            return trabajadorDTO;
        }

        public static ResumenPlanillaTrabajadorDTO VW_ResumenPlanillaTrabajador_To_ResumenPlanillaTrabajadorDTO(VW_ResumenPlanillaTrabajador view)
        {
            var trabajadorDTO = new ResumenPlanillaTrabajadorDTO()
            {
                I_TrabajadorID = view.I_TrabajadorID,
                C_TrabajadorCod = view.C_TrabajadorCod,
                T_Nombre = view.T_Nombre,
                T_ApellidoPaterno = view.T_ApellidoPaterno,
                T_ApellidoMaterno = view.T_ApellidoMaterno,
                I_TipoDocumentoID = view.I_TipoDocumentoID,
                T_TipoDocumentoDesc = view.T_TipoDocumentoDesc,
                C_NumDocumento = view.C_NumDocumento,
                D_FechaIngreso = view.D_FechaIngreso,
                I_RegimenID = view.I_RegimenID,
                T_RegimenDesc = view.T_RegimenDesc,
                I_EstadoID = view.I_EstadoID,
                T_EstadoDesc = view.T_EstadoDesc,
                I_VinculoID = view.I_VinculoID,
                T_VinculoDesc = view.T_VinculoDesc,
                I_TrabajadorPlanillaID = view.I_TrabajadorPlanillaID,
                I_TotalRemuneracion = view.I_TotalRemuneracion,
                I_TotalDescuento = view.I_TotalDescuento,
                I_TotalReintegro = view.I_TotalReintegro,
                I_TotalDeduccion = view.I_TotalDeduccion,
                I_TotalSueldo = view.I_TotalSueldo,
                I_PlanillaID = view.I_PlanillaID,
                I_PeriodoID = view.I_PeriodoID,
                I_Anio = view.I_Anio,
                T_MesDesc = view.T_MesDesc,
                I_CategoriaPlanillaID = view.I_CategoriaPlanillaID,
                T_CategoriaPlanillaDesc = view.T_CategoriaPlanillaDesc
            };

            return trabajadorDTO;
        }

        public static DocenteDTO VW_Docentes_To_DocenteDTO(VW_Docentes view)
        {
            var docenteDTO = new DocenteDTO()
            {
                I_TrabajadorID = view.I_TrabajadorID,
                C_TrabajadorCod = view.C_TrabajadorCod,
                T_Nombre = view.T_Nombre,
                T_ApellidoPaterno = view.T_ApellidoPaterno,
                T_ApellidoMaterno = view.T_ApellidoMaterno,
                I_TipoDocumentoID = view.I_TipoDocumentoID,
                T_TipoDocumentoDesc = view.T_TipoDocumentoDesc,
                C_NumDocumento = view.C_NumDocumento,
                D_FechaIngreso = view.D_FechaIngreso,
                I_RegimenID = view.I_RegimenID,
                T_RegimenDesc = view.T_RegimenDesc,
                I_EstadoID = view.I_EstadoID,
                T_EstadoDesc = view.T_EstadoDesc,
                I_VinculoID = view.I_VinculoID,
                T_VinculoDesc = view.T_VinculoDesc,
                I_DocenteID = view.I_DocenteID,
                I_CategoriaDocenteID = view.I_CategoriaDocenteID,
                C_CategoriaDocenteCod = view.C_CategoriaDocenteCod,
                T_CategoriaDocenteDesc = view.T_CategoriaDocenteDesc,
                I_HorasDocenteID = view.I_HorasDocenteID,
                I_Horas = view.I_Horas,
                I_DedicacionDocenteID = view.I_DedicacionDocenteID,
                C_DedicacionDocenteCod = view.C_DedicacionDocenteCod,
                T_DedicacionDocenteDesc = view.T_DedicacionDocenteDesc
            };

            return docenteDTO;
        }

        public static AdministrativoDTO VW_Administrativo_To_AdministrativoDTO(VW_Administrativo view)
        {
            var administrativoDTO = new AdministrativoDTO()
            {
                I_TrabajadorID = view.I_TrabajadorID,
                C_TrabajadorCod = view.C_TrabajadorCod,
                T_Nombre = view.T_Nombre,
                T_ApellidoPaterno = view.T_ApellidoPaterno,
                T_ApellidoMaterno = view.T_ApellidoMaterno,
                I_TipoDocumentoID = view.I_TipoDocumentoID,
                T_TipoDocumentoDesc = view.T_TipoDocumentoDesc,
                C_NumDocumento = view.C_NumDocumento,
                D_FechaIngreso = view.D_FechaIngreso,
                I_RegimenID = view.I_RegimenID,
                T_RegimenDesc = view.T_RegimenDesc,
                I_EstadoID = view.I_EstadoID,
                T_EstadoDesc = view.T_EstadoDesc,
                I_VinculoID = view.I_VinculoID,
                T_VinculoDesc = view.T_VinculoDesc,
                I_AdministrativoID = view.I_AdministrativoID,
                I_GrupoOcupacionalID = view.I_GrupoOcupacionalID,
                C_GrupoOcupacionalCod = view.C_GrupoOcupacionalCod,
                T_GrupoOcupacionalDesc = view.T_GrupoOcupacionalDesc,
                I_NivelRemunerativoID = view.I_NivelRemunerativoID,
                C_NivelRemunerativoCod = view.C_NivelRemunerativoCod,
                T_NivelRemunerativoDesc = view.T_NivelRemunerativoDesc
            };

            return administrativoDTO;
        }
    }
}
