using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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
                I_PersonaID = dto.I_PersonaID,
                T_Nombre = dto.T_Nombre,
                T_ApellidoPaterno = dto.T_ApellidoPaterno,
                T_ApellidoMaterno = dto.T_ApellidoMaterno,
                I_TipoDocumentoID = dto.I_TipoDocumentoID,
                T_TipoDocumentoDesc = dto.T_TipoDocumentoDesc,
                C_NumDocumento = dto.C_NumDocumento,
                D_FechaIngreso = dto.D_FechaIngreso,
                I_RegimenID = dto.I_RegimenID,
                T_RegimenDesc = dto.T_RegimenDesc,
                I_AfpID = dto.I_AfpID,
                T_AfpDesc = dto.T_AfpDesc,
                T_Cuspp = dto.T_Cuspp,
                I_EstadoID = dto.I_EstadoID,
                T_EstadoDesc = dto.T_EstadoDesc,
                I_VinculoID = dto.I_VinculoID,
                T_VinculoDesc = dto.T_VinculoDesc,
                I_TrabajadorDependenciaID = dto.I_TrabajadorDependenciaID,
                I_DependenciaID = dto.I_DependenciaID,
                C_DependenciaCod = dto.C_DependenciaCod,
                T_DependenciaDesc = dto.T_DependenciaDesc,
                I_CuentaBancariaID = dto.I_CuentaBancariaID,
                T_NroCuentaBancaria = dto.T_NroCuentaBancaria,
                I_BancoID = dto.I_BancoID,
                T_BancoDesc = dto.T_BancoDesc,
                T_BancoAbrv = dto.T_BancoAbrv
            };

            return model;
        }

        public static ResumenPlanillaTrabajadorModel ResumenPlanillaTrabajadorDTO_To_ResumenPlanillaTrabajadorModel(ResumenPlanillaTrabajadorDTO dto)
        {
            var model = new ResumenPlanillaTrabajadorModel()
            {
                trabajadorID = dto.trabajadorID,
                trabajadorCod = dto.trabajadorCod,
                nombre = dto.nombre,
                apellidoPaterno = dto.apellidoPaterno,
                apellidoMaterno = dto.apellidoMaterno,
                tipoDocumentoID = dto.tipoDocumentoID,
                tipoDocumentoDesc = dto.tipoDocumentoDesc,
                numDocumento = dto.numDocumento,
                fechaIngreso = dto.fechaIngreso,
                regimenID = dto.regimenID,
                regimenDesc = dto.regimenDesc,
                estadoID = dto.estadoID,
                estadoDesc = dto.estadoDesc,
                vinculoID = dto.vinculoID,
                vinculoDesc = dto.vinculoDesc,
                trabajadorPlanillaID = dto.trabajadorPlanillaID,
                totalRemuneracion = dto.totalRemuneracion,
                totalReintegro = dto.totalReintegro,
                totalDeduccion = dto.totalDeduccion,
                totalBruto = dto.totalBruto,
                totalDescuento = dto.totalDescuento,
                totalSueldo = dto.totalSueldo,
                planillaID = dto.planillaID,
                periodoID = dto.periodoID,
                anio = dto.anio,
                mes = dto.mes,
                mesDesc = dto.mesDesc,
                categoriaPlanillaID = dto.categoriaPlanillaID,
                categoriaPlanillaDesc = dto.categoriaPlanillaDesc
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

        public static TrabajadorCategoriaPlanillaModel TrabajadorCategoriaPlanillaDTO_To_TrabajadorCategoriaPlanillaModel(
            TrabajadorCategoriaPlanillaDTO dto)
        {
            var model = new TrabajadorCategoriaPlanillaModel()
            {
                I_TrabajadorID = dto.I_TrabajadorID,
                C_TrabajadorCod = dto.C_TrabajadorCod,
                I_PersonaID = dto.I_PersonaID,
                T_Nombre = dto.T_Nombre,
                T_ApellidoPaterno = dto.T_ApellidoPaterno,
                T_ApellidoMaterno = dto.T_ApellidoMaterno,
                I_TipoDocumentoID = dto.I_TipoDocumentoID,
                T_TipoDocumentoDesc = dto.T_TipoDocumentoDesc,
                C_NumDocumento = dto.C_NumDocumento,
                I_EstadoID = dto.I_EstadoID,
                T_EstadoDesc = dto.T_EstadoDesc,
                I_VinculoID = dto.I_VinculoID,
                T_VinculoDesc = dto.T_VinculoDesc,
                I_TrabajadorCategoriaPlanillaID = dto.I_TrabajadorCategoriaPlanillaID,
                I_CategoriaPlanillaID = dto.I_CategoriaPlanillaID,
                T_CategoriaPlanillaDesc = dto.T_CategoriaPlanillaDesc,
                B_Checked = true
            };

            return model;
        }

        public static ConceptoModel ConceptoDTO_To_ConceptoModel(ConceptoDTO dto)
        {
            var model = new ConceptoModel()
            {
                conceptoID = dto.conceptoID,
                tipoConceptoID = dto.tipoConceptoID,
                tipoConceptoDesc = dto.tipoConceptoDesc,
                conceptoCod = dto.conceptoCod,
                conceptoDesc = dto.conceptoDesc,
                conceptoAbrv = dto.conceptoAbrv,
                estaHabilitado = dto.estaHabilitado
            };

            return model;
        }

        public static PlantillaPlanillaModel PlantillaPlanillaDTO_To_PlantillaPlanillaModel(PlantillaPlanillaDTO dto)
        {
            var model = new PlantillaPlanillaModel()
            { 
                plantillaPlanillaID = dto.plantillaPlanillaID,
                categoriaPlanillaID = dto.categoriaPlanillaID,
                plantillaPlanillaDesc = dto.plantillaPlanillaDesc,
                estaHabilitado  = dto.estaHabilitado,
                categoriaPlanillaDesc = dto.categoriaPlanillaDesc,
                clasePlanillaDesc = dto.clasePlanillaDesc,
                clasePlanillaID = dto.clasePlanillaID
            };

            return model;
        }

        public static ConceptoAsignadoPlantillaModel ConceptoAsignadoPlantillaDTO_To_ConceptoAsignadoPlantillaModel(
            ConceptoAsignadoPlantillaDTO dto)
        {
            var model = new ConceptoAsignadoPlantillaModel()
            {
                plantillaPlanillaConceptoID = dto.plantillaPlanillaConceptoID,
                plantillaPlanillaID = dto.plantillaPlanillaID,
                plantillaPlanillaDesc = dto.plantillaPlanillaDesc,
                categoriaPlanillaID = dto.categoriaPlanillaID,
                categoriaPlanillaDesc = dto.categoriaPlanillaDesc,
                clasePlanillaID = dto.clasePlanillaID,
                clasePlanillaDesc = dto.clasePlanillaDesc,
                tipoConceptoID = dto.tipoConceptoID,
                tipoConceptoDesc = dto.tipoConceptoDesc,
                esAdicion = dto.esAdicion,
                incluirEnTotalBruto = dto.incluirEnTotalBruto,
                conceptoID = dto.conceptoID,
                conceptoCod = dto.conceptoCod,
                conceptoDesc = dto.conceptoDesc,
                conceptoAbrv = dto.conceptoAbrv,
                esValorFijo = dto.esValorFijo,
                valorEsExterno = dto.valorEsExterno,
                valorConcepto = dto.valorConcepto,
                aplicarFiltro1 = dto.aplicarFiltro1,
                filtro1 = dto.filtro1,
                aplicarFiltro2 = dto.aplicarFiltro2,
                filtro2 = dto.filtro2,
                estaHabilitado = dto.estaHabilitado
            };

            return model;
        }

        public static ConceptoReferenciaModel ConceptoReferenciaDTO_To_ConceptoReferenciaModel(
            ConceptoReferenciaDTO dto)
        {
            var model = new ConceptoReferenciaModel()
            {
                id = dto.id,
                plantillaPlanillaConceptoID = dto.plantillaPlanillaConceptoID,
                plantillaPlanillaConceptoReferenciaID = dto.plantillaPlanillaConceptoReferenciaID,
                conceptoID = dto.conceptoID,
                conceptoCod = dto.conceptoCod,
                conceptoDesc = dto.conceptoDesc,
                conceptoAbrv = dto.conceptoAbrv
            };

            return model;
        }

        public static MesModel MesDTO_To_MesModel(MesDTO dto)
        {
            var model = new MesModel()
            {
                mesID = dto.I_Mes,
                mesDesc = dto.T_MesDesc
            };

            return model;
        }

        public static PersonaModel PersonaDTO_To_PersonaModel(PersonaDTO dto)
        {
            var model = new PersonaModel()
            {
                personaID = dto.personaID,
                tipoDocumentoID = dto.tipoDocumentoID,
                numDocumento = dto.numDocumento,
                nombre = dto.nombre,
                apellidoPaterno = dto.apellidoPaterno,
                apellidoMaterno = dto.apellidoMaterno,
                fecNac = dto.fecNac,
                cui = dto.cui,
                estaHabilitado = dto.estaHabilitado
            };

            return model;
        }
    }
}
