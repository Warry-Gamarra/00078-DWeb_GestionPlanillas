using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    internal static class Mapper
    {
        public static TrabajadorModel TrabajadorDTO_To_TrabajadorModel(TrabajadorDTO dto)
        {
            var model = new TrabajadorModel()
            {
                I_TrabajadorID = dto.I_TrabajadorID,
                C_TrabajadorCod = dto.C_TrabajadorCod,
                T_Nombre = dto.T_Nombre,
                T_ApellidoPaterno = dto.T_ApellidoPaterno,
                T_ApellidoMaterno = dto.T_ApellidoMaterno,
                I_TipoDocumentoID = dto.I_TipoDocumentoID,
                T_TipoDocumentoDesc = dto.T_TipoDocumentoDesc,
                C_NumDocumento = dto.C_NumDocumento,
                D_FechaIngreso = dto.D_FechaIngreso,
                I_RegimenID = dto.I_RegimenID,
                T_RegimenDesc = dto.T_RegimenDesc,
                I_EstadoID = dto.I_EstadoID,
                T_EstadoDesc = dto.T_EstadoDesc,
                I_VinculoID = dto.I_VinculoID,
                T_VinculoDesc = dto.T_VinculoDesc
            };

            return model;
        }

        public static ResumenPlanillaTrabajadorModel ResumenPlanillaTrabajadorDTO_To_ResumenPlanillaTrabajadorModel(ResumenPlanillaTrabajadorDTO dto)
        {
            var model = new ResumenPlanillaTrabajadorModel()
            {
                I_TrabajadorID = dto.I_TrabajadorID,
                C_TrabajadorCod = dto.C_TrabajadorCod,
                T_Nombre = dto.T_Nombre,
                T_ApellidoPaterno = dto.T_ApellidoPaterno,
                T_ApellidoMaterno = dto.T_ApellidoMaterno,
                I_TipoDocumentoID = dto.I_TipoDocumentoID,
                T_TipoDocumentoDesc = dto.T_TipoDocumentoDesc,
                C_NumDocumento = dto.C_NumDocumento,
                D_FechaIngreso = dto.D_FechaIngreso,
                I_RegimenID = dto.I_RegimenID,
                T_RegimenDesc = dto.T_RegimenDesc,
                I_EstadoID = dto.I_EstadoID,
                T_EstadoDesc = dto.T_EstadoDesc,
                I_VinculoID = dto.I_VinculoID,
                T_VinculoDesc = dto.T_VinculoDesc,
                I_TrabajadorPlanillaID = dto.I_TrabajadorPlanillaID,
                I_TotalRemuneracion = dto.I_TotalRemuneracion,
                I_TotalDescuento = dto.I_TotalDescuento,
                I_TotalReintegro = dto.I_TotalReintegro,
                I_TotalDeduccion = dto.I_TotalDeduccion,
                I_TotalSueldo = dto.I_TotalSueldo,
                I_PlanillaID = dto.I_PlanillaID,
                I_PeriodoID = dto.I_PeriodoID,
                I_Anio = dto.I_Anio,
                T_MesDesc = dto.T_MesDesc,
                I_CategoriaPlanillaID = dto.I_CategoriaPlanillaID,
                T_CategoriaPlanillaDesc = dto.T_CategoriaPlanillaDesc
            };

            return model;
        }

        public static DocenteModel DocenteDTO_To_DocenteModel(DocenteDTO dto)
        {
            var model = new DocenteModel()
            {
                I_TrabajadorID = dto.I_TrabajadorID,
                C_TrabajadorCod = dto.C_TrabajadorCod,
                T_Nombre = dto.T_Nombre,
                T_ApellidoPaterno = dto.T_ApellidoPaterno,
                T_ApellidoMaterno = dto.T_ApellidoMaterno,
                I_TipoDocumentoID = dto.I_TipoDocumentoID,
                T_TipoDocumentoDesc = dto.T_TipoDocumentoDesc,
                C_NumDocumento = dto.C_NumDocumento,
                D_FechaIngreso = dto.D_FechaIngreso,
                I_RegimenID = dto.I_RegimenID,
                T_RegimenDesc = dto.T_RegimenDesc,
                I_EstadoID = dto.I_EstadoID,
                T_EstadoDesc = dto.T_EstadoDesc,
                I_VinculoID = dto.I_VinculoID,
                T_VinculoDesc = dto.T_VinculoDesc,
                I_DocenteID = dto.I_DocenteID,
                I_CategoriaDocenteID = dto.I_CategoriaDocenteID,
                C_CategoriaDocenteCod = dto.C_CategoriaDocenteCod,
                T_CategoriaDocenteDesc = dto.T_CategoriaDocenteDesc,
                I_HorasDocenteID = dto.I_HorasDocenteID,
                I_Horas = dto.I_Horas,
                I_DedicacionDocenteID = dto.I_DedicacionDocenteID,
                C_DedicacionDocenteCod = dto.C_DedicacionDocenteCod,
                T_DedicacionDocenteDesc = dto.T_DedicacionDocenteDesc
            };

            return model;
        }

        public static AdministrativoModel AdministrativoDTO_To_AdministrativoModel(AdministrativoDTO dto)
        {
            var model = new AdministrativoModel()
            {
                I_TrabajadorID = dto.I_TrabajadorID,
                C_TrabajadorCod = dto.C_TrabajadorCod,
                T_Nombre = dto.T_Nombre,
                T_ApellidoPaterno = dto.T_ApellidoPaterno,
                T_ApellidoMaterno = dto.T_ApellidoMaterno,
                I_TipoDocumentoID = dto.I_TipoDocumentoID,
                T_TipoDocumentoDesc = dto.T_TipoDocumentoDesc,
                C_NumDocumento = dto.C_NumDocumento,
                D_FechaIngreso = dto.D_FechaIngreso,
                I_RegimenID = dto.I_RegimenID,
                T_RegimenDesc = dto.T_RegimenDesc,
                I_EstadoID = dto.I_EstadoID,
                T_EstadoDesc = dto.T_EstadoDesc,
                I_VinculoID = dto.I_VinculoID,
                T_VinculoDesc = dto.T_VinculoDesc,
                I_AdministrativoID = dto.I_AdministrativoID,
                I_GrupoOcupacionalID = dto.I_GrupoOcupacionalID,
                C_GrupoOcupacionalCod = dto.C_GrupoOcupacionalCod,
                T_GrupoOcupacionalDesc = dto.T_GrupoOcupacionalDesc,
                I_NivelRemunerativoID = dto.I_NivelRemunerativoID,
                C_NivelRemunerativoCod = dto.C_NivelRemunerativoCod,
                T_NivelRemunerativoDesc = dto.C_NivelRemunerativoCod
            };

            return model;
        }
    }
}
