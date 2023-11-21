using CineCordobaBack.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaFront.Servicios.Interfaz
{
    public interface IServicio
    {
        int TraerProximoComprobante();
        List<Clientes> TraerClientes();
        List<Vendedores> TraerVendedores();
        List<FormasPago> TraerFormasPago();
        List<Sucursales> TraerSucursales();
        List<Peliculas> TraerPeliculas();
        List<Funciones> TraerFunciones();
        List<Salas> TraerSalas();
        List<TipoSalas> TraerTipoSalas();
        List<Promociones> TraerPromociones();

        bool CrearComprobante(Comprobantes oComprobantes);
    }
}
