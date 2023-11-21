using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class TipoSalas
    {
        public int TipoSalasId { get; set; }
        public string Tipo { get; set; }
        public double Precio { get; set; }

        public TipoSalas(string tipo, int id)
        {
            Tipo = tipo;
            TipoSalasId = id;
        }

        public TipoSalas(string tipo, int id, double precio)
        {
            Tipo = tipo;
            TipoSalasId = id;
            Precio = precio;
        }

        public TipoSalas()
        {
            // Constructor sin parámetros
        }

        
    }
}
