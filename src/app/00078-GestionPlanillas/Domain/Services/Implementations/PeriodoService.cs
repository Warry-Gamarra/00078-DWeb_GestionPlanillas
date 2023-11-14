using Data.Connection;
using Data.Procedures;
using Data.Tables;
using Data.Views;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class PeriodoService : IPeriodoService
    {
        public List<int> ListarAños(bool soloAñoConMeses)
        {
            var listar = TR_Periodo.GetYears(soloAñoConMeses);

            var result = listar
                .OrderByDescending(x => x.I_Anio)
                .Select(x => x.I_Anio)
                .ToList();

            return result;
        }

        public List<MesDTO> ListarMesesSegunAño(int año)
        {
            var lista = TR_Periodo.FindMonthsByYear(año);

            var result = lista
                .OrderBy(x => x.I_Mes)
                .Select(x => Mapper.TR_Periodo_To_MesDTO(x))
                .ToList();

            return result;
        }

        public PeriodoDTO ObtenerPeriodo(int año, int mes)
        {
            PeriodoDTO periodoDTO;

            var table = TR_Periodo.GetByYearAndMonth(año, mes);

            if (table == null)
            {
                periodoDTO = null;
            }
            else
            {
                periodoDTO = Mapper.TR_Periodo_To_PeriodoDTO(table);
            }

            return periodoDTO;
        }

        public string ObtenerMesDesc(int mes)
        {
            switch (mes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    throw new Exception("Mes no reconocido.");
            }
        }

        public Response GrabarAño(int año)
        {
            Result result;
            bool añoRepetido;

            try
            {
                añoRepetido = ListarAños(false).Exists(x => x.Equals(año));

                if (!añoRepetido)
                {
                    var registrarPeriodo = new USP_I_RegistrarAnio()
                    {
                        I_Anio = año
                    };

                    result = registrarPeriodo.Execute();
                }
                else
                {
                    result = new Result()
                    {
                        Message = "El año " + año.ToString() + " se encuentra repetido"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message
                };
            }

            return Mapper.Result_To_Response(result);
        }

        public Response GrabarPeriodo(Operacion operacion, PeriodoEntity periodoEntity, int userID)
        {
            Result result;
            bool esPeriodoRepetido;
            bool existenPlanillasGeneradas;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        esPeriodoRepetido = (TR_Periodo.GetByYearAndMonth(periodoEntity.anio, periodoEntity.mes) != null);

                        if (esPeriodoRepetido)
                        {
                            throw new Exception(String.Format("El año {0} y mes {1} ya se encuentran registrados en el sistema.",
                                periodoEntity.anio, ObtenerMesDesc(periodoEntity.mes)));
                        }

                        var registrarPeriodo = new USP_I_RegistrarPeriodo()
                        {
                            I_Anio = periodoEntity.anio,
                            I_Mes = periodoEntity.mes,
                            T_MesDesc = ObtenerMesDesc(periodoEntity.mes),
                            I_UserID = userID
                        };

                        result = registrarPeriodo.Execute();

                        break;

                    case Operacion.Actualizar:

                        if (!periodoEntity.periodoID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        var oldPeriodo = TR_Periodo.FindByID(periodoEntity.periodoID.Value);

                        if (oldPeriodo == null)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        var periodoBD = TR_Periodo.GetByYearAndMonth(periodoEntity.anio, periodoEntity.mes);

                        if (periodoBD != null && periodoBD.I_PeriodoID != periodoEntity.periodoID.Value)
                        {
                            throw new Exception(String.Format("El año {0} y mes {1} ya se encuentran registrados en el sistema.",
                                periodoEntity.anio, ObtenerMesDesc(periodoEntity.mes)));
                        }

                        existenPlanillasGeneradas = TR_Planilla.ExistePlanilla(oldPeriodo.I_Anio, oldPeriodo.I_Mes);

                        if (existenPlanillasGeneradas && (oldPeriodo.I_Anio != periodoEntity.anio || oldPeriodo.I_Mes != periodoEntity.mes))
                        {
                            throw new Exception(String.Format("Ya existen planillas generadas para el año {0} y mes {1}.",
                                oldPeriodo.I_Anio, oldPeriodo.T_MesDesc));
                        }

                        var actualizarPeriodo = new USP_U_ActualizarPeriodo()
                        {
                            I_PeriodoID = periodoEntity.periodoID.Value,
                            I_Anio = periodoEntity.anio,
                            I_Mes = periodoEntity.mes,
                            T_MesDesc = ObtenerMesDesc(periodoEntity.mes),
                            I_UserID = userID
                        };

                        result = actualizarPeriodo.Execute();

                        break;

                    default:
                        result = new Result()
                        {
                            Message = "No se reconoce la operación a realizar."
                        };

                        break;
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message
                };
            }

            return Mapper.Result_To_Response(result);
        }

        public List<PeriodoDTO> ListarPeriodos()
        {
            var lista = TR_Periodo.FindAll();

            var result = lista.Select(x => Mapper.TR_Periodo_To_PeriodoDTO(x))
                .ToList();

            return result;
        }

        public PeriodoDTO ObtenerPeriodo(int periodoID)
        {
            PeriodoDTO periodoDTO;

            var table = TR_Periodo.FindByID(periodoID);

            if (table == null)
            {
                periodoDTO = null;
            }
            else
            {
                periodoDTO = Mapper.TR_Periodo_To_PeriodoDTO(table);
            }

            return periodoDTO;
        }

        public List<MesDTO> ListarMeses()
        {
            var lista = new List<MesDTO>();

            for (int i = 1; i <= 12; i++)
            {
                lista.Add(new MesDTO() { mes = i, mesDesc = ObtenerMesDesc(i) });
            }

            return lista;
        }

        public Response Eliminar(int periodoID, int userID)
        {
            Result result;

            try
            {
                var periodo = TR_Periodo.FindByID(periodoID);

                if (periodo != null)
                {
                    var existenPlanillasGeneradas = TR_Planilla.ExistePlanilla(periodo.I_Anio, periodo.I_Mes);

                    if (!existenPlanillasGeneradas)
                    {
                        var eliminar = new USP_D_EliminarPeriodo()
                        {
                            I_PeriodoID = periodoID,
                            I_UserID = userID
                        };

                        result = eliminar.Execute();
                    }
                    else
                    {
                        result = new Result()
                        {
                            Message = "Existen planillas generadas en el periodo seleccionado."
                        };
                    }
                }
                else
                {
                    result = new Result()
                    {
                        Success = true,
                        Message = "El periodo seleccionado ha sido eliminado con anterioridad."
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message
                };
            }

            return Mapper.Result_To_Response(result);
        }
    }
}
