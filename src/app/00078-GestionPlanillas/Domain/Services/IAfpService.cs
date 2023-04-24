﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAfpService
    {
        List<AfpDTO> ListarAfps(bool incluirDeshabilitados = false);
    }
}
