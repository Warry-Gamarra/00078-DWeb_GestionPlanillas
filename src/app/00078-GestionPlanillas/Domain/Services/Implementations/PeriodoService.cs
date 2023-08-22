using Data.Connection;
using Data.Procedures;
using Data.Tables;
using Data.Views;
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
        public List<int> ListarAños()
        {
            var listar = TR_Periodo.GetYears();

            var result = listar
                .OrderByDescending(x => x.I_Anio)
                .Select(x => x.I_Anio)
                .ToList();

            return result;
        }

        public List<MesDTO> ListarMesesSegunAnio(int I_Anio)
        {
            var lista = TR_Periodo.FindMonthsByYear(I_Anio);

            var result = lista
                .OrderBy(x => x.I_Mes)
                .Select(x => Mapper.TR_Periodo_To_MesDTO(x))
                .ToList();

            return result;
        }

        public PeriodoDTO ObtenerPeriodo(int anio, int mes)
        {
            PeriodoDTO periodoDTO;

            var table = TR_Periodo.GetByYearAndMonth(anio, mes);

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

                        var listaPlanillas = VW_ResumenPlanillaTrabajador.FindAll(oldPeriodo.I_Anio, oldPeriodo.I_Mes, null);

                        existenPlanillasGeneradas = listaPlanillas.Count() > 0;

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
                    var listaPlanillas = VW_ResumenPlanillaTrabajador.FindAll(periodo.I_Anio, periodo.I_Mes, null);

                    if (listaPlanillas == null || listaPlanillas.Count() == 0)
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
