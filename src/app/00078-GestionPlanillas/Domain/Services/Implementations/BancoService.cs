﻿using Data.Tables;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class BancoService : IBancoService
    {
        public List<BancoDTO> ListarBancos(bool incluirDeshabilitados = false)
        {
            var lista = TC_Banco.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_Banco_To_BancoDTO(x))
                .ToList();
            
            return result;
        }

        public List<TipoCuentaBancariaDTO> ListarTipoCuentasBancarias(bool incluirDeshabilitados = false)
        {
            var lista = TC_TipoCuentaBancaria.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_TipoCuentaBancaria_To_TipoCuentaBancariaDTO(x))
                .ToList();

            return result;
        }
    }
}
