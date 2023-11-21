using CineCordobaBack.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Fachada.Interfaz
{
    public interface IAplicacion
    {
        List<FormasPago> TraerFormaPago();
        int TraerProximoComprobante();
        List<Clientes> TraerClientes();
        List<Vendedores> TraerVendedores();
        List<Sucursales> TraerSucursales();
        List<Peliculas> TraerPeliculas();
        List<Funciones> TraerFunciones(int idPeli);
        List<Salas> TraerSalas(int idTipoSalas);
        List<TipoSalas> TraerTipoSalas(int idFuncion);
        List<Promociones> TraerPromociones();

        bool CrearComprobante(Comprobantes oComprobantes);

        bool ExisteDocumentoCliente(int numDoc);
        List<Clientes> ClientePorDoc(int numDoc);
        int TraerProximoDetalle();

        List<DetalleComprobante> TraerDetalles(int numComprobante);
    }
}
