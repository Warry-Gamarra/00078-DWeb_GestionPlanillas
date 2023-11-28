using Domain.Entities;
using Domain.Enums;
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
                esCategoriaPrincipal = dto.esCategoriaPrincipal,
                seleccionado = true,
                dependenciaID = dto.dependenciaID,
                dependenciaCod = dto.dependenciaCod,
                dependenciaDesc = dto.dependenciaDesc,
                grupoTrabajoID = dto.grupoTrabajoID,
                grupoTrabajoCod = dto.grupoTrabajoCod,
                grupoTrabajoDesc = dto.grupoTrabajoDesc,
                estaHabilitado = dto.estaHabilitado
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
                conceptoReferenciaID = dto.conceptoReferenciaID,
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
                cui = dto.cui
            };

            return model;
        }

        public static ValorExternoConceptoModel ValorExternoConceptoDTO_To_ValorExternoConceptoModel(ValorExternoConceptoDTO dto)
        {
            var model = new ValorExternoConceptoModel()
            {
                conceptoExternoValorID = dto.conceptoExternoValorID,
                periodoID = dto.periodoID,
                anio = dto.anio,
                mes = dto.mes,
                mesDesc = dto.mesDesc,
                trabajadorID = dto.trabajadorID,
                tipoDocumentoDesc = dto.tipoDocumentoDesc,
                numDocumento = dto.numDocumento,
                datosPersona = String.Format("{0} {1} {2}", dto.apellidoPaterno, dto.apellidoMaterno, dto.nombre),
                trabajadorCategoriaPlanillaID = dto.trabajadorCategoriaPlanillaID,
                categoriaPlanillaID = dto.categoriaPlanillaID,
                categoriaPlanillaDesc = dto.categoriaPlanillaDesc,
                conceptoID = dto.conceptoID,
                conceptoCod = dto.conceptoCod,
                conceptoDesc = dto.conceptoDesc,
                tipoConceptoDesc = dto.tipoConceptoDesc,
                valorConcepto = dto.valorConcepto,
                proveedorID = dto.proveedorID,
                proveedorDesc = dto.proveedorDesc
            };

            return model;
        }

        public static UsuarioModel UsuarioDTO_To_UsuarioModel(UsuarioDTO dto)
        {
            var model = new UsuarioModel()
            {
                userId = dto.userId,
                userName = dto.userName,
                fecActualizaPassword = dto.fecActualizaPassword,
                cambiaPassword = dto.cambiaPassword,
                estaHabilitado = dto.estaHabilitado,
                usuarioCre = dto.usuarioCre,
                datosUsuarioID = dto.datosUsuarioID,
                numDoc = dto.numDoc,
                nomPersona = dto.nomPersona,
                correoUsuario = dto.correoUsuario,
                fecAlta = dto.fecAlta,
                roleId = dto.roleId,
                roleName = dto.roleName,
                dependenciaID = dto.dependenciaID,
                dependenciaDesc = dto.dependenciaDesc
            };

            return model;
        }

        public static PeriodoModel PeriodoDTO_To_PeriodoModel(PeriodoDTO dto)
        {
            var model = new PeriodoModel()
            {
                periodoID = dto.periodoID,
                anio = dto.anio,
                mes = dto.mes,
                mesDesc = dto.mesDesc
            };

            return model;
        }

        public static DependenciaModel DependenciaDTO_To_DependenciaModel(DependenciaDTO dto)
        {
            var model = new DependenciaModel()
            {
                dependenciaID = dto.dependenciaID,
                dependenciaCod = dto.dependenciaCod,
                dependenciaDesc = dto.dependenciaDesc,
                estaHabilitado = dto.estaHabilitado
            };

            return model;
        }

        public static DepActividadMetaModel DepActividadMetaDTO_To_DepActividadMetaModel(DepActividadMetaDTO dto)
        {
            var model = new DepActividadMetaModel()
            {
                depActividadMetaID = dto.depActividadMetaID,
                anio = dto.anio,
                categoriaPlanillaID = dto.categoriaPlanillaID,
                categoriaPlanillaDesc = dto.categoriaPlanillaDesc,
                dependenciaID = dto.dependenciaID,
                dependenciaCod = dto.dependenciaCod,
                dependenciaDesc = dto.dependenciaDesc,
                descripcion = dto.descripcion,
                actividadID = dto.actividadID,
                actividadCod = dto.actividadCod,
                actividadDesc = dto.actividadDesc,
                metaID = dto.metaID,
                metaCod = dto.metaCod,
                metaDesc = dto.metaDesc,
                categoriaPresupuestalID = dto.categoriaPresupuestalID,
                categoriaPresupCod = dto.categoriaPresupCod,
                categoriaPresupDesc = dto.categoriaPresupDesc
            };

            return model;
        }

        public static ActividadModel ActividadDTO_To_ActividadModel(ActividadDTO dto)
        {
            var model = new ActividadModel()
            {
                actividadID = dto.actividadID,
                actividadCod = dto.actividadCod,
                actividadDesc = dto.actividadDesc
            };

            return model;
        }

        public static MetaModel MetaDTO_To_MetaModel(MetaDTO dto)
        {
            var model = new MetaModel()
            {
                metaID = dto.metaID,
                metaCod = dto.metaCod,
                metaDesc = dto.metaDesc
            };

            return model;
        }
    }
}
