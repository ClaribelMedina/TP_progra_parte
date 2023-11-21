using CineCordobaBack.Datos.Interfaz;
using CineCordobaBack.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Datos.Implementacion
{
    public class ComprobanteDao: IComprobanteDao
    {
        public bool CrearComprobante(Comprobantes oComprobante)           //TRANSACCIONNNNNNNNNN
        {
            bool aux = true;
            SqlConnection cnn = HelperDao.ObtenerInstancia().ObtenerConexion();
            //int nroComp = HelperDao.ObtenerInstancia().ConsultaEscalar("SP_PROXIMO_COMPROBANTE", "@next");
            SqlTransaction t = null;

            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();

                //INSERT DE COMPROBANTE
                SqlCommand cmdComprobante = new SqlCommand("SP_INSERTAR_COMPROBANTE", cnn, t);
                cmdComprobante.CommandType = CommandType.StoredProcedure;
                cmdComprobante.Parameters.AddWithValue("@fecha", oComprobante.FechaComprobante);
                cmdComprobante.Parameters.AddWithValue("@idCliente", oComprobante.Cliente.ClienteId);
                cmdComprobante.Parameters.AddWithValue("@idSucursal", oComprobante.Sucursal.SucursalId);
                cmdComprobante.Parameters.AddWithValue("@idFormaPago", oComprobante.FormaPago.FormasPagoId);
                cmdComprobante.Parameters.AddWithValue("@idVendedor", oComprobante.Vendedor.VendedorId);

                SqlParameter param = new SqlParameter("@id_comprobante", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmdComprobante.Parameters.Add(param);
                cmdComprobante.ExecuteNonQuery();

                int ultimoID = Convert.ToInt32(param.Value);



                //cmdComprobante.ExecuteNonQuery();

                //INSERT DETALLES
                int cantDetalles = 1;
                foreach (DetalleComprobante dt in oComprobante.lDetallesComprobantes)
                {
                    SqlCommand cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE_COMPROBANTE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    //cmdDetalle.Parameters.AddWithValue("@idDetalle", nroDetalle);
                    cmdDetalle.Parameters.AddWithValue("@idfuncion", dt.Funcion.FuncionId);
                    cmdDetalle.Parameters.AddWithValue("@idComprobante", ultimoID);
                    cmdDetalle.Parameters.AddWithValue("@precio", dt.Precio);
                    cmdDetalle.Parameters.AddWithValue("@idPromo", dt.Promos.PromoId);


                    SqlParameter parame = new SqlParameter("@id_detalle_comprobante", SqlDbType.Int);
                    parame.Direction = ParameterDirection.Output;
                    cmdDetalle.Parameters.Add(parame);
                    cmdDetalle.ExecuteNonQuery();

                    int idProxDet =Convert.ToInt32(parame.Value);

                    cantDetalles++;
                }

                t.Commit();

            }
            catch (Exception)
            {

                t.Rollback();
                aux = false;
            }
            finally 

            { if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();

            }

            return aux;

        }

        public List<Clientes> ObtenerClientes()
        {
            List<Clientes> lClientes = new List<Clientes>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_CLIENTES");

            foreach (DataRow fila in tabla.Rows)
            {
                int id = int.Parse(fila["id_cliente"].ToString());
                string nombre = fila["nombre"].ToString();
                string apellido = fila["apellido"].ToString();
                int documento = int.Parse(fila["nro_doc"].ToString());
                

                Clientes oCliente = new Clientes(id, nombre,apellido,documento);
                lClientes.Add(oCliente);
            }
            return lClientes;
        }


        //aca desarrolamos los metodos que necesitamos de nuestro recurso(CRUD)

        public List<FormasPago> ObtenerFormaPago()
        {
            List<FormasPago> lFormaPagos = new List<FormasPago>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_F_PAGOS");

            foreach (DataRow fila in tabla.Rows)
            {
                int id = int.Parse(fila["id_formas_pago"].ToString());
                string formaP = fila["formas_pago"].ToString();
                FormasPago oFP = new FormasPago(id, formaP);
                lFormaPagos.Add(oFP);
            }
            return lFormaPagos;
            
        }

        public List<Funciones> ObtenerFunciones(int IdPelicula)
        {
            List<Funciones> lFunciones = new List<Funciones>();
            List<Parametro> lParam = new List<Parametro>();
            Parametro parametro = new Parametro("@IdPeli", IdPelicula);
            lParam.Add(parametro);

            DataTable tabla = HelperDao.ObtenerInstancia().ConsultaTabla("SP_FUNCIONES_FILTRADAS", lParam );

            foreach (DataRow fila in tabla.Rows)
            {
                Funciones oFuncion = new Funciones();

                //creo un objeto horario para pasarselo a funcion con las properties necesarias
                Horarios oH = new Horarios();
                oH.Inicio = Convert.ToDateTime(fila["inicio"].ToString());
                oH.Final = Convert.ToDateTime(fila["final"].ToString());
                oH.HorarioId = int.Parse(fila["id_horario"].ToString());
                oFuncion.Horario= oH;


                Peliculas oP = new Peliculas();
                oP.NombrePelicula = fila["nombre_pelicula"].ToString();
                oP.PeliculaId= int.Parse(fila["id_pelicula"].ToString()) ;
                oFuncion.Pelicula = oP; 

                oFuncion.FuncionId= int.Parse(fila["id_funcion"].ToString());
                oFuncion.Fecha = Convert.ToDateTime(fila["fecha"].ToString());
                oFuncion.Subtitulo = Convert.ToInt32( fila["subtitulo"]);
                Salas oS = new Salas();
                oS.SalaId = int.Parse(fila["id_sala"].ToString());
               
                oFuncion.Sala = oS;
                
                TipoSalas oTS = new TipoSalas();
                oTS.Tipo = fila["tipo"].ToString();
                oTS.Precio = double.Parse(fila["precio"].ToString());
                oTS.TipoSalasId = int.Parse(fila["id_tipo_sala"].ToString());
                oFuncion.Sala.TipoSala = oTS;




                lFunciones.Add(oFuncion);
            }
            return lFunciones;
        }

        public List<Peliculas> ObtenerPeliculas()
        {
           
            List<Peliculas> lPeliculas = new List<Peliculas>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_PELICULAS");
            foreach (DataRow fila in tabla.Rows) {

                int PeliculaId = int.Parse(fila["id_pelicula"].ToString());
                string NombrePelicula = fila["nombre_pelicula"].ToString();
                Peliculas p = new Peliculas(PeliculaId, NombrePelicula);
                lPeliculas.Add(p);            

            }
            return lPeliculas;

        }

        

        public List<Promociones> ObtenerPromociones()
        {
            List<Promociones> lPromociones = new List<Promociones>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_PROMOS");
            foreach (DataRow fila in tabla.Rows)
            {

                
                string nombrePromo = fila["nombre_promo"].ToString();
                int id = int.Parse( fila["id_promo"].ToString());
                double desc = Convert.ToDouble(fila["descuento"].ToString());
                
                Promociones promo = new Promociones( nombrePromo, id, desc);
                lPromociones.Add(promo);

            }
            return lPromociones;

        }

        public int ObtenerProxComprobante()
        {
            return HelperDao.ObtenerInstancia().ConsultaEscalar("SP_PROXIMO_COMPROBANTE", "@next");


        }

        public bool ExisteNumDocumentoCliente(int numDoc)
        {
            List<Parametro> lparam = new List<Parametro>();
            Parametro p = new Parametro("@numDoc",numDoc);
            lparam.Add(p);
            bool existe;
            int aux;

            
             aux = HelperDao.ObtenerInstancia().ConsultaEscalarConParametros("SP_EXISTE_DOCUMENTO", "@siExiste", lparam);
            if (aux == 1)
            {
                existe = true;
            }
            else
            {
                existe = false;
            }
            return existe;
            
        }

        public List<Salas> ObtenerSalas(int idTipoSala)
        {
            List<Salas> lSalas = new List<Salas>();
            List<Parametro> lparam = new List<Parametro>();
            Parametro p = new Parametro("@IdTipoSala", idTipoSala);
            lparam.Add(p);

            DataTable tabla = HelperDao.ObtenerInstancia().ConsultaTabla("SP_SALAS_FILTRADAS", lparam);
            foreach (DataRow fila in tabla.Rows)
            {

                int id = int.Parse(fila["id_sala"].ToString());

                
                TipoSalas oTS = new TipoSalas();
                
                oTS.TipoSalasId = int.Parse(fila["id_tipo_sala"].ToString());
                oTS.Tipo = fila["tipo"].ToString();
                oTS.Precio = Convert.ToDouble(fila["precio"].ToString());

                

                
                Salas sala = new Salas(id,oTS);
                lSalas.Add(sala);

            }
            return lSalas;
        }

        public List<Sucursales> ObtenerSucursales()
        {
            List<Sucursales> lSucursales = new List<Sucursales>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_SUCURSALES");
            foreach (DataRow fila in tabla.Rows)
            {

                int idSucursal = int.Parse(fila["id_sucursal"].ToString());
                string nombreSucursal = fila["nombre_sucursal"].ToString();
                Sucursales s = new Sucursales(idSucursal, nombreSucursal);
                lSucursales.Add(s);
            }
            return lSucursales;
        }

       

        public List<TipoSalas> ObtenerTipoSala(int idFuncion)
        {
            List<TipoSalas> lTipoSalas = new List<TipoSalas>();
            List<Parametro> lparam = new List<Parametro>();
            Parametro p = new Parametro("@IdFuncion", idFuncion);
            lparam.Add(p);


            DataTable tabla = HelperDao.ObtenerInstancia().ConsultaTabla("SP_SALA_FILT_POR_FUNCION", lparam);
            foreach (DataRow fila in tabla.Rows)
            {

                string tipo = fila["tipo"].ToString();
                int idTipoSala = int.Parse(fila["id_tipo_sala"].ToString());
                double precio = Convert.ToDouble(fila["precio"].ToString());

                TipoSalas oTipoSala = new TipoSalas( tipo, idTipoSala,precio );
                lTipoSalas.Add(oTipoSala);
            }
            return lTipoSalas;
        }


       


        public List<Vendedores> ObtenerVendedores()
        {
            List<Vendedores> lVendedores = new List<Vendedores>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_VENDEDORES");

            foreach (DataRow fila in tabla.Rows)
            {
                int id = int.Parse(fila["id_vendedor"].ToString());
                string nombre = fila["nombre"].ToString();
                string apellido = fila["apellido"].ToString();


                Vendedores oVendedor = new Vendedores (id, nombre, apellido);
                lVendedores.Add(oVendedor);
            }
            return lVendedores;
        }

        public List<Clientes> ClientePorDocumento(int numDoc)
        {
            List<Parametro> lparam = new List<Parametro>();
            Parametro p = new Parametro("@numDoc", numDoc);
            lparam.Add(p);
            List<Clientes> lclientes = new List<Clientes>();
            DataTable tabla = HelperDao.ObtenerInstancia().ConsultaTabla("SP_CLIENTE_POR_DOCUMENTO", lparam);
            foreach (DataRow fila in tabla.Rows)
            {

                string nombre = fila["nombre"].ToString();
                string apellido = fila["apellido"].ToString ();


                Clientes oCliente = new Clientes(nombre,apellido);
                lclientes.Add(oCliente);
            }
            return lclientes;

           
        }

        public List<DetalleComprobante> ObtenerDetallesComprobante(int idComprobante)
        {
            List<Parametro> lparam = new List<Parametro>();
            Parametro p = new Parametro("@id_comprobante", idComprobante);
            lparam.Add(p);
            List<DetalleComprobante> ldetalles = new List<DetalleComprobante>();
            DataTable tabla = HelperDao.ObtenerInstancia().ConsultaTabla("SP_CONSULTAR_DETALLES", lparam);
            foreach (DataRow fila in tabla.Rows)
            {
                
                int id =int.Parse(fila["id_detalle_comprobante"].ToString());
                int idComprob = int.Parse(fila["id_comprobante"].ToString());
                double precio = double.Parse(fila["precio"].ToString());
                
                Funciones f = new Funciones();
                f.FuncionId = int.Parse(fila["id_funcion"].ToString());               


                Promociones promo = new Promociones();
                promo.PromoId = int.Parse(fila["id_promo"].ToString()) ;    



                DetalleComprobante oDet = new DetalleComprobante(id,f,promo,precio);
                ldetalles.Add(oDet);
            }
            return ldetalles;
        }
      
        public int ObtenerProxDetalle()
        {
            return HelperDao.ObtenerInstancia().ConsultaEscalar("SP_PROXIMO_DETALLE_COMPROBANTE", "@proxDetalle");
        }

        
    }
}
