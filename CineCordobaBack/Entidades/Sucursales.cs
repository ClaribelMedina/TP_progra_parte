using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class Sucursales
    {
        public int SucursalId { get; set; }
        public string NombreSucursal { get; set; }
        //public Barrios BarrioId { get; set; }

        public Sucursales(int SucursalId, string NombreSucursal)
        {
            this.SucursalId = SucursalId;
            this.NombreSucursal = NombreSucursal;
        }

        public Sucursales()
        {
                
        }
    }
}
