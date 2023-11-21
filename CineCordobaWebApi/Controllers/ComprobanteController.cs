using CineCordobaBack.Entidades;
using CineCordobaBack.Fachada.Implementacion;
using CineCordobaBack.Fachada.Interfaz;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineCordobaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprobanteController : ControllerBase
    {
        private IAplicacion app;

        public ComprobanteController()   //constructor del controller
        {
                app = new Aplicacion();
        }

        
        //END POINT DE FORMAS DE PAGO
        [HttpGet("/formasDePago")]
        public IActionResult GetformasPago()
        {
            List<FormasPago> lista = null;
            try
            {
                lista = app.TraerFormaPago();
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE FUNCIONES
        [HttpGet("/funciones")]
        public IActionResult GetFunciones(int idPeli)
        {
            List<Funciones> lista = null;
            try
            {
                lista = app.TraerFunciones(idPeli) ;
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE PELICULAS
        [HttpGet("/peliculas")]
        public IActionResult GetPeliculas()
        {
            List<Peliculas> lista = null;
            try
            {
                lista = app.TraerPeliculas();
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE PROMOS
        [HttpGet("/promos")]
        public IActionResult GetPromos()
        {
            List<Promociones> lista = null;
            try
            {
                lista = app.TraerPromociones();
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE SUCURSALES
        [HttpGet("/sucursales")]
        public IActionResult GetSucursales()
        {
            List<Sucursales> lista = null;
            try
            {
                lista = app.TraerSucursales();
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE CLIENTES
        [HttpGet("/clientes")]
        public IActionResult GetClientes()
        {
            List<Clientes> lista = null;
            try
            {
                lista = app.TraerClientes();
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE VENDEDORES
        [HttpGet("/vendedores")]
        public IActionResult GetVendedores()
        {
            List<Vendedores> lista = null;
            try
            {
                lista = app.TraerVendedores();
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE salas
        [HttpGet("/salas")]
        public IActionResult GetSalas(int idTipoSalas)
        {
            List<Salas> lista = null;
            try
            {
                lista = app.TraerSalas(idTipoSalas);
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE TIPOS DE SALAS
        [HttpGet("/tipoSalas")]
        public IActionResult GetTipoSalas(int idFuncion)
        {
            List<TipoSalas> lista = null;
            try
            {
                lista = app.TraerTipoSalas(idFuncion);
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT DE PROX COMPROBANTE
        [HttpGet("/proxComprobante")]
        public IActionResult GetProximoComprobante()
        {
            int prox = 0;
            try
            {
                prox = app.TraerProximoComprobante();
                return Ok(prox);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }
        //END POINT DE PROX  DETALLE COMPROBANTE
        [HttpGet("/proxDetalleComprobante")]
        public IActionResult GetProximoDetalle()
        {
            int proxdet = 0;
            try
            {
                proxdet = app.TraerProximoDetalle();
                return Ok(proxdet);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }

        //END POINT DE detales 
        [HttpGet("/ConsultasDetalles")]
        public IActionResult GetDetalles(int numComprobante)
        {
            List<DetalleComprobante> lista = null;
            try
            {
                lista = app.TraerDetalles(numComprobante);
                return Ok(lista);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }



        //END POINT DE EXISTENCIA DE DOCUMENTO DE UN CLIENTE
        [HttpGet("/existenciaDocumento")]
        public IActionResult GetSiExisteDoc(int numDoc)
        {
            bool existe;
            try
            {
                existe = app.ExisteDocumentoCliente(numDoc);
                return Ok(existe);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }

        //END POINT CLIENTE FILTRADO POR NUM DE DOCUMENTO
        [HttpGet("/clienteFiltrdoPorDoc")]
        public IActionResult GetClientePorDocumento(int numDoc)
        {
            List<Clientes> lclientes = null;
            try
            {
                lclientes = app.ClientePorDoc(numDoc);
                return Ok(lclientes);

            }
            catch (Exception)
            {

                return StatusCode(500, "ERROR INTERNO!");

            }
        }


        //END POINT INSERT DE COMPROBANTE (TRANSACCION)
        [HttpPost("/insertComprobante")]
        public IActionResult PostInsertComprobante(Comprobantes oComprobante)
        {
            try
            {
                if(oComprobante == null)
                {
                    return BadRequest("Error! No se pudo insertar comprobante.");
                }
                return Ok(app.CrearComprobante(oComprobante));
            }
            catch (Exception)
            {

                return StatusCode(500, "Error interno, intente luego");
            }
        }















    }
}
