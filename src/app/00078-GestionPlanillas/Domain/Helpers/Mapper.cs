﻿using Data.Connection;
using Data.Procedures;
using Data.Tables;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClosedXML.Excel.XLPredefinedFormat;

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
                trabajadorID = view.I_TrabajadorID,
                trabajadorCod = view.C_TrabajadorCod,
                codigoPlaza = view.C_CodigoPlaza,
                personaID = view.I_PersonaID,
                nombre = view.T_Nombre,
                apellidoPaterno = view.T_ApellidoPaterno,
                apellidoMaterno = view.T_ApellidoMaterno,
                tipoDocumentoID = view.I_TipoDocumentoID,
                tipoDocumentoDesc = view.T_TipoDocumentoDesc,
                numDocumento = view.C_NumDocumento,
                sexoID = view.I_SexoID,
                sexoDesc = view.T_SexoDesc,
                fechaIngreso = view.D_FechaIngreso,
                regimenID = view.I_RegimenID,
                regimenDesc = view.T_RegimenDesc,
                afpID = view.I_AfpID,
                afpDesc = view.T_AfpDesc,
                cuspp = view.T_Cuspp,
                estadoID = view.I_EstadoID,
                estadoDesc = view.T_EstadoDesc,
                vinculoID = view.I_VinculoID,
                vinculoCod = view.C_VinculoCod,
                vinculoDesc = view.T_VinculoDesc,
                trabajadorDependenciaID = view.I_TrabajadorDependenciaID,
                dependenciaID = view.I_DependenciaID,
                dependenciaCod = view.C_DependenciaCod,
                dependenciaDesc = view.T_DependenciaDesc,
                cuentaBancariaID = view.I_CuentaBancariaID,
                nroCuentaBancaria = view.T_NroCuentaBancaria,
                tipoCuentaBancariaID = view.I_TipoCuentaBancariaID,
                tipoCuentaBancariaDesc = view.T_TipoCuentaBancariaDesc,
                bancoID = view.I_BancoID,
                bancoDesc = view.T_BancoDesc,
                bancoAbrv = view.T_BancoAbrv,
                tipoDocumentoCod = view.T_TipoDocumentoCod
            };

            return trabajadorDTO;
        }

        public static ResumenPlanillaTrabajadorDTO USP_S_ListarResumenPlanillaTrabajador_To_ResumenPlanillaTrabajadorDTO(USP_S_ListarResumenPlanillaTrabajador sp)
        {
            var trabajadorDTO = new ResumenPlanillaTrabajadorDTO()
            {
                trabajadorID = sp.I_TrabajadorID,
                trabajadorCod = sp.C_TrabajadorCod,
                nombre = sp.T_Nombre,
                apellidoPaterno = sp.T_ApellidoPaterno,
                apellidoMaterno = sp.T_ApellidoMaterno,
                tipoDocumentoID = sp.I_TipoDocumentoID,
                tipoDocumentoDesc = sp.T_TipoDocumentoDesc,
                numDocumento = sp.C_NumDocumento,
                regimenID = sp.I_RegimenID,
                regimenDesc = sp.T_RegimenDesc,
                estadoID = sp.I_EstadoID,
                estadoDesc = sp.T_EstadoDesc,
                vinculoID = sp.I_VinculoID,
                vinculoDesc = sp.T_VinculoDesc,
                trabajadorPlanillaID = sp.I_TrabajadorPlanillaID,
                totalRemuneracion = sp.I_TotalRemuneracion,
                totalReintegro = sp.I_TotalReintegro,
                totalDeduccion = sp.I_TotalDeduccion,
                totalBruto = sp.I_TotalBruto,
                totalDescuento = sp.I_TotalDescuento,
                totalSueldo = sp.I_TotalSueldo,
                planillaID = sp.I_PlanillaID,
                periodoID = sp.I_PeriodoID,
                anio = sp.I_Anio,
                mes = sp.I_Mes,
                mesDesc = sp.T_MesDesc,
                categoriaPlanillaID = sp.I_CategoriaPlanillaID,
                categoriaPlanillaDesc = sp.T_CategoriaPlanillaDesc,
                trabajadorCategoriaPlanillaID = sp.I_TrabajadorCategoriaPlanillaID
            };

            return trabajadorDTO;
        }

        public static DocenteDTO VW_Docentes_To_DocenteDTO(VW_Docentes view)
        {
            var docenteDTO = new DocenteDTO()
            {
                trabajadorID = view.I_TrabajadorID,
                trabajadorCod = view.C_TrabajadorCod,
                nombre = view.T_Nombre,
                apellidoPaterno = view.T_ApellidoPaterno,
                apellidoMaterno = view.T_ApellidoMaterno,
                tipoDocumentoID = view.I_TipoDocumentoID,
                tipoDocumentoDesc = view.T_TipoDocumentoDesc,
                numDocumento = view.C_NumDocumento,
                fechaIngreso = view.D_FechaIngreso,
                regimenID = view.I_RegimenID,
                regimenDesc = view.T_RegimenDesc,
                estadoID = view.I_EstadoID,
                estadoDesc = view.T_EstadoDesc,
                vinculoID = view.I_VinculoID,
                vinculoDesc = view.T_VinculoDesc,
                docenteID = view.I_DocenteID,
                categoriaDocenteID = view.I_CategoriaDocenteID,
                categoriaDocenteCod = view.C_CategoriaDocenteCod,
                categoriaDocenteDesc = view.T_CategoriaDocenteDesc,
                horasDocenteID = view.I_HorasDocenteID,
                horas = view.I_Horas,
                dedicacionDocenteID = view.I_DedicacionDocenteID,
                dedicacionDocenteCod = view.C_DedicacionDocenteCod,
                dedicacionDocenteDesc = view.T_DedicacionDocenteDesc,
                estaHabilitado = view.B_Habilitado
            };

            return docenteDTO;
        }

        public static AdministrativoDTO VW_Administrativo_To_AdministrativoDTO(VW_Administrativo view)
        {
            var administrativoDTO = new AdministrativoDTO()
            {
                trabajadorID = view.I_TrabajadorID,
                trabajadorCod = view.C_TrabajadorCod,
                nombre = view.T_Nombre,
                apellidoPaterno = view.T_ApellidoPaterno,
                apellidoMaterno = view.T_ApellidoMaterno,
                tipoDocumentoID = view.I_TipoDocumentoID,
                tipoDocumentoDesc = view.T_TipoDocumentoDesc,
                numDocumento = view.C_NumDocumento,
                fechaIngreso = view.D_FechaIngreso,
                regimenID = view.I_RegimenID,
                regimenDesc = view.T_RegimenDesc,
                estadoID = view.I_EstadoID,
                estadoDesc = view.T_EstadoDesc,
                vinculoID = view.I_VinculoID,
                vinculoDesc = view.T_VinculoDesc,
                administrativoID = view.I_AdministrativoID,
                grupoOcupacionalID = view.I_GrupoOcupacionalID,
                grupoOcupacionalCod = view.C_GrupoOcupacionalCod,
                grupoOcupacionalDesc = view.T_GrupoOcupacionalDesc,
                nivelRemunerativoID = view.I_NivelRemunerativoID,
                nivelRemunerativoCod = view.C_NivelRemunerativoCod,
                nivelRemunerativoDesc = view.T_NivelRemunerativoDesc,
                estaHabilitado = view.B_Habilitado
            };

            return administrativoDTO;
        }

        public static EstadoDTO TC_Estado_To_EstadoDTO(TC_Estado table)
        {
            var estadoDTO = new EstadoDTO()
            { 
                estadoID = table.I_EstadoID,
                estadoDesc = table.T_EstadoDesc,
                estadoCod = table.C_EstadoCod,
                estaHabilitado = table.B_Habilitado
            };

            return estadoDTO;
        }

        public static RegimenDTO TC_Regimen_To_RegimenDTO(TC_Regimen table)
        {
            var regimenDTO = new RegimenDTO()
            {
                regimenID = table.I_RegimenID,
                regimenDesc = table.T_RegimenDesc,
                regimenCod = table.C_RegimenCod,
                estaHabilitado = table.B_Habilitado
            };

            return regimenDTO;
        }

        public static VinculoDTO TC_Vinculo_To_VinculoDTO(TC_Vinculo table)
        {
            var vinculoDTO = new VinculoDTO()
            {
                vinculoID = table.I_VinculoID,
                vinculoDesc = table.T_VinculoDesc,
                vinculoCod = table.C_VinculoCod,
                estaHabilitado = table.B_Habilitado
            };

            return vinculoDTO;
        }

        public static TipoDocumentoDTO TC_TipoDocumento_To_TipoDocumentoDTO(TC_TipoDocumento table)
        {
            var tipoDocumentoDTO = new TipoDocumentoDTO()
            {
                tipoDocumentoID = table.I_TipoDocumentoID,
                tipoDocumentoDesc = table.T_TipoDocumentoDesc,
                tipoDocumentoCod = table.T_TipoDocumentoCod,
                estaHabilitado = table.B_Habilitado
            };

            return tipoDocumentoDTO;
        }

        public static BancoDTO TC_Banco_To_BancoDTO(TC_Banco table)
        {
            var bancoDTO = new BancoDTO()
            {
                bancoID = table.I_BancoID,
                bancoDesc = table.T_BancoDesc,
                bancoAbrv = table.T_BancoAbrv,
                bancoCod = table.C_BancoCod,
                estaHabilitado = table.B_Habilitado
            };

            return bancoDTO;
        }

        public static DependenciaDTO TC_Dependencia_To_DependenciaDTO(TC_Dependencia table)
        {
            var dependenciaDTO = new DependenciaDTO()
            {
                dependenciaID = table.I_DependenciaID,
                dependenciaDesc = table.T_DependenciaDesc,
                dependenciaCod = table.C_DependenciaCod,
                estaHabilitado = table.B_Habilitado
            };

            return dependenciaDTO;
        }

        public static AfpDTO TC_Afp_To_AfpDTO(TC_Afp table) 
        {
            var afpDTO = new AfpDTO()
            {
                afpID = table.I_AfpID,
                afpDesc = table.T_AfpDesc,
                afpCod = table.C_AfpCod,
                estaHabilitado = table.B_Habilitado
            };

            return afpDTO;
        }

        public static GrupoOcupacionalDTO TC_GrupoOcupacional_To_GrupoOcupacionalDTO(TC_GrupoOcupacional table)
        {
            var grupoOcupacionalDTO = new GrupoOcupacionalDTO()
            {
                grupoOcupacionalID = table.I_GrupoOcupacionalID,
                grupoOcupacionalCod = table.C_GrupoOcupacionalCod,
                grupoOcupacionalDesc = table.T_GrupoOcupacionalDesc,
                estaHabilitado = table.B_Habilitado
            };

            return grupoOcupacionalDTO;
        }

        public static NivelRemunerativoDTO TC_NivelRemunerativo_To_NivelRemunerativoDTO(TC_NivelRemunerativo table)
        {
            var nivelRemunerativoDTO = new NivelRemunerativoDTO()
            {
                nivelRemunerativoID = table.I_NivelRemunerativoID,
                nivelRemunerativoCod = table.C_NivelRemunerativoCod,
                nivelRemunerativoDesc = table.T_NivelRemunerativoDesc,
                estaHabilitado = table.B_Habilitado
            };

            return nivelRemunerativoDTO;
        }

        public static CategoriaDocenteDTO TC_CategoriaDocente_To_CategoriaDocenteDTO(TC_CategoriaDocente table)
        {
            var categoriaDocenteDTO = new CategoriaDocenteDTO()
            {
                categoriaDocenteID = table.I_CategoriaDocenteID,
                categoriaDocenteDesc = table.T_CategoriaDocenteDesc,
                categoriaDocenteCod = table.C_CategoriaDocenteCod,
                estaHabilitado = table.B_Habilitado,
                esParaDocenteOrdinario = table.B_ParaDocenteOrdinario
            };

            return categoriaDocenteDTO;
        }

        public static DedicacionDocenteDTO TC_DedicacionDocente_To_DedicacionDocenteDTO(TC_DedicacionDocente table)
        {
            var dedicacionDocenteDTO = new DedicacionDocenteDTO()
            {
                dedicacionDocenteID = table.I_DedicacionDocenteID,
                dedicacionDocenteCod = table.C_DedicacionDocenteCod,
                dedicacionDocenteDesc = table.T_DedicacionDocenteDesc,
                esParaDocenteOrdinario = table.B_ParaDocenteOrdinario,
                estaHabilitado = table.B_Habilitado
            };

            return dedicacionDocenteDTO;
        }

        public static PersonaDTO TC_Persona_To_PersonaDTO(TC_Persona table)
        {
            var personaDTO = new PersonaDTO()
            {
                personaID = table.I_PersonaID,
                tipoDocumentoID = table.I_TipoDocumentoID,
                numDocumento = table.C_NumDocumento,
                nombre = table.T_Nombre,
                apellidoPaterno = table.T_ApellidoPaterno,
                apellidoMaterno = table.T_ApellidoMaterno,
                fecNac = table.D_FecNac,
                cui = table.C_Cui
            };

            return personaDTO;
        }

        public static CategoriaPlanillaDTO TC_CategoriaPlanilla_To_CategoriaPlanillaDTO(TC_CategoriaPlanilla table)
        {
            var categoriaPlanillaDTO = new CategoriaPlanillaDTO()
            {
                categoriaPlanillaID = table.I_CategoriaPlanillaID,
                categoriaPlanillaDesc = table.T_CategoriaPlanillaDesc,
                estaHabilitado = table.B_Habilitado
            };

            return categoriaPlanillaDTO;
        }

        public static MesDTO TR_Periodo_To_MesDTO(TR_Periodo table)
        {
            var mesDTO = new MesDTO()
            {
                mes = table.I_Mes,
                mesDesc = table.T_MesDesc
            };

            return mesDTO;
        }

        public static TrabajadorCategoriaPlanillaDTO VW_TrabajadoresCategoriaPlanilla_To_TrabajadorCategoriaPlanillaDTO(
            VW_TrabajadoresCategoriaPlanilla view)
        {
            var trabajadorCategoriaPlanillaDTO = new TrabajadorCategoriaPlanillaDTO()
            {
                trabajadorID = view.I_TrabajadorID,
                trabajadorCod = view.C_TrabajadorCod,
                codigoPlaza = view.C_CodigoPlaza,
                personaID = view.I_PersonaID,
                nombre = view.T_Nombre,
                apellidoPaterno = view.T_ApellidoPaterno,
                apellidoMaterno = view.T_ApellidoMaterno,
                tipoDocumentoID = view.I_TipoDocumentoID,
                tipoDocumentoDesc = view.T_TipoDocumentoDesc,
                numDocumento = view.C_NumDocumento,
                estadoID = view.I_EstadoID,
                estadoDesc = view.T_EstadoDesc,
                vinculoID = view.I_VinculoID,
                vinculoCod = view.C_VinculoCod,
                vinculoDesc = view.T_VinculoDesc,
                regimenID = view.I_RegimenID,
                regimenDesc = view.T_RegimenDesc,
                trabajadorCategoriaPlanillaID = view.I_TrabajadorCategoriaPlanillaID,
                categoriaPlanillaID = view.I_CategoriaPlanillaID,
                categoriaPlanillaDesc = view.T_CategoriaPlanillaDesc,
                esCategoriaPrincipal = view.B_CategoriaPrincipal,
                dependenciaID = view.I_DependenciaID,
                dependenciaCod = view.C_DependenciaCod,
                dependenciaDesc = view.T_DependenciaDesc,
                grupoTrabajoID = view.I_GrupoTrabajoID,
                grupoTrabajoCod = view.C_GrupoTrabajoCod,
                grupoTrabajoDesc = view.T_GrupoTrabajoDesc,
                estaHabilitado = view.B_Habilitado,
                esJefe = view.B_EsJefe
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
                conceptoReferenciaID = view.I_ConceptoReferenciaID,
                conceptoCod = view.C_ConceptoCod,
                conceptoDesc = view.T_ConceptoDesc,
                conceptoAbrv = view.T_ConceptoAbrv
            };

            return conceptoReferenciaDTO;
        }

        public static ValorExternoLecturaDTO ExcelDataReader_To_ValorExternoLecturaDTO(IExcelDataReader reader)
        {
            string stringValue; int intValue; decimal decimalValue;

            var dtoConceptoExternoValorDTO = new ValorExternoLecturaDTO();

            if (reader.GetValue(0) != null)
            {
                stringValue = reader.GetValue(0).ToString();

                if (int.TryParse(stringValue, out intValue))
                    dtoConceptoExternoValorDTO.anio = intValue;
            }

            if (reader.GetValue(1) != null)
            {
                stringValue = reader.GetValue(1).ToString();

                if (int.TryParse(stringValue, out intValue))
                    dtoConceptoExternoValorDTO.mes = intValue;
            }

            if (reader.GetValue(2) != null)
            {
                dtoConceptoExternoValorDTO.tipoDocumentoCod = reader.GetValue(2).ToString();
            }

            if (reader.GetValue(3) != null)
            {
                dtoConceptoExternoValorDTO.numDocumento = reader.GetValue(3).ToString();
            }

            if (reader.GetValue(4) != null)
            {
                stringValue = reader.GetValue(4).ToString();

                if (int.TryParse(stringValue, out intValue))
                    dtoConceptoExternoValorDTO.categoriaPlanillaID = intValue;
            }


            if (reader.GetValue(5) != null)
            {
                dtoConceptoExternoValorDTO.conceptoCod = reader.GetValue(5).ToString();
            }
            
            if (reader.GetValue(6) != null)
            {
                stringValue = reader.GetValue(6).ToString();

                if (decimal.TryParse(stringValue, out decimalValue))
                    dtoConceptoExternoValorDTO.valorConcepto = decimalValue;
            }

            if (reader.GetValue(7) != null)
            {
                stringValue = reader.GetValue(7).ToString();

                if (int.TryParse(stringValue, out intValue))
                    dtoConceptoExternoValorDTO.proveedorID = intValue;
            }

            return dtoConceptoExternoValorDTO;
        }

        public static ProveedorDTO TC_Proveedor_To_ProveedorDTO(
            TC_Proveedor table)
        {
            var proveedorDTO = new ProveedorDTO()
            {
                proveedorID = table.I_ProveedorID,
                proveedorDesc = table.T_ProveedorDesc,
                estaHabilitado = table.B_Habilitado
            };

            return proveedorDTO;
        }

        public static PeriodoDTO TR_Periodo_To_PeriodoDTO(TR_Periodo table)
        {
            var periodoDTO = new PeriodoDTO()
            {
                periodoID = table.I_PeriodoID,
                anio = table.I_Anio,
                mes = table.I_Mes,
                mesDesc = table.T_MesDesc
            };

            return periodoDTO;
        }

        public static ValorExternoConceptoDTO VW_ValoresExternos_To_ValorExternoConceptoDTO(VW_ValoresExternos view)
        {
            var valorExternoConceptoDTO = new ValorExternoConceptoDTO()
            {
                conceptoExternoValorID = view.I_ConceptoExternoValorID,
                periodoID = view.I_PeriodoID,
                anio = view.I_Anio,
                mes = view.I_Mes,
                mesDesc = view.T_MesDesc,
                trabajadorID = view.I_TrabajadorID,
                tipoDocumentoDesc = view.T_TipoDocumentoDesc,
                numDocumento = view.C_NumDocumento,
                apellidoPaterno = view.T_ApellidoPaterno,
                apellidoMaterno = view.T_ApellidoMaterno,
                nombre = view.T_Nombre,
                trabajadorCategoriaPlanillaID = view.I_TrabajadorCategoriaPlanillaID,
                categoriaPlanillaID = view.I_CategoriaPlanillaID,
                categoriaPlanillaDesc = view.T_CategoriaPlanillaDesc,
                conceptoID = view.I_ConceptoID,
                conceptoCod = view.C_ConceptoCod,
                conceptoDesc = view.T_ConceptoDesc,
                tipoConceptoDesc = view.T_TipoConceptoDesc,
                valorConcepto = view.M_ValorConcepto,
                proveedorID = view.I_ProveedorID,
                proveedorDesc = view.T_ProveedorDesc,
                trabajadorCod = view.C_TrabajadorCod,
                vinculoCod = view.C_VinculoCod,
                vinculoDesc = view.T_VinculoDesc,
                estadoDesc = view.T_EstadoDesc,
                tipoDocumentoCod = view.T_TipoDocumentoCod
            };

            return valorExternoConceptoDTO;
        }

        public static UsuarioDTO VW_Usuarios_To_UsuarioDTO(VW_Usuarios view)
        {
            var usuarioDTO = new UsuarioDTO()
            {
                userId = view.UserId,
                userName = view.UserName,
                fecActualizaPassword = view.D_FecActualizaPassword,
                cambiaPassword = view.B_CambiaPassword,
                estaHabilitado = view.B_Habilitado,
                usuarioCre = view.I_UsuarioCre,
                datosUsuarioID = view.I_DatosUsuarioID,
                numDoc = view.N_NumDoc,
                nomPersona = view.T_NomPersona,
                correoUsuario = view.T_CorreoUsuario,
                fecAlta = view.D_FecAlta,
                roleId = view.RoleId,
                roleName = view.RoleName,
                dependenciaID = view.I_DependenciaID,
                dependenciaDesc = view.T_DependenciaDesc
            };

            return usuarioDTO;
        }

        public static RolDTO Webpages_Roles_To_Webpages_Roles(Webpages_Roles table)
        {
            var rolDTO = new RolDTO()
            {
                roleId = table.RoleId,
                roleName = table.RoleName
            };

            return rolDTO;
        }

        public static ActividadDTO TC_Actividad_To_ActividadDTO(TC_Actividad table)
        {
            var actividadDTO = new ActividadDTO()
            {
                actividadID = table.I_ActividadID,
                actividadDesc = table.T_ActividadDesc,
                actividadCod = table.C_ActividadCod
            };

            return actividadDTO;
        }

        public static MetaDTO TC_Meta_To_MetaDTO(TC_Meta table)
        {
            var metaDTO = new MetaDTO()
            {
                metaID = table.I_MetaID,
                metaDesc = table.T_MetaDesc,
                metaCod = table.C_MetaCod
            };

            return metaDTO;
        }

        public static DepActividadMetaDTO VW_DepActividadMeta_To_DepActividadMetaDTO(VW_DepActividadMeta view)
        {
            var depActividadMetaDTO = new DepActividadMetaDTO()
            {
                depActividadMetaID = view.I_DepActividadMetaID,
                anio = view.I_Anio,
                categoriaPlanillaID = view.I_CategoriaPlanillaID,
                categoriaPlanillaDesc = view.T_CategoriaPlanillaDesc,
                dependenciaID = view.I_DependenciaID,
                dependenciaCod = view.C_DependenciaCod,
                dependenciaDesc = view.T_DependenciaDesc,
                descripcion = view.T_Descripcion,
                actividadID = view.I_ActividadID,
                actividadCod = view.C_ActividadCod,
                actividadDesc = view.T_ActividadDesc,
                metaID = view.I_MetaID,
                metaCod = view.C_MetaCod,
                metaDesc = view.T_MetaDesc,
                categoriaPresupuestalID = view.I_CategoriaPresupuestalID,
                categoriaPresupCod = view.T_CategoriaPresupCod,
                categoriaPresupDesc = view.T_CategoriaPresupDesc
            };

            return depActividadMetaDTO;
        }

        public static CategoriaPresupuestalDTO TC_CategoriaPresupuestal_To_CategoriaPresupuestalDTO(TC_CategoriaPresupuestal table)
        {
            var categoriaPresupuestalDTO = new CategoriaPresupuestalDTO()
            {
                categoriaPresupuestalID = table.I_CategoriaPresupuestalID,
                categoriaPresupDesc = table.T_CategoriaPresupDesc,
                categoriaPresupCod = table.T_CategoriaPresupCod,
                estaHabilitado = table.B_Habilitado
            };

            return categoriaPresupuestalDTO;
        }

        public static ResumenPorActividadYDependenciaDTO USP_S_ListarResumenPorActividadYDependencia_To_ResumenPorActividadYDependenciaDTO(
            USP_S_ListarResumenPorActividadYDependencia sp)
        {
            var dto = new ResumenPorActividadYDependenciaDTO()
            {
                actividadCod = sp.C_ActividadCod,
                dependenciaCod = sp.C_DependenciaCod,
                dependenciaDesc = sp.T_DependenciaDesc,
                totalRemuneracion = sp.I_TotalRemuneracion,
                totalReintegro = sp.I_TotalReintegro,
                totalDeduccion = sp.I_TotalDeduccion,
                totalBruto = sp.I_TotalBruto,
                totalDescuento = sp.I_TotalDescuento,
                totalSueldo = sp.I_TotalSueldo
            };

            return dto;
        }

        public static GrupoTrabajoDTO TC_GrupoTrabajo_To_GrupoTrabajoDTO(TC_GrupoTrabajo table)
        {
            var dto = new GrupoTrabajoDTO()
            {
                grupoTrabajoID = table.I_GrupoTrabajoID,
                grupoTrabajoDesc = table.T_GrupoTrabajoDesc,
                grupoTrabajoCod = table.C_GrupoTrabajoCod,
                estaHabilitado = table.B_Habilitado
            };

            return dto;
        }

        public static TrabajadorConPlanillaDTO USP_S_ListarTrabajadoresConPlanilla_To_TrabajadorConPlanillaDTO(USP_S_ListarTrabajadoresConPlanilla sp)
        {
            var dto = new TrabajadorConPlanillaDTO()
            {
                trabajadorID = sp.I_TrabajadorID,
                trabajadorCod = sp.C_TrabajadorCod,
                nombre = sp.T_Nombre,
                apellidoPaterno = sp.T_ApellidoPaterno,
                apellidoMaterno = sp.T_ApellidoMaterno,
                tipoDocumentoDesc = sp.T_TipoDocumentoDesc,
                numDocumento = sp.C_NumDocumento,
                estadoDesc = sp.T_EstadoDesc,
                vinculoDesc = sp.T_VinculoDesc,
                año = sp.I_Anio,
                mes = sp.I_Mes
            };

            return dto;
        }

        public static CategoriaPlanillaGeneradaParaTrabajadorDTO USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador_To_CategoriaPlanillaGeneradaParaTrabajadorModel(
            USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador sp)
        {
            var dto = new CategoriaPlanillaGeneradaParaTrabajadorDTO()
            {
                planillaID = sp.I_PlanillaID,
                trabajadorPlanillaID = sp.I_TrabajadorPlanillaID,
                año = sp.I_Anio,
                mes = sp.I_Mes,
                mesDesc = sp.T_MesDesc,
                dependenciaDesc = sp.T_DependenciaDesc,
                categoriaPlanillaID = sp.I_CategoriaPlanillaID,
                categoriaPlanillaDesc = sp.T_CategoriaPlanillaDesc,
                clasePlanillaDesc = sp.T_ClasePlanillaDesc,
                tipoPlanillaDesc = sp.T_TipoPlanillaDesc,
                grupoTrabajoDesc = sp.T_GrupoTrabajoDesc,
                totalRemuneracion = sp.I_TotalRemuneracion,
                totalReintegro = sp.I_TotalReintegro,
                totalDeduccion = sp.I_TotalDeduccion,
                totalBruto = sp.I_TotalBruto,
                totalDescuento = sp.I_TotalDescuento,
                totalSueldo = sp.I_TotalSueldo
            };

            return dto;
        }

        public static ConceptoGeneradoDTO USP_S_ListarConceptosGeneradosPorategoriaYTrabajador_To_ConceptoGeneradoDTO(
            USP_S_ListarConceptosGeneradosPorategoriaYTrabajador sp)
        {
            var dto = new ConceptoGeneradoDTO()
            {
                trabajadorPlanillaID = sp.I_TrabajadorPlanillaID,
                tipoConceptoID = sp.I_TipoConceptoID,
                tipoConceptoDesc = sp.T_TipoConceptoDesc,
                conceptoCod = sp.C_ConceptoCod,
                conceptoDesc = sp.T_ConceptoDesc,
                conceptoAbrv = sp.T_ConceptoAbrv,
                monto = sp.M_Monto
            };

            return dto;
        }

        public static SexoDTO TC_Sexo_To_SexoDTO(TC_Sexo table)
        {
            var sexoDTO = new SexoDTO()
            {
                sexoID = table.I_SexoID,
                sexoCod = table.T_SexoCod,
                sexoDesc = table.T_SexoDesc,
                estaHabilitado = table.B_Habilitado
            };

            return sexoDTO;
        }

        public static TipoCuentaBancariaDTO TC_TipoCuentaBancaria_To_TipoCuentaBancariaDTO(TC_TipoCuentaBancaria table)
        {
            var dto = new TipoCuentaBancariaDTO()
            {
                tipoCuentaBancariaID = table.I_TipoCuentaBancariaID,
                tipoCuentaBancariaCod = table.C_TipoCuentaBancariaCod,
                tipoCuentaBancariaDesc = table.T_TipoCuentaBancariaDesc,
                estaHabilitado=table.B_Habilitado
            };

            return dto;
        }

        public static TrabajadorLecturaDTO ExcelDataReader_To_TrabajadorLecturaDTO(IExcelDataReader reader)
        {
            string stringValue; int intValue;

            var TrabajadorLecturaDTO = new TrabajadorLecturaDTO();

            if (reader.GetValue(0) != null)
            {
                TrabajadorLecturaDTO.operacion = reader.GetValue(0).ToString();
            }

            if (reader.GetValue(1) != null)
            {
                TrabajadorLecturaDTO.tipoDocumentoCod = reader.GetValue(1).ToString();
            }

            if (reader.GetValue(2) != null)
            {
                TrabajadorLecturaDTO.numDocumento = reader.GetValue(2).ToString();
            }

            if (reader.GetValue(3) != null)
            {
                TrabajadorLecturaDTO.apePaterno = reader.GetValue(3).ToString();
            }

            if (reader.GetValue(4) != null)
            {
                TrabajadorLecturaDTO.apeMaterno = reader.GetValue(4).ToString();
            }

            if (reader.GetValue(5) != null)
            {
                TrabajadorLecturaDTO.nombres = reader.GetValue(5).ToString();
            }

            if (reader.GetValue(6) != null)
            {
                TrabajadorLecturaDTO.sexoCod = reader.GetValue(6).ToString();
            }

            if (reader.GetValue(7) != null)
            {
                TrabajadorLecturaDTO.codigoTrabajador = reader.GetValue(7).ToString();
            }

            if (reader.GetValue(8) != null)
            {
                TrabajadorLecturaDTO.vinculoCod = reader.GetValue(8).ToString();
            }

            if (reader.GetValue(9) != null)
            {
                TrabajadorLecturaDTO.grupoOcupacionalCod = reader.GetValue(9).ToString();
            }

            if (reader.GetValue(10) != null)
            {
                TrabajadorLecturaDTO.nivelRemunerativoCod = reader.GetValue(10).ToString();
            }

            if (reader.GetValue(11) != null)
            {
                TrabajadorLecturaDTO.categoriaDocenteCod = reader.GetValue(11).ToString();
            }

            if (reader.GetValue(12) != null)
            {
                TrabajadorLecturaDTO.dedicacionDocenteCod = reader.GetValue(12).ToString();
            }

            if (reader.GetValue(13) != null)
            {
                stringValue = reader.GetValue(13).ToString();

                if (int.TryParse(stringValue, out intValue))
                    TrabajadorLecturaDTO.horasDocente = intValue;
            }

            if (reader.GetValue(14) != null)
            {
                TrabajadorLecturaDTO.fechaIngreso = reader.GetValue(14).ToString();
            }

            if (reader.GetValue(15) != null)
            {
                TrabajadorLecturaDTO.dependenciaCod = reader.GetValue(15).ToString();
            }

            if (reader.GetValue(16) != null)
            {
                TrabajadorLecturaDTO.bancoCod = reader.GetValue(16).ToString();
            }

            if (reader.GetValue(17) != null)
            {
                TrabajadorLecturaDTO.numeroCuentaBancaria = reader.GetValue(17).ToString();
            }

            if (reader.GetValue(18) != null)
            {
                TrabajadorLecturaDTO.tipoCuentaBancariaCod = reader.GetValue(18).ToString();
            }

            if (reader.GetValue(19) != null)
            {
                TrabajadorLecturaDTO.regimenPensionarioCod = reader.GetValue(19).ToString();
            }

            if (reader.GetValue(20) != null)
            {
                TrabajadorLecturaDTO.afpCod = reader.GetValue(20).ToString();
            }

            if (reader.GetValue(21) != null)
            {
                TrabajadorLecturaDTO.cuspp = reader.GetValue(21).ToString();
            }

            if (reader.GetValue(22) != null)
            {
                TrabajadorLecturaDTO.codigoPlaza = reader.GetValue(22).ToString();
            }

            if (reader.GetValue(23) != null)
            {
                TrabajadorLecturaDTO.estadoTrabajadorCod = reader.GetValue(23).ToString();
            }

            return TrabajadorLecturaDTO;
        }
    }
}
