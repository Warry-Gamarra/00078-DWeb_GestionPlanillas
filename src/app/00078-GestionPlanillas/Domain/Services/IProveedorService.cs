using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IProveedorService
    {
        List<ProveedorDTO> ListarProveedores(bool incluirDeshabilitados = false);

        ProveedorDTO ObtenerProveedor(int proveedorID);
    }
}
