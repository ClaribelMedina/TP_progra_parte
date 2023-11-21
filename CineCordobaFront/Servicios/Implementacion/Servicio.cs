using CineCordobaBack.Datos.Implementacion;
using CineCordobaBack.Datos.Interfaz;
using CineCordobaBack.Entidades;
using CineCordobaFront.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaFront.Servicios.Implementacion
{
    public class Servicio : IServicio
    {
        private IComprobanteDao dao;

        public Servicio()                 //constructor con definicion de dao
        {
            dao= new ComprobanteDao();
               
        }



        public bool CrearComprobante(Comprobantes oComprobantes)
        {
            throw new NotImplementedException();
        }

        public List<Clientes> TraerClientes()
        {
            throw new NotImplementedException();
        }

        public List<FormasPago> TraerFormasPago()
        {
            return dao.ObtenerFormaPago();
        }

        public List<Funciones> TraerFunciones()
        {
            throw new NotImplementedException();
        }

        public List<Peliculas> TraerPeliculas()
        {
            throw new NotImplementedException();
        }

        public List<Promociones> TraerPromociones()
        {
            throw new NotImplementedException();
        }

        public int TraerProximoComprobante()
        {
            throw new NotImplementedException();
        }

        public List<Salas> TraerSalas()
        {
            throw new NotImplementedException();
        }

        public List<Sucursales> TraerSucursales()
        {
            throw new NotImplementedException();
        }

        public List<TipoSalas> TraerTipoSalas()
        {
            throw new NotImplementedException();
        }

        public List<Vendedores> TraerVendedores()
        {
            throw new NotImplementedException();
        }
    }
}
