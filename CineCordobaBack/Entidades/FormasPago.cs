using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class FormasPago
    {
        public int FormasPagoId { get; set; }
        public string FormasDePago { get; set; }

        public FormasPago(int id, string formaPago)
        { 
            this.FormasPagoId = id;
            this.FormasDePago = formaPago;
        }

        public FormasPago()
        {
            FormasPagoId = 0;
            
                
        }

        public override string ToString()
        {
            return "Id: " + FormasPagoId + "Forma de pago: "+ FormasDePago;
        }
    }

}
