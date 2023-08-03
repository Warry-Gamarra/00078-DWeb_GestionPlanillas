using Data.Tables;
using Data.Views;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class ProveedorService : IProveedorService
    {
        public List<ProveedorDTO> ListarProveedores(bool incluirDeshabilitados = false)
        {
            var lista = TC_Proveedor.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_Proveedor_To_ProveedorDTO(x))
                .ToList();

            return result;
        }

        public ProveedorDTO ObtenerProveedor(int proveedorID)
        {
            ProveedorDTO proveedorDTO;

            var table = TC_Proveedor.FindByID(proveedorID);

            if (table == null)
            {
                proveedorDTO = null;
            }
            else
            {
                proveedorDTO = Mapper.TC_Proveedor_To_ProveedorDTO(table);
            }

            return proveedorDTO;
        }
    }
}
