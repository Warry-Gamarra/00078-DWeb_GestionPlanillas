using Data.Connection;
using Data.Tables;
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

        public static EstadoDTO TC_Estado_To_EstadoDTO(TC_Estado table)
        {
            var estadoDTO = new EstadoDTO()
            { 
                I_EstadoID = table.I_EstadoID,
                T_EstadoDesc = table.T_EstadoDesc,
                C_EstadoCod = table.C_EstadoCod,
                B_Habilitado = table.B_Habilitado,
                B_Eliminado = table.B_Eliminado
            };

            return estadoDTO;
        }

        public static RegimenDTO TC_Regimen_To_RegimenDTO(TC_Regimen table)
        {
            var regimenDTO = new RegimenDTO()
            {
                I_RegimenID = table.I_RegimenID,
                T_RegimenDesc = table.T_RegimenDesc,
                C_RegimenCod = table.C_RegimenCod,
                B_Habilitado = table.B_Habilitado,
                B_Eliminado = table.B_Eliminado
            };

            return regimenDTO;
        }

        public static VinculoDTO TC_Vinculo_To_VinculoDTO(TC_Vinculo table)
        {
            var vinculoDTO = new VinculoDTO()
            {
                I_VinculoID = table.I_VinculoID,
                T_VinculoDesc = table.T_VinculoDesc,
                C_VinculoCod = table.C_VinculoCod,
                B_Habilitado = table.B_Habilitado,
                B_Eliminado = table.B_Eliminado
            };

            return vinculoDTO;
        }

        public static TipoDocumentoDTO TC_TipoDocumento_To_TipoDocumentoDTO(TC_TipoDocumento table)
        {
            var tipoDocumentoDTO = new TipoDocumentoDTO()
            {
                I_TipoDocumentoID = table.I_TipoDocumentoID,
                T_TipoDocumentoDesc = table.T_TipoDocumentoDesc,
                B_Habilitado = table.B_Habilitado,
                B_Eliminado = table.B_Eliminado
            };

            return tipoDocumentoDTO;
        }

        public static BancoDTO TC_Banco_To_BancoDTO(TC_Banco table)
        {
            var bancoDTO = new BancoDTO()
            {
                I_BancoID = table.I_BancoID,
                T_BancoDesc = table.T_BancoDesc,
                T_BancoAbrv = table.T_BancoAbrv,
                B_Habilitado = table.B_Habilitado,
                B_Eliminado = table.B_Eliminado
            };

            return bancoDTO;
        }

        public static DependenciaDTO TC_Dependencia_To_DependenciaDTO(TC_Dependencia table)
        {
            var dependenciaDTO = new DependenciaDTO()
            {
                I_DependenciaID = table.I_DependenciaID,
                T_DependenciaDesc = table.T_DependenciaDesc,
                C_DependenciaCod = table.C_DependenciaCod,
                B_Habilitado = table.B_Habilitado,
                B_Eliminado = table.B_Eliminado
            };

            return dependenciaDTO;
        }
    }
}
