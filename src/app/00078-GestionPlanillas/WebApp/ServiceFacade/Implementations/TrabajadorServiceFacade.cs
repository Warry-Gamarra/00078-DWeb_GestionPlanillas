using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class TrabajadorServiceFacade : ITrabajadorServiceFacade
    {
        private ITrabajadorService _trabajadorService;
        private IAdministrativoService _administrativoService;
        private IDocenteService _docenteService;
        private IPlanillaService _planillaService;

        private readonly string serverPath;

        public TrabajadorServiceFacade()
        {
            _trabajadorService = new TrabajadorService();
            _administrativoService = new AdministrativoService();
            _docenteService = new DocenteService();
            _planillaService = new PlanillaService();

            serverPath = WebConfigParams.DirectorioCargaTrabajadores;
        }

        public IEnumerable<TrabajadorModel> ListarTrabajadores()
        {
            var lista = _trabajadorService.ListarTrabajadores()
                .Select(x => Mapper.TrabajadorDTO_To_TrabajadorModel(x));

            return lista;
        }

        public TrabajadorModel ObtenerTrabajador(int trabajadorID)
        {
            TrabajadorModel trabajadorModel;

            var trabajadorDTO = _trabajadorService.ObtenerTrabajador(trabajadorID);

            if (trabajadorDTO == null)
            {
                trabajadorModel = null;
            }
            else
            {
                trabajadorModel = Mapper.TrabajadorDTO_To_TrabajadorModel(trabajadorDTO);

                if (trabajadorModel.Vinculo.Equals(Vinculo.AdministrativoPermanente) || trabajadorModel.Vinculo.Equals(Vinculo.AdministrativoContratado))
                {
                    var administrativo = _administrativoService.ListarAdministrativoPorTrabajadorID(trabajadorID).First();

                    trabajadorModel.nivelRemunerativoID = administrativo.nivelRemunerativoID;

                    trabajadorModel.nivelRemunerativoDesc = administrativo.nivelRemunerativoDesc;

                    trabajadorModel.grupoOcupacionalID = administrativo.grupoOcupacionalID;

                    trabajadorModel.grupoOcupacionalDesc = administrativo.grupoOcupacionalDesc;
                } else if (trabajadorModel.Vinculo.Equals(Vinculo.DocentePermanente) || trabajadorModel.Vinculo.Equals(Vinculo.DocenteContratado))
                {
                    var docente = _docenteService.ListarDocentePorTrabajadorID(trabajadorID).First();

                    trabajadorModel.categoriaDocenteID = docente.categoriaDocenteID;

                    trabajadorModel.categoriaDocenteDesc = docente.categoriaDocenteDesc;

                    trabajadorModel.horasDocenteID = docente.horasDocenteID;

                    trabajadorModel.horasDocente = String.Format("{0} / {1}", docente.dedicacionDocenteCod, docente.horas);
                }
            }

            return trabajadorModel;
        }

        public Response GrabarTrabajador(Operacion operacion, TrabajadorModel model, int userID)
        {
            Response response;

            try
            {
                var trabajadorEntity = new TrabajadorEntity()
                {
                    trabajadorID = model.trabajadorID,
                    personaID = model.personaID,
                    trabajadorCod = model.trabajadorCod,
                    codigoPlaza = model.codigoPlaza.PadLeft(6,'0'),
                    apellidoPaterno = model.apellidoPaterno.ToUpperInvariant(),
                    apellidoMaterno = model.apellidoMaterno.ToUpperInvariant(),
                    nombre = model.nombre.ToUpperInvariant(),
                    tipoDocumentoID = model.tipoDocumentoID,
                    numDocumento = model.numDocumento,
                    sexoID = model.sexoID,
                    fechaIngreso = model.fechaIngreso,
                    regimenID = model.regimenID.Value,
                    estadoID = model.estadoID,
                    vinculoID = model.vinculoID,
                    bancoID = model.bancoID,
                    nroCuentaBancaria = model.nroCuentaBancaria,
                    tipoCuentaBancariaID = model.tipoCuentaBancariaID,
                    dependenciaID = model.dependenciaID,
                    afp = model.afpID,
                    cuspp = model.cuspp,
                    categoriaDocenteID = model.categoriaDocenteID,
                    horasDocenteID = model.horasDocenteID,
                    grupoOcupacionalID = model.grupoOcupacionalID,
                    nivelRemunerativoID = model.nivelRemunerativoID
                };

                response = _trabajadorService.GrabarTrabajador(operacion, trabajadorEntity, userID);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return response;
        }

        public IEnumerable<TrabajadorConPlanillaModel> ListarTrabajadoresConPlanilla(int año, int mes)
        {
            var lista = _trabajadorService.ListarTrabajadoresConPlanilla(año, mes)
                .Select(x => Mapper.TrabajadorConPlanillaDTO_To_TrabajadorConPlanillaModel(x));

            return lista;
        }

        public Tuple<string, List<TrabajadorLecturaProcesadoDTO>> ObtenerListaTrabajadores(HttpPostedFileBase file)
        {
            string newFileName;
            ILecturaArchivoService lecturaArchivoService;
            List<TrabajadorLecturaDTO> lectura;
            List<TrabajadorLecturaProcesadoDTO> lecturaProcesada;

            try
            {
                newFileName = FileManagement.GuardarArchivo(serverPath, file);

                lecturaArchivoService = FileManagement.GetLecturaService(Path.GetExtension(newFileName));

                lectura = lecturaArchivoService.ObtenerListaTrabajadores(Path.Combine(serverPath, newFileName));

                lecturaProcesada = ObtenerLecturaProcesada(lectura);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException("El archivo no tiene la estructura correcta.", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new Tuple<string, List<TrabajadorLecturaProcesadoDTO>>(newFileName, lecturaProcesada);
        }

        private List<TrabajadorLecturaProcesadoDTO> ObtenerLecturaProcesada(List<TrabajadorLecturaDTO> lectura)
        {
            //var listaTipoDocumento = _tipoDocumentoService.ListaTipoDocumentos();

            //var listaCategoriaPlanilla = _categoriaPlanillaService.ListarCategoriasPlanillas();

            //var listaProveedores = _proveedorService.ListarProveedores();

            var lecturaProcesada = new List<TrabajadorLecturaProcesadoDTO>();

            //foreach (var item in lectura)
            //{
            //    PeriodoDTO periodoDTO = null;
            //    PersonaDTO personaDTO = null;
            //    TrabajadorCategoriaPlanillaDTO trabajadorDTO = null;
            //    ConceptoDTO conceptoDTO = null;

            //    var dto = new TrabajadorLecturaProcesadoDTO()
            //    {
            //        anio = item.anio,
            //        mes = item.mes,
            //        tipoDocumentoID = item.tipoDocumentoID,
            //        numDocumento = item.numDocumento,
            //        categoriaPlanillaID = item.categoriaPlanillaID,
            //        conceptoCod = item.conceptoCod,
            //        valorConcepto = item.valorConcepto,
            //        proveedorID = item.proveedorID
            //    };

            //    dto.esDuplicadoEnArchivo = (lectura.Count(x => x.anio == item.anio && x.anio == item.anio &&
            //        x.tipoDocumentoID == item.tipoDocumentoID && x.numDocumento == item.numDocumento && x.categoriaPlanillaID == item.categoriaPlanillaID &&
            //        x.conceptoCod == item.conceptoCod) > 1);

            //    if (dto.esDuplicadoEnArchivo)
            //    {
            //        dto.observaciones.Add("El concepto se encuentra duplicado dentro del archivo para este trabajador.");
            //    }

            //    if (dto.anio.HasValue && dto.anio.Value >= 1963 && dto.anio.Value <= 2099)
            //    {
            //        dto.esAnioCorrecto = true;
            //    }
            //    else
            //    {
            //        dto.observaciones.Add("Año incorrecto.");
            //    }

            //    if (dto.mes.HasValue && dto.mes.Value >= 1 && dto.mes.Value <= 12)
            //    {
            //        dto.esMesCorrecto = true;

            //        dto.mesDesc = _periodoService.ObtenerMesDesc(dto.mes.Value);
            //    }
            //    else
            //    {
            //        dto.observaciones.Add("Mes incorrecto.");

            //        dto.mesDesc = "";
            //    }

            //    if (dto.esAnioCorrecto && dto.esMesCorrecto)
            //    {
            //        periodoDTO = _periodoService.ObtenerPeriodo(dto.anio.Value, dto.mes.Value);

            //        if (periodoDTO != null)
            //        {
            //            dto.periodoID = periodoDTO.periodoID;

            //            dto.mesDesc = periodoDTO.mesDesc;

            //            dto.esPeriodoCorrecto = true;
            //        }
            //        else
            //        {
            //            dto.observaciones.Add("El periodo no se encuentra registrado en el sistema.");
            //        }
            //    }

            //    if (dto.tipoDocumentoID.HasValue && listaTipoDocumento.Exists(x => x.tipoDocumentoID == dto.tipoDocumentoID.Value))
            //    {
            //        dto.tipoDocumentoDesc = listaTipoDocumento.First(x => x.tipoDocumentoID == dto.tipoDocumentoID.Value).tipoDocumentoDesc;

            //        dto.esTipoDocumentoCorrecto = true;
            //    }
            //    else
            //    {
            //        dto.observaciones.Add("Tipo de documento de identidad incorrecto.");

            //        dto.tipoDocumentoDesc = "";
            //    }

            //    if (dto.numDocumento != null && dto.numDocumento.Length > 0)
            //    {
            //        dto.esNumDocumentoCorrecto = true;
            //    }
            //    else
            //    {
            //        dto.observaciones.Add("Número de documento de identidad incorrecto");
            //    }

            //    if (dto.categoriaPlanillaID.HasValue && listaCategoriaPlanilla.Exists(x => x.categoriaPlanillaID == dto.categoriaPlanillaID.Value))
            //    {
            //        dto.categoriaPlanillaDesc = listaCategoriaPlanilla.First(x => x.categoriaPlanillaID == dto.categoriaPlanillaID.Value).categoriaPlanillaDesc;

            //        dto.esCategoriaPlanillaCorrecto = true;
            //    }
            //    else
            //    {
            //        dto.observaciones.Add("Categoría de planilla incorrecto.");

            //        dto.categoriaPlanillaDesc = "";
            //    }

            //    if (dto.esTipoDocumentoCorrecto && dto.esNumDocumentoCorrecto && dto.esCategoriaPlanillaCorrecto)
            //    {
            //        personaDTO = _personaService.ObtenerPersona(dto.tipoDocumentoID.Value, dto.numDocumento);

            //        if (personaDTO != null)
            //        {
            //            dto.datosPersona = String.Format("{0} {1} {2}", personaDTO.apellidoPaterno, personaDTO.apellidoMaterno, personaDTO.nombre);

            //            trabajadorDTO = _trabajadorCategoriaPlanillaService.ObtenerTrabajadorPorDocumentoYCategoria(dto.tipoDocumentoID.Value, dto.numDocumento, dto.categoriaPlanillaID.Value);

            //            if (trabajadorDTO != null)
            //            {
            //                dto.trabajadorCategoriaPlanillaID = trabajadorDTO.trabajadorCategoriaPlanillaID;

            //                dto.trabajadorExiste = true;
            //            }
            //            else
            //            {
            //                dto.observaciones.Add("El trabajador no existe en la categoria de planilla indicada.");
            //            }
            //        }
            //        else
            //        {
            //            dto.observaciones.Add("El trabajador no existe en el sistema.");
            //        }
            //    }

            //    if (dto.esPeriodoCorrecto && dto.trabajadorExiste)
            //    {
            //        dto.tienePlanilla = _planillaService.ExistePlanillaTrabajador(trabajadorDTO.trabajadorID, periodoDTO.anio, periodoDTO.mes, trabajadorDTO.categoriaPlanillaID);

            //        if (dto.tienePlanilla)
            //        {
            //            dto.observaciones.Add("El trabajador ya tiene una planilla generada para este periodo.");
            //        }
            //    }

            //    if (dto.conceptoCod != null && dto.conceptoCod.Length > 0)
            //    {
            //        conceptoDTO = _conceptoService.ObtenerConcepto(dto.conceptoCod);

            //        if (conceptoDTO != null)
            //        {
            //            dto.conceptoID = conceptoDTO.conceptoID;

            //            dto.conceptoDesc = conceptoDTO.conceptoDesc;

            //            dto.tipoConceptoDesc = conceptoDTO.tipoConceptoDesc;

            //            dto.conceptoExiste = true;

            //            if (dto.esCategoriaPlanillaCorrecto)
            //            {
            //                var lista = _plantillaPlanillaConceptoService.ListarGrupoDeConceptosAsignados(dto.categoriaPlanillaID.Value, dto.conceptoID.Value);

            //                if (lista != null && lista.Count > 0)
            //                {
            //                    dto.esValorFijo = lista.First().esValorFijo;
            //                }
            //                else
            //                {
            //                    dto.observaciones.Add("El concepto no se encuentra asignado a ninguna planilla.");
            //                }
            //            }

            //            if (dto.valorConcepto.HasValue && dto.valorConcepto.Value > 0)
            //            {
            //                if (dto.esValorFijo.HasValue)
            //                {
            //                    if (dto.esValorFijo.Value)
            //                    {
            //                        dto.valorConceptoCorrecto = true;
            //                    }
            //                    else if (dto.valorConcepto.Value <= 100)
            //                    {
            //                        dto.valorConceptoCorrecto = true;
            //                    }
            //                    else
            //                    {
            //                        dto.observaciones.Add("Valor del concepto incorrecto (porcentaje superior al 100%).");
            //                    }
            //                }
            //                else
            //                {
            //                    dto.observaciones.Add("No se reconoce si el concepto es un valor fijo o porcentual.");
            //                }
            //            }
            //            else
            //            {
            //                dto.observaciones.Add("Valor del concepto incorrecto.");
            //            }
            //        }
            //        else
            //        {
            //            dto.observaciones.Add("Código de concepto no existe en el sistema.");
            //        }
            //    }
            //    else
            //    {
            //        dto.observaciones.Add("Se necesita el Código de concepto.");
            //    }

            //    if (dto.proveedorID.HasValue && listaProveedores.Exists(x => x.proveedorID == dto.proveedorID.Value))
            //    {
            //        dto.proveedorDesc = listaProveedores.First(x => x.proveedorID == dto.proveedorID.Value).proveedorDesc;

            //        dto.esProveedorCorrecto = true;
            //    }
            //    else
            //    {
            //        dto.observaciones.Add("Proveedor de información incorrecto.");

            //        dto.proveedorDesc = "";
            //    }

            //    lecturaProcesada.Add(dto);
            //}

            return lecturaProcesada;
        }
    }
}
