using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class Comprobantes
    {
        public int ComprobanteId { get; set; }
        public DateTime FechaComprobante { get; set; }
        public Clientes Cliente { get; set; }
        public Sucursales Sucursal { get; set; }
        public FormasPago FormaPago { get; set; }
        public Vendedores Vendedor { get; set; }

        public List<DetalleComprobante> lDetallesComprobantes { get; set; }

        //public double total { get; set; }

        public Comprobantes(int id, DateTime fechaCompra, Clientes cliente, Sucursales suc, FormasPago formPago, Vendedores idVend)
        { 
            this.ComprobanteId = id;
            this.FechaComprobante = fechaCompra;
            this.Cliente = cliente;
            this.Sucursal = suc;
            this.FormaPago = formPago;
            this.Vendedor = idVend;
            lDetallesComprobantes = new List<DetalleComprobante>();

        }

        
        public Comprobantes()
        {
           
           
            Cliente = new Clientes();
            Sucursal = new Sucursales();
            FormaPago = new FormasPago();
            Vendedor = new Vendedores();
            lDetallesComprobantes = new List<DetalleComprobante>();
                
        }

        public void AgregarDetalle(DetalleComprobante oDetalle)
        {
            lDetallesComprobantes.Add(oDetalle);
        }
        public void EliminarDetalle(int nroDetalle)
        {
            lDetallesComprobantes.RemoveAt(nroDetalle);
        }
        public double CalcularTotal()
        {
            double total = 0;

            foreach (DetalleComprobante oDetalle in lDetallesComprobantes)
            {
                total += oDetalle.CalcularSubTotal();
                
            }
            return total;
        }



        //public void ModificarDetalles(int cantidad, Insumo oInsumo)
        //{
        //    foreach (DetalleComprobante oDetalle in lDetallesComprobantes)
        //    {
        //        if (oDetalle.oInsumo == oInsumo)
        //        {
        //            oDetalle.Cantidad = oDetalle.Cantidad + cantidad;
        //        }
        //    }
       // }

    }
}
