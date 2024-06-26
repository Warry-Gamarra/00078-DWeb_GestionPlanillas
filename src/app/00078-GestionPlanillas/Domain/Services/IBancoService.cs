﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IBancoService
    {
        List<BancoDTO> ListarBancos(bool incluirDeshabilitados = false);

        List<TipoCuentaBancariaDTO> ListarTipoCuentasBancarias(bool incluirDeshabilitados = false);
    }
}
