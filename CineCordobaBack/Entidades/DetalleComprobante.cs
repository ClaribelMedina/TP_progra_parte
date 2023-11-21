using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class DetalleComprobante
    {
        public int DetalleComprobanteId { get; set; }
        public Funciones Funcion { get; set; }
        //public Comprobantes Comprobantes { get; set; }
        public double Precio { get; set; }
        public Promociones Promos { get; set; }

        public DetalleComprobante()
        { 
            Funcion= new Funciones();
            Promos = new Promociones();
           
            
        }

        public DetalleComprobante(int id, Funciones f, Promociones p , double precio)
        {
            DetalleComprobanteId = id;
            Funcion= f;
            Promos= p;
            Precio = precio;
             
        }


        public double CalcularSubTotal()
        {
           
            return Funcion.Sala.TipoSala.Precio ;
        }
        public DetalleComprobante(Funciones f, Promociones p)
        {
            Funcion = f;
            Promos = p;



        }
    }
}
