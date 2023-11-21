using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class Clientes
    {
        public int ClienteId { get; set; }
        public int NroDoc { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        //public DateTime FechaNac { get; set; }
        //public int Telefono { get; set; }
        //public string Email { get; set; }
        //public Barrios BarrioId { get; set; }
        //public string Calle { get; set; }
        //public int Altura { get; set; }
        //public TipoDoc TipoDocId { get; set; }

     

        public Clientes(int clienteId, string nombre, string apellido, int doc)
        {
            this.ClienteId= clienteId;
            this.Nombre= nombre;
            this.Apellido= apellido;
            this.NroDoc= doc;
        }

        public Clientes(string nombre, string apellido)
        {
            Nombre= nombre;
            Apellido= apellido;
        }

        public Clientes()
        {
            
        }
        public string NombreCompleto
        {
           get { return $"{Nombre}, {Apellido}"; } 
        }

        public override string ToString()
        {
            return Apellido;
        }



    }
}
