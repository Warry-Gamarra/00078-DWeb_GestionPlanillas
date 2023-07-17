﻿using Domain.Entities;
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
                trabajadorID = dto.trabajadorID,
                trabajadorCod = dto.trabajadorCod,
                personaID = dto.personaID,
                nombre = dto.nombre,
                apellidoPaterno = dto.apellidoPaterno,
                apellidoMaterno = dto.apellidoMaterno,
                tipoDocumentoID = dto.tipoDocumentoID,
                tipoDocumentoDesc = dto.tipoDocumentoDesc,
                numDocumento = dto.numDocumento,
                fechaIngreso = dto.fechaIngreso,
                regimenID = dto.regimenID,
                regimenDesc = dto.regimenDesc,
                afpID = dto.afpID,
                afpDesc = dto.afpDesc,
                cuspp = dto.cuspp,
                estadoID = dto.estadoID,
                estadoDesc = dto.estadoDesc,
                vinculoID = dto.vinculoID,
                vinculoDesc = dto.vinculoDesc,
                trabajadorDependenciaID = dto.trabajadorDependenciaID,
                dependenciaID = dto.dependenciaID,
                dependenciaCod = dto.dependenciaCod,
                dependenciaDesc = dto.dependenciaDesc,
                cuentaBancariaID = dto.cuentaBancariaID,
                nroCuentaBancaria = dto.nroCuentaBancaria,
                bancoID = dto.bancoID,
                bancoDesc = dto.bancoDesc,
                bancoAbrv = dto.bancoAbrv
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
                docenteID = dto.docenteID,
                categoriaDocenteID = dto.categoriaDocenteID,
                categoriaDocenteCod = dto.categoriaDocenteCod,
                categoriaDocenteDesc = dto.categoriaDocenteDesc,
                horasDocenteID = dto.horasDocenteID,
                horas = dto.horas,
                dedicacionDocenteID = dto.dedicacionDocenteID,
                dedicacionDocenteCod = dto.dedicacionDocenteCod,
                dedicacionDocenteDesc = dto.dedicacionDocenteDesc
            };

            return model;
        }

        public static AdministrativoModel AdministrativoDTO_To_AdministrativoModel(AdministrativoDTO dto)
        {
            var model = new AdministrativoModel()
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
                administrativoID = dto.administrativoID,
                grupoOcupacionalID = dto.grupoOcupacionalID,
                grupoOcupacionalCod = dto.grupoOcupacionalCod,
                grupoOcupacionalDesc = dto.grupoOcupacionalDesc,
                nivelRemunerativoID = dto.nivelRemunerativoID,
                nivelRemunerativoCod = dto.nivelRemunerativoCod,
                nivelRemunerativoDesc = dto.nivelRemunerativoCod
            };

            return model;
        }

        public static TrabajadorCategoriaPlanillaModel TrabajadorCategoriaPlanillaDTO_To_TrabajadorCategoriaPlanillaModel(
            TrabajadorCategoriaPlanillaDTO dto)
        {
            var model = new TrabajadorCategoriaPlanillaModel()
            {
                trabajadorID = dto.trabajadorID,
                trabajadorCod = dto.trabajadorCod,
                personaID = dto.personaID,
                nombre = dto.nombre,
                apellidoPaterno = dto.apellidoPaterno,
                apellidoMaterno = dto.apellidoMaterno,
                tipoDocumentoID = dto.tipoDocumentoID,
                tipoDocumentoDesc = dto.tipoDocumentoDesc,
                numDocumento = dto.numDocumento,
                estadoID = dto.estadoID,
                estadoDesc = dto.estadoDesc,
                vinculoID = dto.vinculoID,
                vinculoDesc = dto.vinculoDesc,
                trabajadorCategoriaPlanillaID = dto.trabajadorCategoriaPlanillaID,
                categoriaPlanillaID = dto.categoriaPlanillaID,
                categoriaPlanillaDesc = dto.categoriaPlanillaDesc,
                seleccionado = true
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
                mesID = dto.mes,
                mesDesc = dto.mesDesc
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
