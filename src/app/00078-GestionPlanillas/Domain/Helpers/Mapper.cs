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
                I_PersonaID = view.I_PersonaID,
                T_Nombre = view.T_Nombre,
                T_ApellidoPaterno = view.T_ApellidoPaterno,
                T_ApellidoMaterno = view.T_ApellidoMaterno,
                I_TipoDocumentoID = view.I_TipoDocumentoID,
                T_TipoDocumentoDesc = view.T_TipoDocumentoDesc,
                C_NumDocumento = view.C_NumDocumento,
                D_FechaIngreso = view.D_FechaIngreso,
                I_RegimenID = view.I_RegimenID,
                T_RegimenDesc = view.T_RegimenDesc,
                I_AfpID = view.I_AfpID,
                T_AfpDesc = view.T_AfpDesc,
                T_Cuspp = view.T_Cuspp,
                I_EstadoID = view.I_EstadoID,
                T_EstadoDesc = view.T_EstadoDesc,
                I_VinculoID = view.I_VinculoID,
                T_VinculoDesc = view.T_VinculoDesc,
                I_TrabajadorDependenciaID = view.I_TrabajadorDependenciaID,
                I_DependenciaID = view.I_DependenciaID,
                C_DependenciaCod = view.C_DependenciaCod,
                T_DependenciaDesc = view.T_DependenciaDesc,
                I_CuentaBancariaID = view.I_CuentaBancariaID,
                T_NroCuentaBancaria = view.T_NroCuentaBancaria,
                I_BancoID = view.I_BancoID,
                T_BancoDesc = view.T_BancoDesc,
                T_BancoAbrv = view.T_BancoAbrv
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
                I_Mes = view.I_Mes,
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
                T_DedicacionDocenteDesc = view.T_DedicacionDocenteDesc,
                estaHabilitado = view.B_Habilitado
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
                T_NivelRemunerativoDesc = view.T_NivelRemunerativoDesc,
                estaHabilitado = view.B_Habilitado
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
                B_Habilitado = table.B_Habilitado
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
                B_Habilitado = table.B_Habilitado
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
                B_Habilitado = table.B_Habilitado
            };

            return vinculoDTO;
        }

        public static TipoDocumentoDTO TC_TipoDocumento_To_TipoDocumentoDTO(TC_TipoDocumento table)
        {
            var tipoDocumentoDTO = new TipoDocumentoDTO()
            {
                I_TipoDocumentoID = table.I_TipoDocumentoID,
                T_TipoDocumentoDesc = table.T_TipoDocumentoDesc,
                B_Habilitado = table.B_Habilitado
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
                B_Habilitado = table.B_Habilitado
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
                B_Habilitado = table.B_Habilitado
            };

            return dependenciaDTO;
        }

        public static AfpDTO TC_Afp_To_AfpDTO(TC_Afp table) 
        {
            var afpDTO = new AfpDTO()
            {
                I_AfpID = table.I_AfpID,
                T_AfpDesc = table.T_AfpDesc,
                C_AfpCod = table.C_AfpCod,
                B_Habilitado = table.B_Habilitado
            };

            return afpDTO;
        }

        public static GrupoOcupacionalDTO TC_GrupoOcupacional_To_GrupoOcupacionalDTO(TC_GrupoOcupacional table)
        {
            var grupoOcupacionalDTO = new GrupoOcupacionalDTO()
            {
                I_GrupoOcupacionalID = table.I_GrupoOcupacionalID,
                C_GrupoOcupacionalCod = table.C_GrupoOcupacionalCod,
                T_GrupoOcupacionalDesc = table.T_GrupoOcupacionalDesc,
                B_Habilitado = table.B_Habilitado
            };

            return grupoOcupacionalDTO;
        }

        public static NivelRemunerativoDTO TC_NivelRemunerativo_To_NivelRemunerativoDTO(TC_NivelRemunerativo table)
        {
            var nivelRemunerativoDTO = new NivelRemunerativoDTO()
            {
                I_NivelRemunerativoID = table.I_NivelRemunerativoID,
                C_NivelRemunerativoCod = table.C_NivelRemunerativoCod,
                T_NivelRemunerativoDesc = table.T_NivelRemunerativoDesc,
                B_Habilitado = table.B_Habilitado
            };

            return nivelRemunerativoDTO;
        }

        public static CategoriaDocenteDTO TC_CategoriaDocente_To_CategoriaDocenteDTO(TC_CategoriaDocente table)
        {
            var categoriaDocenteDTO = new CategoriaDocenteDTO()
            {
                I_CategoriaDocenteID = table.I_CategoriaDocenteID,
                T_CategoriaDocenteDesc = table.T_CategoriaDocenteDesc,
                C_CategoriaDocenteCod = table.C_CategoriaDocenteCod,
                B_Habilitado = table.B_Habilitado
            };

            return categoriaDocenteDTO;
        }

        public static PersonaDTO TC_Persona_To_PersonaDTO(TC_Persona table)
        {
            var personaDTO = new PersonaDTO()
            {
                I_PersonaID = table.I_PersonaID,
                I_TipoDocumentoID = table.I_TipoDocumentoID,
                C_NumDocumento = table.C_NumDocumento,
                T_Nombre = table.T_Nombre,
                T_ApellidoPaterno = table.T_ApellidoPaterno,
                T_ApellidoMaterno = table.T_ApellidoMaterno,
                D_FecNac = table.D_FecNac,
                C_Cui = table.C_Cui,
                B_Habilitado = table.B_Habilitado
            };

            return personaDTO;
        }

        public static CategoriaPlanillaDTO TC_CategoriaPlanilla_To_CategoriaPlanillaDTO(TC_CategoriaPlanilla table)
        {
            var categoriaPlanillaDTO = new CategoriaPlanillaDTO()
            {
                I_CategoriaPlanillaID = table.I_CategoriaPlanillaID,
                T_CategoriaPlanillaDesc = table.T_CategoriaPlanillaDesc,
                B_Habilitado = table.B_Habilitado
            };

            return categoriaPlanillaDTO;
        }

        public static MesDTO TR_Periodo_To_MesDTO(TR_Periodo table)
        {
            var mesDTO = new MesDTO()
            {
                I_Mes = table.I_Mes,
                T_MesDesc = table.T_MesDesc
            };

            return mesDTO;
        }

        public static TrabajadorCategoriaPlanillaDTO VW_TrabajadoresCategoriaPlanilla_To_TrabajadorCategoriaPlanillaDTO(
            VW_TrabajadoresCategoriaPlanilla view)
        {
            var trabajadorCategoriaPlanillaDTO = new TrabajadorCategoriaPlanillaDTO()
            {
                I_TrabajadorID = view.I_TrabajadorID,
                C_TrabajadorCod = view.C_TrabajadorCod,
                I_PersonaID = view.I_PersonaID,
                T_Nombre = view.T_Nombre,
                T_ApellidoPaterno = view.T_ApellidoPaterno,
                T_ApellidoMaterno = view.T_ApellidoMaterno,
                I_TipoDocumentoID = view.I_TipoDocumentoID,
                T_TipoDocumentoDesc = view.T_TipoDocumentoDesc,
                C_NumDocumento = view.C_NumDocumento,
                I_EstadoID = view.I_EstadoID,
                T_EstadoDesc = view.T_EstadoDesc,
                I_VinculoID = view.I_VinculoID,
                T_VinculoDesc = view.T_VinculoDesc,
                I_TrabajadorCategoriaPlanillaID = view.I_TrabajadorCategoriaPlanillaID,
                I_CategoriaPlanillaID = view.I_CategoriaPlanillaID,
                T_CategoriaPlanillaDesc = view.T_CategoriaPlanillaDesc
            };

            return trabajadorCategoriaPlanillaDTO;
        }

        public static ConceptoDTO VW_Conceptos_To_ConceptoDTO(VW_Conceptos view)
        {
            var conceptoDTO = new ConceptoDTO()
            {
                conceptoID = view.I_ConceptoID,
                tipoConceptoID = view.I_TipoConceptoID,
                tipoConceptoDesc = view.T_TipoConceptoDesc,
                conceptoCod = view.C_ConceptoCod,
                conceptoDesc = view.T_ConceptoDesc,
                conceptoAbrv = view.T_ConceptoAbrv,
                estaHabilitado = view.B_Habilitado
            };

            return conceptoDTO;
        }

        public static TipoConceptoDTO TC_TipoConcepto_To_TipoConceptoDTO(TC_TipoConcepto table)
        {
            var tipoConceptoDTO = new TipoConceptoDTO()
            {
                tipoConceptoID = table.I_TipoConceptoID,
                tipoConceptoDesc = table.T_TipoConceptoDesc,
                estaHabilitado = table.B_Habilitado
            };

            return tipoConceptoDTO;
        }

        public static PlantillaPlanillaDTO VW_PlantillasPlanilla_To_PlantillaPlanillaDTO(VW_PlantillasPlanilla view)
        {
            var plantillaPlanillaDTO = new PlantillaPlanillaDTO()
            {
                plantillaPlanillaID = view.I_PlantillaPlanillaID,
                plantillaPlanillaDesc = view.T_PlantillaPlanillaDesc,
                estaHabilitado = view.B_Habilitado,
                categoriaPlanillaID = view.I_CategoriaPlanillaID,
                categoriaPlanillaDesc = view.T_CategoriaPlanillaDesc,
                clasePlanillaID = view.I_ClasePlanillaID,
                clasePlanillaDesc = view.T_ClasePlanillaDesc
            };

            return plantillaPlanillaDTO;
        }

        public static ConceptoAsignadoPlantillaDTO VW_ConceptosAsignados_Plantilla_To_ConceptoAsignadoPlantillaDTO(
            VW_ConceptosAsignados_Plantilla view)
        {
            var conceptoAsignadoPlantillaDTO = new ConceptoAsignadoPlantillaDTO()
            {
                plantillaPlanillaConceptoID = view.I_PlantillaPlanillaConceptoID,
                plantillaPlanillaID = view.I_PlantillaPlanillaID,
                plantillaPlanillaDesc = view.T_PlantillaPlanillaDesc,
                categoriaPlanillaID = view.I_CategoriaPlanillaID,
                categoriaPlanillaDesc = view.T_CategoriaPlanillaDesc,
                clasePlanillaID = view.I_ClasePlanillaID,
                clasePlanillaDesc = view.T_ClasePlanillaDesc,
                tipoConceptoID = view.I_TipoConceptoID,
                tipoConceptoDesc = view.T_TipoConceptoDesc,
                esAdicion = view.B_EsAdicion,
                incluirEnTotalBruto = view.B_IncluirEnTotalBruto,
                conceptoID = view.I_ConceptoID,
                conceptoCod = view.C_ConceptoCod,
                conceptoDesc = view.T_ConceptoDesc,
                conceptoAbrv = view.T_ConceptoAbrv,
                esValorFijo = view.B_EsValorFijo,
                valorEsExterno = view.B_ValorEsExterno,
                valorConcepto = view.M_ValorConcepto,
                aplicarFiltro1 = view.B_AplicarFiltro1,
                filtro1 = view.I_Filtro1,
                aplicarFiltro2 = view.B_AplicarFiltro2,
                filtro2 = view.I_Filtro2,
                estaHabilitado = view.B_Habilitado
            };

            return conceptoAsignadoPlantillaDTO;
        }

        public static ConceptoReferenciaDTO VW_ConceptosReferencia_Plantilla_To_ConceptoReferenciaDTO(
            VW_ConceptosReferencia_Plantilla view)
        {
            var conceptoReferenciaDTO = new ConceptoReferenciaDTO()
            {
                id = view.I_ID,
                plantillaPlanillaConceptoID = view.I_PlantillaPlanillaConceptoID,
                plantillaPlanillaConceptoReferenciaID = view.I_PlantillaPlanillaConceptoReferenciaID,
                conceptoID = view.I_ConceptoID,
                conceptoCod = view.C_ConceptoCod,
                conceptoDesc = view.T_ConceptoDesc,
                conceptoAbrv = view.T_ConceptoAbrv
            };

            return conceptoReferenciaDTO;
        }
    }
}
