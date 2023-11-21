using CineCordobaBack.Datos.Implementacion;
using CineCordobaBack.Datos.Interfaz;
using CineCordobaBack.Entidades;
using CineCordobaBack.Fachada.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Fachada.Implementacion
{
    public class Aplicacion : IAplicacion
    {
        private IComprobanteDao dao;

        public Aplicacion()
        {
             dao= new ComprobanteDao();
        }

        public bool CrearComprobante(Comprobantes oComprobante)    //FALTA FALTA FALTA 
        {
            return dao.CrearComprobante(oComprobante);
        }

        public List<FormasPago> TraerFormaPago()      //listo
        {
            return dao.ObtenerFormaPago();
        }

        public List<Clientes> TraerClientes()        //listo
        {
            return dao.ObtenerClientes();
        }


        public List<Funciones> TraerFunciones(int idPeli)      //listo
        {
            return dao.ObtenerFunciones(idPeli);
        }

        public List<Peliculas> TraerPeliculas()     //listo
        {
            return dao.ObtenerPeliculas();
        }

        public List<Promociones> TraerPromociones()    //listo
        {
            return dao.ObtenerPromociones();
        }

        public int TraerProximoComprobante()          //listo

        {
            return dao.ObtenerProxComprobante();
        }

        public List<Salas> TraerSalas(int idTipoSala)               //listo
        {
            return dao.ObtenerSalas(idTipoSala);
        }

        public List<Sucursales> TraerSucursales()     //listo
        {
            return dao.ObtenerSucursales();
        }

        public List<TipoSalas> TraerTipoSalas(int idFuncion)     //listo
        {
            return dao.ObtenerTipoSala( idFuncion);
        }

        public List<Vendedores> TraerVendedores()    //listo
        {
            return dao.ObtenerVendedores();
        }

        public bool ExisteDocumentoCliente(int numDoc)
        {
            return dao.ExisteNumDocumentoCliente(numDoc);
        }

        public List<Clientes> ClientePorDoc(int numDoc)
        {
            return dao.ClientePorDocumento(numDoc);
        }

        public int TraerProximoDetalle()
        {
            return dao.ObtenerProxDetalle();
        }

        public List<DetalleComprobante> TraerDetalles(int numComprobante)
        {
            return dao.ObtenerDetallesComprobante(numComprobante);
        }
    }
}
