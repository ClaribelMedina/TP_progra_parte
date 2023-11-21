using CineCordobaBack.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Datos.Interfaz
{
    public interface IComprobanteDao
    {
        public List<FormasPago> ObtenerFormaPago();
        List<Clientes> ObtenerClientes();
        List<Funciones> ObtenerFunciones(int idPeli);
        List<Peliculas> ObtenerPeliculas();
        List<Promociones> ObtenerPromociones();
        int ObtenerProxComprobante();
        List<Salas> ObtenerSalas(int idTipoSala);
        List<Sucursales> ObtenerSucursales();
        List<TipoSalas> ObtenerTipoSala(int idFuncion);
        List<Vendedores> ObtenerVendedores();
        bool CrearComprobante(Comprobantes oComprobante);

        bool ExisteNumDocumentoCliente(int numDoc);

        List<Clientes> ClientePorDocumento(int numDoc);
        int ObtenerProxDetalle();

        List<DetalleComprobante> ObtenerDetallesComprobante(int numComprobante);
    }
}
